using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static gs.PointSetHashtable;

namespace OMEGA.Backend.Modules
{
    internal class GiveRig : Module
    {
        internal override string Name => "Give Rig Gun";
        internal override string Category => "Player";
        internal override string ToolTip => "Gives your rig to the player you shoot at";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool ToggleAntiRepeat = false;

        internal override void Update()
        {
            if (State)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    GunLib.EmulateGun(GunLib.ResultType.Player, (object _player) =>
                    {
                        Photon.Realtime.Player player = (Photon.Realtime.Player)_player;

                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = RigManager.GetRigFromPlayer(player).leftHandTransform.position;
                    });
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = true;
                }
            }
        }
    }
}
