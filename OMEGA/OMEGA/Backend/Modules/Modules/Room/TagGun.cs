using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class TagGun : Module
    {
        internal override string Name => "Tag Gun";
        internal override string Category => "Room";
        internal override string ToolTip => "Tags Whoever You Shoot.";
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
                    Librairies.RigManager.self.enabled = false;
                    Librairies.RigManager.self.transform.position = rig.transform.position + new Vector3(0, 0, 0);
                    Librairies.RigManager.self.transform.position = rig.transform.position + new Vector3(0, 0, 0);
                    GorillaTagger.Instance.leftHandTransform.position = rig.transform.position;
                    GorillaTagger.Instance.rightHandTransform.position = rig.transform.position;
                    Librairies.RigManager.self.enabled = true;

                });
            }
        }
    }
}
