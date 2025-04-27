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
    internal class TeleportGun : Module
    {
        internal override string Name => "Teleport Gun";
        internal override string Category => "Movement";
        internal override string ToolTip => "Teleports you wherever you shoot";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool IsTeleporting = false;

        internal override void Update()
        {
            if (State)
            {
                GunLib.EmulateGun(GunLib.ResultType.Position, (object pos) =>
                {
                    if(!IsTeleporting)
                    {
                        GorillaTagger.Instance.transform.position = (Vector3)pos;
                        IsTeleporting = true;
                    }
                });
                

                if(!GunLib.IsShooting)
                    IsTeleporting = false;
            }
        }
    }
}
