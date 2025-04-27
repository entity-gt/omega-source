using ExitGames.Client.Photon;
using Fusion;
using GorillaExtensions;
using GorillaTagScripts;
using GorillaTagScripts.ModIO;
using GorillaTagScripts.UI.ModIO;
using HarmonyLib;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using Module = OMEGA.Backend.Modules.System.Module;
using Random = UnityEngine.Random;

namespace OMEGA.Backend.Modules
{
    internal class AlwaysGuardian : Module
    {
        internal override string Name => "Always Guardian";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Gets guradian in guardian gamemode";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal float FlingGrabThrottle = Time.time;

        internal override void Update()
        {
            if (!State || !PhotonNetwork.InRoom) return;
            GorillaGuardianManager manager = (GorillaGuardianManager)GorillaGuardianManager.instance;

            if (!GlobalModCache.GetCacheMember<TappableGuardianIdol[]>("Idols", out TappableGuardianIdol[] idols))
            {
                idols = UnityEngine.Object.FindObjectsOfType<TappableGuardianIdol>();
                GlobalModCache.SetCacheMember<TappableGuardianIdol[]>("Idols", idols);
            }

            if(idols.All(_i => !_i.enabled))
            {
                idols = UnityEngine.Object.FindObjectsOfType<TappableGuardianIdol>();
                GlobalModCache.SetCacheMember<TappableGuardianIdol[]>("Idols", idols);
            }

            if (manager.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer)) { RigManager.self.enabled = true; return; }
            TappableGuardianIdol enabledIdol = idols.First(_idol => _idol.enabled);

            if (enabledIdol == null) { RigManager.self.enabled = true; return; }

            if (PhotonNetwork.IsMasterClient)
            {
                foreach (GorillaGuardianZoneManager g in GorillaGuardianZoneManager.zoneManagers)
                    if (manager.enabled)
                        g.SetGuardian(NetworkSystem.Instance.LocalPlayer);
                return;
            }

            if (Time.time < FlingGrabThrottle) return;

            if (RigManager.self.enabled) RigManager.self.enabled = false;
            RigManager.self.transform.position = enabledIdol.transform.position;
            RigManager.self.leftHand.rigTarget.transform.position = enabledIdol.transform.position;
            RigManager.self.rightHand.rigTarget.transform.position = enabledIdol.transform.position;
            enabledIdol.manager.photonView.RPC("SendOnTapRPC", RpcTarget.All, enabledIdol.tappableId, Random.Range(0.2f, 0.4f));
            FlingGrabThrottle = Time.time + 0.1f;
        }

        internal override void OnStateChanged()
        {
            if(!State)
                RigManager.self.enabled = true;
        }
    }
}