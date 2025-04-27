using OMEGA.Backend.Modules.System;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class InvisRig : Module
    {
        internal override string Name => "Invis Monkey [A]";
        internal override string Category => "Player";
        internal override string ToolTip => "Makes your body invisible for other people";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool ToggleAntiRepeat = false;

        internal override void Update()
        {
            if (State)
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton && !ToggleAntiRepeat)
                {
                    ToggleAntiRepeat = true;
                    Librairies.RigManager.self.enabled = !Librairies.RigManager.self.enabled;
                    Librairies.RigManager.self.transform.position = new UnityEngine.Vector3(float.MinValue, float.MinValue, float.MinValue);
                }
                else if (!ControllerInputPoller.instance.rightControllerPrimaryButton)
                    ToggleAntiRepeat = false;
            }
        }
    }
}
