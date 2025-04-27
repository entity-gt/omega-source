using OMEGA.Backend.Modules.System.Config;
using OMEGA.Backend.Modules.System;
using OMEGA.Frontend;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class LogPlayerIDs : Module
    {
        internal override string Name => "Log PlayerIds";
        internal override string Category => "Config";
        internal override string ToolTip => "Logs player IDs in a text file on join";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal bool doOnceUpdateConfig = false;

        internal override void Update()
        {
            if (doOnceUpdateConfig)
                return;
            State = Config.logIDs.Value;
            doOnceUpdateConfig = true;
        }

        internal override void OnStateChanged()
        {
            Config.logIDs.Value = State;
            Config.SaveConfig();
        }
    }
}
