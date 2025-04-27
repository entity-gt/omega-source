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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using Player = Photon.Realtime.Player;

namespace OMEGA.Backend.Listeners
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnJoinedRoom")]
    internal class JoinPatch : MonoBehaviour
    {
        public static void Prefix()
        {

            if (PhotonNetwork.CurrentRoom.Name + Regex.Replace(PhotonNetwork.CloudRegion, "[^a-zA-Z0-9]", "") == Globals.LastRoomJoined) return;
            NotifiLib.Instance.SendNotification("JOIN", "Joined room: " + PhotonNetwork.CurrentRoom.Name, "grey");

            Globals.LastRoomJoined = PhotonNetwork.CurrentRoom.Name + Regex.Replace(PhotonNetwork.CloudRegion, "[^a-zA-Z0-9]", "");
        }
    }
}
