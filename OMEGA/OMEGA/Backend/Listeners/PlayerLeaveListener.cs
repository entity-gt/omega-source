using HarmonyLib;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.Categories;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Player = Photon.Realtime.Player;

namespace OMEGA.Backend.Listeners
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    internal class PlayerLeavePatch : MonoBehaviour
    {
        public static void Prefix(Player otherPlayer)
        {
            if (OldOtherPlayer == otherPlayer) return;

            NotifiLib.Instance.SendNotification("LEAVE", "Player <color=yellow>" + otherPlayer.NickName + "</color> left current room", "grey");
                
            if (OMEGA.Backend.Modules.System.Config.Config.logIDs.Value)
            {
                if (!Directory.Exists("Omega")) Directory.CreateDirectory("Omega");
                if (!File.Exists("Omega/PlayerLogs.txt")) File.Create("Omega/PlayerLogs.txt");
                File.AppendAllText("Omega/PlayerLogs.txt", "PLAYER " + otherPlayer.NickName + " (" + otherPlayer.UserId + ") left. " + PhotonNetwork.CurrentRoom + Environment.NewLine);
            }

            OldOtherPlayer = otherPlayer;
        }

        public static Player OldOtherPlayer;
    }
}
