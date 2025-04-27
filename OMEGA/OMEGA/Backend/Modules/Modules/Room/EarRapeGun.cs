using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class EarRapeGun : Module
    {
        internal override string Name => "Earrape Gun";
        internal override string Category => "Room";
        internal override string ToolTip => "Plays an annoying sound to a selected player";
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
                    bool shouldEnable = false;
                    if(rig.CheckDistance(RigManager.self.transform.position, 5f))
                    {
                        shouldEnable = true;
                        RigManager.self.enabled = false;
                        RigManager.self.transform.position = rig.transform.position - new Vector3(0f, 3f, 0f);
                    }

                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RigManager.GetPlayerFromRig(rig), 54, false, 0.1f);

                    if(shouldEnable)
                        RigManager.self.enabled = true;
                });
                RigManager.self.enabled = true;
            }
        }
    }
}
