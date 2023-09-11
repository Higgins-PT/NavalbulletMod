using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Navalmod
{
    public struct byteAndBB
    {
        public byteAndBB(BlockBehaviour blockBehaviour,int b)
        {
            bb = blockBehaviour;
            offset = b;
        }
        public BlockBehaviour bb;
        public int offset;
    }
    public class H3NetworkManager : SingleInstance<H3NetworkManager>
    {
        public float rateSend { 
            get {return ratesend; } 
            set {ratesend = value; } 
        }
        private float ratesend = 0.1f;
        private float time;
        public H3NetworkManager()
        {
            OptionsMaster.maxSendRate = 10000000f;
            OptionsMaster.defaultSendRate = 10000000f;
            
            Messages.H3NetBlock = ModNetworking.CreateMessageType(new DataType[]
            {
                DataType.ByteArray
            });

            ModNetworking.Callbacks[Messages.H3NetBlock] += delegate (Message msg)//plyaercount*4+(blockcount 4 + playerid 2 + {block...(blockid+blockpos+blockrot)(35*block*count)})*playercount
            {
                byte[] recbytes = (byte[])msg.GetData(0);
                PullAllPlayers(recbytes);
               

            };
        }
        
        public override string Name => "manager";
        public byte[] test;
        public void FixedUpdate()
        {
            if (StatMaster.isHosting)
            {
                time += Time.fixedDeltaTime;
                if (time >= ratesend)
                {
                    time = 0;

                    //ModConsole.Log(PushAllPlayers().Length.ToString());

                    ModNetworking.SendToAll(Messages.H3NetBlock.CreateMessage(new object[]
                {
                    PushAllPlayers()
                }));



                }
                
                
            }
        }
        public void PullAllPlayers(byte[] bytes)
        {
            int offset = 0;
            int playernum = BitConverter.ToInt32(bytes, offset);
            offset += 4;
            for (int i = 0; i < playernum; i++)
            {
                int playerlenght = BitConverter.ToInt32(bytes, offset) * 35 + 6;
                byte[] playerbytes = new byte[playerlenght];
                Buffer.BlockCopy(bytes, offset, playerbytes, 0, playerlenght);
                offset += playerlenght;
                try
                {
                    PullPlayer(playerbytes);
                }
                catch
                {

                }
            }
            

        }
        public void PullPlayer(byte[] bytes)
        {
            List<byteAndBB> byteandbb = new List<byteAndBB>();
            List<H3NetworkBlock> rePos = new List<H3NetworkBlock>();
            int offset = 0;
            int blocknum = BitConverter.ToInt32(bytes, offset);
            offset += 4;
            ushort playid = BitConverter.ToUInt16(bytes, offset);
            try
            {
                PlayerData playerData = Playerlist.GetPlayer(playid);
                //List<BlockBehaviour> blockBehaviours = FindObjectsOfType<BlockBehaviour>().ToList();
                List<BlockBehaviour> blockBehaviours = ReferenceMaster.GetAllSimulationBlocks();
                try
                {
                    playerData.machine.networkBlocks = new NetworkBlock[0];
                    foreach (Machine.SimCluster simCluster in playerData.machine.simClusters)
                    {
                        simCluster.BaseTransform.GetComponent<H3NetworkBlock>().isClusterBase = true;
                    }
                }
                catch
                {

                }
                offset += 2;
                try
                {
                    for (int i = 0; i < blocknum; i++)
                    {
                        Guid guid = Int2Guid(BitConverter.ToInt32(bytes, offset), BitConverter.ToInt32(bytes, offset + 4), BitConverter.ToInt32(bytes, offset + 8), BitConverter.ToInt32(bytes, offset + 12));
                        offset += 16;

                        for (int n = 0; n < blockBehaviours.Count; n++)
                        {
                            try
                            {
                                if (blockBehaviours[n].BuildingBlock.Guid == guid && blockBehaviours[n].ParentMachine.PlayerID == playid)
                                {
                                    BlockBehaviour blockBehaviour = blockBehaviours[n];
                                    try
                                    {
                                        if (blockBehaviour.transform.parent.name == (blockBehaviour.transform.name + "Base") && blockBehaviour.transform.GetComponent<H3NetworkBlock>().isClusterBase == true)
                                        {
                                            try
                                            {
                                                if (blockBehaviour.transform.parent.GetComponent<H3NetworkBlock>() == null)
                                                {
                                                    blockBehaviour.transform.parent.gameObject.AddComponent<H3NetworkBlock>().blockBehaviour = blockBehaviour;

                                                }
                                                H3NetworkBlock h3NetworkBlock = blockBehaviour.transform.parent.GetComponent<H3NetworkBlock>();
                                                h3NetworkBlock.islocal = false;
                                                h3NetworkBlock.PullObject(ref offset, bytes);
                                                rePos.Add(h3NetworkBlock);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        else if (blockBehaviour.transform.parent.GetComponent<H3NetworkBlock>() == null)
                                        {
                                            try
                                            {
                                                H3NetworkBlock h3NetworkBlock = blockBehaviour.transform.GetComponent<H3NetworkBlock>();
                                                h3NetworkBlock.islocal = false;
                                                h3NetworkBlock.PullObject(ref offset, bytes);
                                                rePos.Add(h3NetworkBlock);
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                
                                                blockBehaviours.Remove(blockBehaviour);
                                                byteandbb.Add(new byteAndBB(blockBehaviour, offset));
                                                
                                                offset += 19;
                                                
                                                
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    break;
                                }
                            }
                            catch
                            {

                            }
                        }

                    }
                }
                catch
                {

                }
               foreach(byteAndBB byteAndBB in byteandbb)
                {
                    H3NetworkBlock h3NetworkBlock = byteAndBB.bb.GetComponent<H3NetworkBlock>();
                    h3NetworkBlock.islocal = true;
                    int offsetE = 0;
                    offsetE = byteAndBB.offset;
                    h3NetworkBlock.PullObject(ref offsetE, bytes);
                }
                foreach (H3NetworkBlock h3NetworkBlock1 in rePos)
                {
                    h3NetworkBlock1.LerpPos();
                }
            }
            catch
            {
                
            }
            


        }
        public byte[] PushAllPlayers()
        {
            byte[][] send = new byte[Playerlist.Players.Count][];
            for (int i = 0; i< Playerlist.Players.Count;i++)
            {
                send[i] = PushPlayer((ushort)i);
            }
            int number = 0;
            foreach (byte[] bytes in send)
            {
                number+=bytes.Length;
            }
            byte[] ret = new byte[number+4];
            number = 0;
            byte[] bytes2 = BitConverter.GetBytes(Playerlist.Players.Count);
            ret[number] = bytes2[0];
            ret[number + 1] = bytes2[1];
            ret[number + 2] = bytes2[2];
            ret[number + 3] = bytes2[3];
            number += 4;
            NetworkCompression.WriteArray(send, ret, number);
            return ret;
        }
        public byte[] PushPlayer(ushort player)//blockcount 4 + playerid 2 + {block...(blockid+blockpos+blockrot)(35*block*count)}   blockcount*35+6
        {
            PlayerData playerData=Playerlist.Players[player];

            List<BlockBehaviour> blockBehaviours = new List<BlockBehaviour>();
            try
            {
                foreach (Machine.SimCluster block in playerData.machine.simClusters)
                {
                    try
                    {
                        foreach (BlockBehaviour blockBehaviour in block.Blocks)
                        {
                            if (blockBehaviour.transform.GetComponent<H3ClustersTest>() == null && blockBehaviour != block.Base)
                            {
                                blockBehaviour.transform.gameObject.AddComponent<H3ClustersTest>().ClusterBaseBlock = block.Base;

                            }
                            H3ClustersTest h3ClustersTest = blockBehaviour.transform.GetComponent<H3ClustersTest>();
                            if (h3ClustersTest.send == true)
                            {
                                h3ClustersTest.send = false;
                                if (blockBehaviour.GetComponent<H3NetworkBlock>() == true)
                                {
                                    blockBehaviours.Add(blockBehaviour);
                                }
                               
                            }
                        }
                        if (block.Base.GetComponent<H3NetworkBlock>() == true)
                        {
                            blockBehaviours.Add(block.Base);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
            byte[][] send = new byte[blockBehaviours.Count][];
            for (int i = 0; i < blockBehaviours.Count; i++)
            {

                BlockBehaviour blockBehaviour = blockBehaviours[i];
                try
                {
                    H3NetworkBlock h3NetworkBlock = blockBehaviour.GetComponent<H3NetworkBlock>();
                    int[] ints=Guid2Int(blockBehaviour.BuildingBlock.Guid);
                    byte[] bytes = new byte[16];
                    BitConverter.GetBytes(ints[0]).CopyTo(bytes, 0);
                    BitConverter.GetBytes(ints[1]).CopyTo(bytes, 4);
                    BitConverter.GetBytes(ints[2]).CopyTo(bytes, 8);
                    BitConverter.GetBytes(ints[3]).CopyTo(bytes, 12);
                    byte[] buffer = new byte[bytes.Length+19];
                    int offset = 0;
                    Buffer.BlockCopy(bytes, 0,buffer,0, bytes.Length);
                    offset += bytes.Length;
                    h3NetworkBlock.PushObject(ref offset, buffer);//19
                    send[i] = buffer;//35
                }
                catch
                {

                }
            }
            int number = 0;
            int bytecount=0;
            foreach (byte[] bytes1 in send)
            {
                bytecount += bytes1.Length;
            }
            byte[] sendreturn = new byte[bytecount+6];
            byte[] bytes2 = BitConverter.GetBytes(blockBehaviours.Count);
            sendreturn[number] = bytes2[0];
            sendreturn[number + 1] = bytes2[1];
            sendreturn[number + 2] = bytes2[2];
            sendreturn[number + 3] = bytes2[3];
            number += 4;

            sendreturn[number] = BitConverter.GetBytes(playerData.networkId)[0];
            sendreturn[number+1] = BitConverter.GetBytes(playerData.networkId)[1];
            number += 2;
            
            NetworkCompression.WriteArray(send, sendreturn, number);
            
            return sendreturn;
        }

        public static int[] Guid2Int(Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            var bint1 = BitConverter.ToInt32(b, 4);
            var bint2 = BitConverter.ToInt32(b, 8);
            var bint3 = BitConverter.ToInt32(b, 12);
            return new[] { bint, bint1, bint2, bint3 };
        }

        public static Guid Int2Guid(int value, int value1, int value2, int value3)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            BitConverter.GetBytes(value1).CopyTo(bytes, 4);
            BitConverter.GetBytes(value2).CopyTo(bytes, 8);
            BitConverter.GetBytes(value3).CopyTo(bytes, 12);
            return new Guid(bytes);
        }
    }
}
