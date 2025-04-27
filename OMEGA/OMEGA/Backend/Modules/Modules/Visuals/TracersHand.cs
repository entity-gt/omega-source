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
    internal class TracersHand : Module
    {
        internal override string Name => "Hand Tracers";
        internal override string Category => "Visuals";
        internal override string ToolTip => "Adds line to everybody from your hands";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State && PhotonNetwork.InRoom)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != Librairies.RigManager.self)
                    {
                        LineRenderer Line = new GameObject("Line").AddComponent<LineRenderer>();
                        Line.startWidth = 0.01f;
                        Line.endWidth = 0.01f;
                        Line.material.shader = Shader.Find("GUI/Text Shader");
                        Line.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
                        Line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.transform.position);
                        Line.SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));

                        if (vrrig.mainSkin.material.name.Contains("infected"))
                            Line.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 100);
                        else
                            Line.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                        GameObject.Destroy(Line, Time.deltaTime);
                    }
                }
            }
        }
    }
}
