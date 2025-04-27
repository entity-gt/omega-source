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
    internal class SpeedBoostRG : Module
    {
        internal override string Name => "SpeedBoost (RG)";
        internal override string Category => "Movement";
        internal override string ToolTip => "Gives you speed while holding RG";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State && ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.7f;
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 8.5f;
            }

            else
            {
                GorillaLocomotion.Player.Instance.jumpMultiplier = 1.1f;
                GorillaLocomotion.Player.Instance.maxJumpSpeed = 6.5f;
            }
        }
    }
}
