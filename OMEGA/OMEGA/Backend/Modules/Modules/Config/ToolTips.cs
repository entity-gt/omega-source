using OMEGA.Backend.Modules.System.Config;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OMEGA.Frontend;

namespace OMEGA.Backend.Modules
{
    internal class ToolTips : Module
    {
        internal override string Name => "ToolTips";
        internal override string Category => "Config";
        internal override string ToolTip => "Shows the tooltip buttons";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = true;

        internal bool doOnceUpdateConfig = false;

        internal override void Update()
        {
            if (doOnceUpdateConfig)
                return;

            State = Config.showTooltips.Value;
            doOnceUpdateConfig = true;
        }

        internal override void OnStateChanged()
        {
            Config.showTooltips.Value = State;
            WristMenu.RefreshButtons();
            Config.SaveConfig();
        }
    }
}
