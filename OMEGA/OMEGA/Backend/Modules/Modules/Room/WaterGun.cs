using g3;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class WaterGun : Module
    {
        internal override string Name => "Water Gun";
        internal override string Category => "Room";
        internal override string ToolTip => "Water splash effects wherever you shoot at";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;
        internal float cooldownTimer = Time.time;

        internal override void Update()
        {
            if (State)
            {
                GunLib.EmulateGun(GunLib.ResultType.Position, (object _pos) =>
                {
                    Vector3 pos = (Vector3)_pos;
                    Librairies.RigManager.self.enabled = false;
                    Librairies.RigManager.self.transform.position = pos - new Vector3(0, 1, 0);

                    if(Time.time > cooldownTimer + 0.5f)
                    {
                        if(PhotonNetwork.InRoom) GorillaTagger.Instance.myVRRig.GetView.RPC("RPC_PlaySplashEffect", RpcTarget.All, pos, Quaternion.identity, 30f, 30f, false, true);
                        cooldownTimer = Time.time;
                    }

                    Librairies.RigManager.self.enabled = true;
                });
            }
        }
    }
}
