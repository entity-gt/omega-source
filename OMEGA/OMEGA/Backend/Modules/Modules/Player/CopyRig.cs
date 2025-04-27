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
    internal class copyrig : Module
    {
        internal override string Name => "Copy Rig Gun";
        internal override string Category => "Player";
        internal override string ToolTip => "Points your rig wherver you shoot";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                GunLib.EmulateGun(GunLib.ResultType.VRRig, (object _rig) =>
                {
                    VRRig rig = (VRRig)_rig;
                    RigManager.self.transform.position = rig.transform.position + new Vector3(0, 0, 0);
                    RigManager.self.transform.position = rig.transform.position + new Vector3(0, 0, 0);
                    RigManager.self.transform.rotation = rig.transform.rotation;
                });

                if(!GunLib.IsPointing) 
                    RigManager.self.enabled = true;
            }
        }
        internal override void OnStateChanged()
        {
            if (!State)
                RigManager.self.enabled = true;
        }
    }
}
