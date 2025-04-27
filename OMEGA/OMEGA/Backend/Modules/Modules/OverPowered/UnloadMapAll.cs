using GorillaExtensions;
using GorillaGameModes;
using GorillaNetworking;
using GorillaTagScripts;
using GorillaTagScripts.ModIO;
using HarmonyLib;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OMEGA.Backend.Modules
{
    internal class UnloadVirtStumpMapAll : Module
    {
        internal override string Name => "Unload Map";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Unloads virtual stump map causing everyone to fall";
        internal override bool Pinned => false;
        internal override bool IsTogglable => false;
        internal override bool State { get; set; } = false;
        internal override void OnStateChanged()
        {
            ModIOModDetailsScreen screen = GameObject.FindObjectOfType<ModIOModDetailsScreen>();
            if (screen == null)
            {
                NotifiLib.Instance.SendNotification("UNLOAD", "You need to be in a room and in the virtual stump!", "red");
                State = false;
                return;
            }

            object networkObject = typeof(ModIOModDetailsScreen).GetField("networkObject", PhotonUtils.InstanceNonPublic).GetValue(screen);
            if (networkObject == null)
            {
                NotifiLib.Instance.SendNotification("UNLOAD", "You need to be in a room and in the virtual stump!", "red");
                return;
            }
            
            Type type = typeof(ModIOModDetailsScreen).Assembly.GetType("GorillaSerializer");
            PhotonView view = (PhotonView) type.GetField("photonView", PhotonUtils.InstanceNonPublic).GetValue(networkObject);
            if (view == null)
            {
                NotifiLib.Instance.SendNotification("UNLOAD", "You need to be in a room and in the virtual stump!", "red");
                return;
            }

            view.RPC("UnloadMapRPC", RpcTarget.Others);
        }
    }
}