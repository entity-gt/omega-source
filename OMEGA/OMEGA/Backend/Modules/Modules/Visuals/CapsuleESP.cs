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
    internal class CapSuleESP : Module
    {
        internal override string Name => "Capsule ESP";
        internal override string Category => "Visuals";
        internal override string ToolTip => "Adds a capsule to all players";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if(State) 
            {
                foreach(VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (Librairies.RigManager.self != i)
                    {
                        GameObject Capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        Capsule.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        Capsule.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 100);
                        Capsule.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        Capsule.transform.position = i.transform.position;
                        GameObject.Destroy(Capsule.GetComponent<CapsuleCollider>());
                        GameObject.Destroy(Capsule, Time.deltaTime);
                        if (i.mainSkin.material.name.Contains("infected"))
                        {
                            Capsule.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 100);
                        }
                        else
                        {
                            Capsule.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                        }
                    }
                }
            }
        }

    }
}
