using GorillaTagScripts;
using HarmonyLib;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OMEGA.Backend.Modules
{
    internal class BrickSpamGun : Module
    {
        internal override string Name => "Spam Bricks Gun";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Spams pieces at the pointed position";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
                GunLib.EmulateGun(GunLib.ResultType.Position, _pos => BricksHelper.SpawnRandomPiece((Vector3)_pos));
        }

        internal override void OnStateChanged()
        {
            if(State)
            {
                BricksHelper.GetFactory();
                if (BricksHelper.factory == null) State = false;
            }
        }
    }
}