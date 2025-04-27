using OMEGA.Backend.Components;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace OMEGA.Backend.Modules
{
    internal class Platforms : Module
    {
        internal override string Name => "Platforms";
        internal override string Category => "Movement";
        internal override string ToolTip => "Summons platforms when grips are pressed";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal GameObject RPlat;
        internal GameObject LPlat;

        internal override void Update()
        {
            if (State)
            {
                if(ControllerInputPoller.instance.rightGrab && RPlat == null)
                {
                    RPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RPlat.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    RPlat.AddComponent<ColorChanger>();
                    RPlat.transform.localScale = new Vector3(0.03f, 0.25f, 0.25f);
                    RPlat.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, -0.03f, 0f);
                    RPlat.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                }
                else if(!ControllerInputPoller.instance.rightGrab && RPlat != null)
                {
                    GameObject.Destroy(RPlat);
                    RPlat = null;
                }
                
                if(ControllerInputPoller.instance.leftGrab && LPlat == null) {
                    LPlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LPlat.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                    LPlat.AddComponent<ColorChanger>();
                    LPlat.transform.localScale = new Vector3(0.03f, 0.25f, 0.25f);
                    LPlat.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, -0.03f, 0f);
                    LPlat.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                } 
                else if(!ControllerInputPoller.instance.leftGrab &&  LPlat != null)
                {
                    GameObject.Destroy(LPlat);
                    LPlat = null;
                }
            }
        }
    }
}
