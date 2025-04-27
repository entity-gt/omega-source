using OMEGA.Backend.Modules.System;
using Photon.Pun;

namespace OMEGA.Backend.Modules
{
    internal class EarRapeAll : Module
    {
        internal override string Name => "Earrape All";
        internal override string Category => "Room";
        internal override string ToolTip => "Plays an annoying sound to everyone";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, 54, false, 0.1f);
        }
    }
}