using GorillaExtensions;
using GorillaTagScripts;
using HarmonyLib;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Pathfinding;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OMEGA.Backend.Modules
{
    internal class BrickAura : Module
    {
        internal override string Name => "Brick Aura";
        internal override string Category => "Overpowered";
        internal override string ToolTip => "Spams pieces at your position";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal float progress = 0f;

        internal override void Update()
        {
            if (State)
            {
                Vector3 pos = RigManager.self.transform.position;
                Vector2 circle = Formulas.Circle(1f, progress);
                pos.x += circle.x;
                pos.z += circle.y;

                BricksHelper.SpawnRandomPiece(pos);
                progress += 30f;
                progress %= 360f;
            }
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