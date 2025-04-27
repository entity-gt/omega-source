using HarmonyLib;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Linq;

namespace OMEGA.Backend.Modules
{
    internal class AntiModerator : Module
    {
        internal override string Name => "Anti Moderator";
        internal override string Category => "Room";
        internal override string ToolTip => "Makes you leave when a moderator is in the room";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                foreach(VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (rig == RigManager.self) continue;

                    if(rig.IsItemAllowed("LBAAK.") || rig.IsItemAllowed("LBAAD."))
                    {
                        NotifiLib.Instance.SendNotification("ANTIMOD", "Moderator found! Leaving", "red");
                        PhotonNetwork.Disconnect();
                    }
                }
            }
        }
    }
}