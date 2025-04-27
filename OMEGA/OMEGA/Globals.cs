using OMEGA.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace OMEGA.Backend
{
    internal enum ProductEnvironment
    {
        Development,
        Production
    }

    internal static class Globals
    {
        internal static string MenuTitle = "ΩMΞGΛ";
        internal static string MenuVersion = "EARLY ACCESS 1";
        internal static int LocalHandTapButtonIndex = 93;
        internal static string LastRoomJoined;
        internal static Font Consolas = Font.CreateDynamicFontFromOSFont("Consolas", 6);

        internal static ProductEnvironment environment = ProductEnvironment.Production; // TODO: Change to prod

        internal static Color _primaryColor = Color.HSVToRGB(0.75f, 1f, 1f);
        internal static Color _secondaryColor = Color.HSVToRGB(0.71f, 1f, 1f);

        internal static Color PrimaryColor => isRGB ? WristMenu.rgbColorSlow : _primaryColor;
        internal static Color SecondaryColor => isRGB ? WristMenu.rgbColorSlow : _secondaryColor;

        internal static bool isRGB = false;

        internal static Color GetMainThemeColor() => isRGB ? WristMenu.rgbColorSlow : Color.Lerp(PrimaryColor, SecondaryColor, Mathf.PingPong(Time.time, 1f));
    }
}
