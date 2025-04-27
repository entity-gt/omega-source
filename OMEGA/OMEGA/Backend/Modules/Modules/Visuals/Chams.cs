using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class Chams : Module
    {
        internal override string Name => "Chams";
        internal override string Category => "Visuals";
        internal override string ToolTip => "Adds a cham to everyone";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State & PhotonNetwork.InRoom) 
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if(i != Librairies.RigManager.self)
                    {
                        i.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        if (i.mainSkin.material.name.Contains("infected"))
                        {
                            i.mainSkin.material.color = new Color32(255, 0, 0, 200);
                        }
                        else
                        {
                            i.mainSkin.material.color = new Color32(0, 255, 0, 200);
                        }
                    }
                }
            }
        }

        internal override void OnStateChanged()
        {
            if(!State)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    if (vrrig != RigManager.self)
                        vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
            }
        }
    }
}
