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
    internal class GhostRig : Module
    {
        internal override string Name => "Ghost Monkey [A]";
        internal override string Category => "Player";
        internal override string ToolTip => "Makes your body frozen for other people";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool ToggleAntiRepeat = false;

        internal override void Update()
        {
            if (State)
            {
                if(ControllerInputPoller.instance.rightControllerPrimaryButton && !ToggleAntiRepeat)
                {
                    ToggleAntiRepeat = true;
                    Librairies.RigManager.self.enabled = !Librairies.RigManager.self.enabled;
                }
                
                else if(!ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    ToggleAntiRepeat = false;
                }
            }
        }
    }
}
