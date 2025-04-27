using ExitGames.Client.Photon;
using GorillaGameModes;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Reflection;
using Module = OMEGA.Backend.Modules.System.Module;

namespace OMEGA.Backend.Librairies
{
    internal static class PhotonUtils
    {
        public const BindingFlags InstanceNonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

        public static NetworkView GameModeNetworking;
        public static Queue<StreamBuffer> MessageBufferPool;
        public static bool initialized = false;

        public static void InitNetworking()
        {
            if (!PhotonNetwork.InRoom) return;

            Type type = Assembly.GetAssembly(typeof(GameMode)).GetType("GorillaWrappedSerializer");
            object activeNetworkHandler = typeof(GameMode).GetField("activeNetworkHandler", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            GameModeNetworking = (NetworkView)type.GetField("netView", InstanceNonPublic).GetValue(activeNetworkHandler);

            MessageBufferPool = (Queue<StreamBuffer>)typeof(PeerBase).GetField("MessageBufferPool", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);

            initialized = true;
        }

        public static bool CheckMaster(Module _this)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                NotifiLib.Instance.SendNotification("MODULE", "You are not master client!", "red");
                _this.State = false;
            }
            
            return PhotonNetwork.IsMasterClient;
        }
    }
}
