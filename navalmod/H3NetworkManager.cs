using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Navalmod
{
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
                PullPlayer(playerbytes);
                
            }

        }
        public void PullPlayer(byte[] bytes)
        {
            int offset = 0;
            int blocknum = BitConverter.ToInt32(bytes, offset);
            offset += 4;
            ushort playid = BitConverter.ToUInt16(bytes, offset);
            try
            {
                PlayerData playerData = Playerlist.GetPlayer(playid);
                List<BlockBehaviour> blockBehaviours = FindObjectsOfType<BlockBehaviour>().ToList();
                ModConsole.Log(blockBehaviours.Count.ToString());
                try
                {
                    playerData.machine.networkBlocks = new NetworkBlock[0];
                }
                catch
                {

                }
                offset += 2;
                for (int i = 0; i < blocknum; i++)
                {
                    Guid guid=Int2Guid(BitConverter.ToInt32(bytes, offset), BitConverter.ToInt32(bytes, offset+4), BitConverter.ToInt32(bytes, offset+8), BitConverter.ToInt32(bytes, offset+12));
                    offset += 16;
                    for (int n = 0; n < blockBehaviours.Count;n++)
                    {
                        if (blockBehaviours[n].BuildingBlock.Guid == guid && blockBehaviours[n].ParentMachine.PlayerID == playid)
                        {
                            
                            BlockBehaviour blockBehaviour = blockBehaviours[n];
                            blockBehaviours.Remove(blockBehaviour);
                            blockBehaviour.GetComponent<H3NetworkBlock>().PullObject(ref offset , bytes);
                            break;
                        }
                    }

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
            ModConsole.Log("发送成功");
            return ret;
        }
        public byte[] PushPlayer(ushort player)//blockcount 4 + playerid 2 + {block...(blockid+blockpos+blockrot)(35*block*count)}   blockcount*35+6
        {
            PlayerData playerData=Playerlist.Players[player];
            byte[][] send = new byte[playerData.machine.SimulationBlocks.Count][];
            for (int i = 0; i < playerData.machine.SimulationBlocks.Count; i++)
            {
                BlockBehaviour blockBehaviour = playerData.machine.SimulationBlocks[i];
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
            byte[] bytes2 = BitConverter.GetBytes(playerData.machine.SimulationBlocks.Count);
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
