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
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnLeftRoom")]
    internal class DisconnectPatch : MonoBehaviour
    {
        public static void Prefix()
        {
            Globals.LastRoomJoined = string.Empty;
        }
    }
}
