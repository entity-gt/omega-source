using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

namespace OMEGA.Backend.Modules
{
    internal class WaterBending : Module
    {
        internal override string Name => "Water Bending";
        internal override string Category => "Room";
        internal override string ToolTip => "Causes water splash effects";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal float cooldownTimer = Time.time;

        internal override void Update()
        {
            if (State && Time.time > cooldownTimer && ControllerInputPoller.instance.rightGrab)
            {
                Vector3 rightHand = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
                Vector3 leftHand = GorillaTagger.Instance.offlineVRRig.leftHandTransform.position;
                Vector3 pos = Vector3.Lerp(rightHand, leftHand, 0.5f);

                GorillaTagger.Instance.myVRRig.GetView.RPC("RPC_PlaySplashEffect", RpcTarget.All, pos, Quaternion.identity, Vector3.Distance(rightHand, leftHand), Vector3.Distance(rightHand, leftHand), false, true);

                cooldownTimer = Time.time + 0.5f;
            }
        }
    }
}
