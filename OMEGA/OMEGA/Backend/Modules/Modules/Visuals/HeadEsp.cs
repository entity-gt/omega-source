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
    internal class HeadEsp : Module
    {
        internal override string Name => "Head Esp";
        internal override string Category => "Visuals";
        internal override string ToolTip => "Adds a Circle Over Everybodys head";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State && PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs) 
                {
                    if(Librairies.RigManager.self != i) 
                    {
                        GameObject headesp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        headesp.transform.localScale = new Vector3(0.185f, 0.185f, 0.185f);
                        headesp.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        GameObject.Destroy(headesp.GetComponent<SphereCollider>());
                        headesp.transform.position = i.transform.position + new Vector3(0, 1, 0);
                        GameObject.Destroy(headesp, Time.deltaTime);

                        if (i.mainSkin.material.name.Contains("infected"))
                        {
                            headesp.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 100);
                        }
                        else
                        {
                            headesp.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                        }
                    }
                }
            }
        }
    }
}
