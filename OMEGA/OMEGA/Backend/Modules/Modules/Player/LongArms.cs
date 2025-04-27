using OMEGA.Backend.Modules.System;
using Photon.Pun;
using GorillaLocomotion;
using UnityEngine.UI;
using UnityEngine;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class LongArms : Module
    {
        internal override string Name => "Long Arms [T]";
        internal override string Category => "Player";
        internal override string ToolTip => "Give you longer/shorter arms with triggers";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool ToggleAntiRepeat = false;
        public static float CurrentArmSize = 1;
        internal override void Update()
        {
            if (State)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.8f)
                    CurrentArmSize += 0.05f;
                if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.8f)
                    CurrentArmSize -= 0.05f;
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(CurrentArmSize, CurrentArmSize, CurrentArmSize);
            }

            else
            {
                CurrentArmSize = 1;
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
