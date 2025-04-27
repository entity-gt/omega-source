using GorillaExtensions;
using OMEGA.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

/*
 Dear dev, (Catlicker/Hawa), Please, do NOT try to "optimize" this code.
 If you are suicidal and you don't care of how many time you will get brainfucked by this code, Good luck.
 */

namespace OMEGA.Frontend
{
    internal class BoardModifier
    {
        private static GameObject COCBoard;
        private static TMPro.TextMeshPro COCText;
        private static TMPro.TextMeshPro COCTitle;

        private static bool COCBoardChanged = false;
        private static bool COCTitleChanged = false;
        private static bool COCTextChanged  = false;

        public static void Update()
        {
            if (COCBoardChanged && COCTitleChanged && COCTextChanged) return;

            if (COCTitle == null)
                COCTitle = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConduct").GetComponent<TMPro.TextMeshPro>();
            else
            {
                COCTitleChanged = true;
                COCTitle.text = "OMEGA MENU";
            }

            if (COCBoard == null)
                COCBoard = GameObject.FindObjectsOfType<GameObject>().Where(g => g.GetPath().Equals("/Environment Objects/LocalObjects_Prefab/TreeRoom/UnityTempFile-0e668886bb0df974486eaa852fd0514a (combined by EdMeshCombiner)")).ToList()[0];
            else
            {
                COCBoardChanged = true;
                COCBoard.GetComponent<MeshRenderer>().material.color = new Color32(0, 0, 0, 255);
            }

            if (COCText == null)
                COCText = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COC Text").GetComponent<TMPro.TextMeshPro>();
            else
            {
                COCTextChanged = true;
                COCText.richText = true;
                COCText.text = "Thanks for using Omega Menu!\n\n" + 

                    "Credits:\n" +
                    "<color=#5400FF>Entity</color> - Loader & Template & Mods\n" +
                    "<color=blue>Catlicker</color> - Loader & Mods\n" +
                    "<color=#9900FF>Neble</color> - Mods & GUI\n" +
                    "<color=#5400FF>Zenkizs</color> - Mods & Patches\n" + 
                    "<color=green>kfjfjfj</color> - Mods\n" +
                    "<color=red>Larsl2005</color> - Notification Lib\n\n" +
                    "Special thanks: <color=blue>Hawa</color>, ex co-founder, left the community.";
            }
        }
    }
}
    