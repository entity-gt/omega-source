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
    internal class UnloadVirtStumpMapGun : Module
    {
        internal override string Name => "Unload Map Gun";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Unloads virtual stump map for the pointed player";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if(!State) return;

            if (!GlobalModCache.GetCacheMember<PhotonView>("ModIOSerializerView", out PhotonView view))
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
                    State = false;
                    return;
                }

                Type type = typeof(ModIOModDetailsScreen).Assembly.GetType("GorillaSerializer");
                view = (PhotonView)type.GetField("photonView", PhotonUtils.InstanceNonPublic).GetValue(networkObject);

                if (view == null)
                {
                    NotifiLib.Instance.SendNotification("UNLOAD", "You need to be in a room and in the virtual stump!", "red");
                    State = false;
                    return;
                }

                GlobalModCache.SetCacheMember("ModIOSerializerView", view);
            }

            GunLib.EmulateGun(GunLib.ResultType.Player, _player =>
            {
                Player player = (Player)_player;
                view.RPC("UnloadMapRPC", player);
            });
        }
    }
}