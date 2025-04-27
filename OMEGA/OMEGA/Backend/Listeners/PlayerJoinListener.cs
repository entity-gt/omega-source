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
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerEnteredRoom")]
    internal class PlayerJoinPatch : MonoBehaviour
    {
        public static void Prefix(Player newPlayer)
        {
            if (OldPlayer == newPlayer) return;

            NotifiLib.Instance.SendNotification("JOIN", "Player <color=yellow>" + newPlayer.NickName + "</color> joined current room", "grey");

            if (OMEGA.Backend.Modules.System.Config.Config.logIDs.Value)
            {
                if (!Directory.Exists("Omega")) Directory.CreateDirectory("Omega");
                if (!File.Exists("Omega/PlayerLogs.txt")) File.Create("Omega/PlayerLogs.txt");
                File.AppendAllText("Omega/PlayerLogs.txt", "PLAYER " + newPlayer.NickName + " (" + newPlayer.UserId + ") joined. " + PhotonNetwork.CurrentRoom + Environment.NewLine);
            }

            OldPlayer = newPlayer;
        }

        public static Player OldPlayer;
    }
}
