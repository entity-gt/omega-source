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
    internal class SlapEffectGun : Module
    {
        internal override string Name => "Slap Effect Gun";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Spams slap effect, guardian is needed.";
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

                GunLib.EmulateGun(GunLib.ResultType.Position, (_pos) =>
                {
                    Vector3 pos = (Vector3)_pos;
                    PhotonUtils.GameModeNetworking.SendRPC("ShowSlapEffects", RpcTarget.All, pos, (RigManager.self.transform.position - pos).normalized);
                });

                if (GunLib.IsShooting) RigManager.self.enabled = true;
            }
        }
    }
}