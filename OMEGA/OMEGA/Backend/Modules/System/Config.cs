using BepInEx.Configuration;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace OMEGA.Backend.Modules.System.Config
{
    internal static class Config
    {
        public static ConfigFile configFile = new ConfigFile("Omega/OmegaConfig.cfg", false);
        public static void SaveConfig() => configFile.Save();

        public static ConfigEntry<bool> showTooltips;
        public static ConfigEntry<bool> logIDs;

        public static void InitConfig()
        {
            showTooltips = configFile.Bind<bool>("Config", "ToolTips", true);
            logIDs = configFile.Bind<bool>("Config", "LogIDs", false);
            SaveConfig();
        }
    }
}
