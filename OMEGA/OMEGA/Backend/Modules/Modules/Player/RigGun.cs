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
    internal class RigGun : Module
    {
        internal override string Name => "Rig Gun";
        internal override string Category => "Player";
        internal override string ToolTip => "Points your rig wherver you shoot";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    Librairies.RigManager.self.enabled = false;
                    GunLib.EmulateGun(GunLib.ResultType.Position, (object pos) =>
                    {
                        Librairies.RigManager.self.enabled = false;
                        Librairies.RigManager.self.transform.position = (Vector3)pos;
                    });
                }
                else
                {
                    Librairies.RigManager.self.enabled = true;
                }
            }
        }
    }
}
