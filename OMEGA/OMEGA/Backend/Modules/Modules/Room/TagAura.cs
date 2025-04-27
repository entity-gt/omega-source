using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class TagAura : Module
    {
        internal override string Name => "Tag Aura";
        internal override string Category => "Room";
        internal override string ToolTip => "Tags everone in your surrounding";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (Librairies.RigManager.self != i)
                    {
                        if (Librairies.RigManager.self.GetComponent<VRRig>().mainSkin.material.name.Contains("infected") && !i.mainSkin.material.name.Contains("infected") && Vector3.Distance(GorillaTagger.Instance.transform.position, i.transform.position) < 10)
                        {
                            Librairies.RigManager.self.enabled = false;
                            Librairies.RigManager.self.transform.position = i.transform.position + new Vector3(0, 0, 0);
                            GorillaTagger.Instance.myVRRig.transform.position = i.transform.position + new Vector3(0, 0, 0);
                            GorillaTagger.Instance.leftHandTransform.position = i.transform.position;
                            GorillaTagger.Instance.rightHandTransform.position = i.transform.position;
                        }

                        else
                        {
                            Librairies.RigManager.self.enabled = true;
                        }
                    }
                }
            }

        }
    }
}
