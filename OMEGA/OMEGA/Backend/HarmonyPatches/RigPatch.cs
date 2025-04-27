using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.HarmonyPatches
{
    [HarmonyPatch(typeof(VRRig), "OnDisable")]
    internal class RigPatch : MonoBehaviour
    {
        public static bool Prefix(VRRig __instance)
        {
            return !(__instance == Librairies.RigManager.self);
        }
    }
}