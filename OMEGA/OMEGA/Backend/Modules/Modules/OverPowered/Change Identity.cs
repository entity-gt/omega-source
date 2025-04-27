using GorillaExtensions;
using GorillaGameModes;
using GorillaNetworking;
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
    internal class ChangeIdentity : Module
    {
        internal override string Name => "Change Identity";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Changes your identity on the leaderboard if needed.";
        internal override bool Pinned => false;
        internal override bool IsTogglable => false;
        internal override bool State { get; set; } = false;

        internal float progress = 0f;

        internal override void Update()
        {
            if (State)
            {
                string[] al = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                string name = string.Empty;
                for (int i = 0; i < 10; i++)
                    name += al[UnityEngine.Random.Range(0, al.Length)];

                NetworkSystem.Instance.SetMyNickName(name);
                PlayerPrefs.SetString("playerName", name);
                PlayerPrefs.Save();
                GorillaComputer.instance.savedName = name;
                GorillaComputer.instance.offlineVRRigNametagText.text = name;
                GorillaComputer.instance.currentName = name;
                PhotonNetwork.LocalPlayer.NickName = name;
            }
        }
    }
}