using OMEGA.Backend.Modules.System;
using Photon.Pun;
using GorillaLocomotion;
using UnityEngine.UI;
using UnityEngine;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class SpazHead : Module
    {
        internal override string Name => "Spaz Head";
        internal override string Category => "Player";
        internal override string ToolTip => "Spazzes head aggresivley";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool ToggleAntiRepeat = false;

        internal override void Update()
        {
            if (State)
            {
                GorillaTagger.Instance.offlineVRRig.head.headTransform.transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
            }
        }
    }
}
