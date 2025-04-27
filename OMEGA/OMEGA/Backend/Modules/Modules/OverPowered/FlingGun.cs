using GorillaExtensions;
using GorillaGameModes;
using GorillaTagScripts;
using HarmonyLib;
using OculusSampleFramework;
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
    internal class FlingGun : Module
    {
        internal override string Name => "Fling Gun";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Flings people, guardian is needed.";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal float progress = 0f;

        internal override void Update()
        {
            if (State)
            {
                if (!State || !PhotonNetwork.InRoom) return;

                if (!PhotonUtils.GameModeNetworking) PhotonUtils.InitNetworking();

                GorillaGuardianManager manager = (GorillaGuardianManager)GorillaGuardianManager.instance;
                if (!manager.IsPlayerGuardian(PhotonNetwork.LocalPlayer)) return;

                GunLib.EmulateGun(GunLib.ResultType.Player, (_player) =>
                {
                    Player player = (Player)_player;

                    Console.WriteLine(PhotonUtils.GameModeNetworking.ViewID);
                    Vector3 pos = RigManager.GetRigFromPlayer(player).transform.position;
                    Vector3 vel = new Vector3(0f, 20f, 0f);

                    if (RigManager.self.enabled) RigManager.self.enabled = false;
                    RigManager.self.transform.position = pos;
                    RigManager.self.leftHand.rigTarget.transform.position = pos;
                    RigManager.self.rightHand.rigTarget.transform.position = pos;

                    PhotonUtils.GameModeNetworking.SendRPC("GuardianLaunchPlayer", player, vel);
                });


                if (GunLib.IsShooting) RigManager.self.enabled = true;
            }
        }
    }
}