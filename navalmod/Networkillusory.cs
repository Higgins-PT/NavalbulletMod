using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Modding;
using Modding.Blocks;
using UnityEngine;
using UnityEngine.Rendering;
using System.Reflection;
using UnityEngine.SocialPlatforms;

namespace Navalmod
{
    public class Networkillusory:MonoBehaviour
    {
        public void RefreshPlay()
        {
            foreach (PlayerData playerData in Playerlist.Players)
            {
                ModConsole.Log(playerData.networkId.ToString());
                if (playerData.networkId != 0) {
                    base.StartCoroutine(this.PlayerJoin(playerData, false));
                }
            }
        }
        public IEnumerator PlayerJoin(PlayerData targetPlayer, bool spectator)
        {
            NetworkAuxAddPiece networkAuxAddPiece = UnityEngine.Object.FindObjectOfType<NetworkAuxAddPiece>();
            NetworkAddPiece networkAddPiece = UnityEngine.Object.FindObjectOfType<NetworkAddPiece>();
            networkAuxAddPiece.ToggleSpectator(targetPlayer.networkId, new byte[2]);
            targetPlayer.inLocalSim = !StatMaster.Mode.LevelEditor.clientSimControl;
            
            List<byte[]> dataList = new List<byte[]>();
            int totalLength = 0;
            byte playerCount = 0;
            int offset = 0;
            for (int i = 0; i < Playerlist.Players.Count; i++)
            {
                PlayerData player = Playerlist.Players[i];
                playerCount += 1;
                byte[] playerNameBytes = Encoding.UTF8.GetBytes(player.name);
                int headerSize = 5 + playerNameBytes.Length;
                byte[] idBytes = null;
                byte[] platformUserNameBytes = null;
                PlayerPlatform platform = PlayerPlatform.Unknown;
                if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
                {
                    headerSize++;
                    platform = player.platform;
                    if (platform != PlayerPlatform.Unknown)
                    {
                        platformUserNameBytes = Encoding.UTF8.GetBytes(player.platformUserName);
                        headerSize += 1 + platformUserNameBytes.Length;
                        idBytes = BitConverter.GetBytes(player.platformUserId);
                        headerSize += idBytes.Length;
                    }
                }
                offset = 0;
                byte[] data;
                if (player.isSpectator)
                {
                    data = new byte[headerSize];
                    NetworkCompression.WriteUInt16(player.networkId, data, offset);
                    offset += 2;
                    NetworkCompression.WriteUInt16((ushort)playerNameBytes.Length, data, offset);
                    offset += 2;
                    Buffer.BlockCopy(playerNameBytes, 0, data, offset, playerNameBytes.Length);
                    offset += playerNameBytes.Length;
                    if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
                    {
                        byte[] array = data;
                        int num;
                        offset = (num = offset) + 1;
                        array[num] = (byte)platform;
                        if (platform != PlayerPlatform.Unknown)
                        {
                            byte[] array2 = data;
                            offset = (num = offset) + 1;
                            array2[num] = (byte)platformUserNameBytes.Length;
                            Buffer.BlockCopy(platformUserNameBytes, 0, data, offset, platformUserNameBytes.Length);
                            offset += platformUserNameBytes.Length;
                            Buffer.BlockCopy(idBytes, 0, data, offset, idBytes.Length);
                            offset += idBytes.Length;
                        }
                    }
                    data[offset] = (byte)(1 | ((!player.inLocalSim) ? 0 : 2));
                    offset++;
                }
                else
                {
                    ServerMachine machine = player.machine;
                    PlayerBuildZone buildZone = player.buildZone;
                    bool isSim = machine.isSimulating;
                    bool hasSimData = isSim && machine.isReady;
                    bool includeClusters = targetPlayer.networkId != machine.PlayerID && !machine.analyzing;
                    byte[] machineData = machine.Encode(hasSimData, ref includeClusters);
                    bool hasSpawnZone = buildZone.hasSpawnZone;
                    data = new byte[headerSize + 1 + 1 + ((!hasSpawnZone) ? 0 : LevelEntity.ID_LENGTH) + 12 + 16 + machineData.Length];
                    NetworkCompression.WriteUInt16(player.networkId, data, offset);
                    offset += 2;
                    NetworkCompression.WriteUInt16((ushort)playerNameBytes.Length, data, offset);
                    offset += 2;
                    Buffer.BlockCopy(playerNameBytes, 0, data, offset, playerNameBytes.Length);
                    offset += playerNameBytes.Length;
                    if (OptionsMaster.networkType == PlayerNetworkType.Playfab)
                    {
                        byte[] array3 = data;
                        int num;
                        offset = (num = offset) + 1;
                        array3[num] = (byte)platform;
                        if (platform != PlayerPlatform.Unknown)
                        {
                            byte[] array4 = data;
                            offset = (num = offset) + 1;
                            array4[num] = (byte)platformUserNameBytes.Length;
                            Buffer.BlockCopy(platformUserNameBytes, 0, data, offset, platformUserNameBytes.Length);
                            offset += platformUserNameBytes.Length;
                            Buffer.BlockCopy(idBytes, 0, data, offset, idBytes.Length);
                            offset += idBytes.Length;
                        }
                    }
                    bool boundsEnabled = ((!machine.isLocalMachine) ? buildZone.boundingBoxController.boundingEnabled : StatMaster.Bounding.Enabled);
                    data[offset] = (byte)(((!player.inLocalSim) ? 0 : 2) | ((!isSim) ? 0 : 4) | ((!includeClusters) ? 0 : 8) | ((!boundsEnabled) ? 0 : 16) | ((!hasSpawnZone) ? 0 : 32) | ((!player.voteState) ? 0 : 64));
                    offset++;
                    data[offset] = (byte)machine.Session;
                    offset++;
                    data[offset] = machine.GetGodModes();
                    offset++;
                    if (hasSpawnZone)
                    {
                        byte[] spawnIdBytes = buildZone.spawnZone.GetIdentifierBytes();
                        Buffer.BlockCopy(spawnIdBytes, 0, data, offset, spawnIdBytes.Length);
                        offset += LevelEntity.ID_LENGTH;
                    }
                    NetworkCompression.PackVector(buildZone.transform.position, data, offset);
                    offset += 12;
                    NetworkCompression.PackQuaternion(buildZone.transform.rotation, data, offset);
                    offset += 16;
                    Buffer.BlockCopy(machineData, 0, data, offset, machineData.Length);
                    offset += machineData.Length;
                }
                dataList.Add(data);
                totalLength += offset;
            }
           
            byte[] levelData = UnityEngine.Object.FindObjectOfType<CustomLevel>().Encode(UnityEngine.Object.FindObjectOfType<LevelEditor>().Settings.Name);
            bool levelSim = StatMaster.levelSimulating && !StatMaster.isLocalSim;
            int levelSimLength = ((!levelSim) ? 0 : networkAddPiece.dataManager.GetSimFrame());
            byte[] timeScaleBytes = networkAddPiece.GetTimeScale(networkAddPiece.lastTimeScale);
            byte[] serverSettings = NetworkScene.ServerSettings.Encode();
            byte[] autoTimeScaleBytes = ((!levelSim) ? new byte[0] : networkAddPiece.GetTimeScale(networkAddPiece.lastAutoTimeScale));
            byte[] completionData = ((!levelSim) ? new byte[0] : new byte[0]);
            int completionLength = completionData.Length;
            byte[] teamWinData = ((!levelSim) ? new byte[0] : new byte[0]);
            int teamWinDataLength = teamWinData.Length;
            byte[] stateBytes = new byte[timeScaleBytes.Length + serverSettings.Length + 1 + 1 + autoTimeScaleBytes.Length + completionLength + teamWinDataLength + 4 + levelData.Length + levelSimLength + totalLength];
            offset = 0;
            Buffer.BlockCopy(timeScaleBytes, 0, stateBytes, offset, timeScaleBytes.Length);
            offset += timeScaleBytes.Length;
            Buffer.BlockCopy(serverSettings, 0, stateBytes, offset, serverSettings.Length);
            offset += serverSettings.Length;
            stateBytes[offset] = (byte)UnityEngine.Object.FindObjectOfType<CustomLevel>().Session;
            offset++;
            stateBytes[offset] = (byte)(((!levelSim) ? 0 : 1) | ((!StatMaster.Mode.LevelEditor.clientSimControl) ? 0 : 2) | ((int)playerCount << 2));
            offset++;
            if (levelSim)
            {
                Buffer.BlockCopy(autoTimeScaleBytes, 0, stateBytes, offset, autoTimeScaleBytes.Length);
                offset += autoTimeScaleBytes.Length;
                Buffer.BlockCopy(completionData, 0, stateBytes, offset, completionLength);
                offset += completionLength;
                Buffer.BlockCopy(teamWinData, 0, stateBytes, offset, teamWinDataLength);
                offset += teamWinDataLength;
            }
            NetworkCompression.WriteUInt((uint)levelData.Length, false, stateBytes, offset);
            offset += 4;
            Buffer.BlockCopy(levelData, 0, stateBytes, offset, levelData.Length);
            offset += levelData.Length;
            if (levelSim)
            {
                networkAddPiece.dataManager.WriteSimFrame(stateBytes, offset);
                offset += levelSimLength;
            }
            NetworkCompression.WriteArray(dataList, stateBytes, offset);
            UnityEngine.Object.FindObjectOfType<NetworkAuxAddPiece>().SendFragmentedPlayerMessage(targetPlayer.networkId, RPCMessageType.ReceiveLevelState, CLZF2.Compress(stateBytes));
           
            yield break;
        }
    }
}
