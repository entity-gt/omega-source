using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class ChamsSelf : Module
    {
        internal override string Name => "Chams Self";
        internal override string Category => "Visuals";
        internal override string ToolTip => "Adds a cham to self";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                Librairies.RigManager.self.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                Librairies.RigManager.self.mainSkin.material.color = Globals.GetMainThemeColor();
            }
        }

        internal override void OnStateChanged()
        {
            if(!State)
            {
                Librairies.RigManager.self.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                Librairies.RigManager.self.mainSkin.material.color = Librairies.RigManager.self.playerColor;
            }
        }
    }
}
