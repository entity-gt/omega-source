using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class SpeedBoost : Module
    {
        internal override string Name => "SpeedBoost (Comp)";
        internal override string Category => "Movement";
        internal override string ToolTip => "give you speed";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void OnStateChanged()
        {
            if (State)
            {
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.5f;
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 8.2f;
            }

            else
            {
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
            }
        }
    }
}
