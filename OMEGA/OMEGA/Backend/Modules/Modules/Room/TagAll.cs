using GorillaGameModes;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class TagAll : Module
    {
        internal override string Name => "Tag All";
        internal override string Category => "Room";
        internal override string ToolTip => "Tags everone in your room";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal VRRig lockedPlayer = null;

        internal override void Update()
        {
            if(State)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != Librairies.RigManager.self)
                    {
                        if (Librairies.RigManager.self.mainSkin.material.name.Contains("infected") && !vrrig.mainSkin.material.name.Contains("infected"))
                        {
                            Librairies.RigManager.self.enabled = false;
                            Librairies.RigManager.self.transform.position = vrrig.transform.position;
                            Photon.Realtime.Player player = Librairies.RigManager.GetPlayerFromRig(vrrig);

                            GameMode.ReportTag(player);
                        }

                        if (vrrig.mainSkin.material.name.Contains("infected"))
                        {
                            Librairies.RigManager.self.enabled = true;
                        }
                    }
                }

                if (GorillaParent.instance.vrrigs.All(r => r.mainSkin.material.name.Contains("infected")))
                    State = false;
            }
        }
    }
}
