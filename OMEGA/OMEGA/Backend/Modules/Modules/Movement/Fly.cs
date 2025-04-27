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
    internal class Fly : Module
    {
        internal override string Name => "Fly [B]";
        internal override string Category => "Movement";
        internal override bool Pinned => true;
        internal override string ToolTip => "Makes you fly when pressing B button";
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State && ControllerInputPoller.instance.rightControllerSecondaryButton)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.SetVelocity(new Vector3(0f, 0f, 0f));
            }
        }
    }
}
