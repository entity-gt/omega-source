using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace OMEGA.Backend.HarmonyPatches
{
    [HarmonyPatch(typeof(GameObject))]
    [HarmonyPatch("CreatePrimitive", 0)]
    internal class ShaderFix : MonoBehaviour
    {
        private static void Postfix(GameObject __result)
        {
            Renderer renderer = __result.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.shader = Shader.Find("GorillaTag/UberShader");
            }
        }
    }
}
