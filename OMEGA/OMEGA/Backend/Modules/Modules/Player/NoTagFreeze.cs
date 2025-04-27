using OMEGA.Backend.Modules.System;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class NoTagFreeze : Module
    {
        internal override string Name => "No Tag Freeze";
        internal override string Category => "Player";
        internal override string ToolTip => "Causes you to get no tag freeze";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
                GorillaLocomotion.Player.Instance.disableMovement = false;
        }
    }
}
