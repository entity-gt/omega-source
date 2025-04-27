using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using OMEGA.Frontend;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class BoxESP : Module
    {
        internal override string Name => "Box ESP";
        internal override string Category => "Visuals";
        internal override string ToolTip => "adds a box to everone";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;
        internal override void Update()
        {
            if (!State)
                return;

            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != null && !vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                {
                    Transform boxTransform = vrrig.transform.Find("box");
                    if (boxTransform == null)
                    {
                        GameObject gameObject = new GameObject("box");
                        gameObject.transform.SetParent(vrrig.transform);
                        gameObject.transform.position = vrrig.transform.position;

                        GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject5 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                        UnityEngine.Object.Destroy(gameObject2.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject5.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject4.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject3.GetComponent<BoxCollider>());

                        gameObject2.transform.SetParent(gameObject.transform);
                        gameObject5.transform.SetParent(gameObject.transform);
                        gameObject4.transform.SetParent(gameObject.transform);
                        gameObject3.transform.SetParent(gameObject.transform);

                        gameObject2.transform.localPosition = new Vector3(0f, 0.49f, 0f);
                        gameObject2.transform.localScale = new Vector3(1f, 0.03f, 0.02f);
                        gameObject5.transform.localPosition = new Vector3(0f, -0.49f, 0f);
                        gameObject5.transform.localScale = new Vector3(1f, 0.03f, 0.02f);
                        gameObject4.transform.localPosition = new Vector3(-0.49f, 0f, 0f);
                        gameObject4.transform.localScale = new Vector3(0.03f, 1f, 0.02f);
                        gameObject3.transform.localPosition = new Vector3(0.49f, 0f, 0f);
                        gameObject3.transform.localScale = new Vector3(0.03f, 1f, 0.02f);
                        
                        gameObject2.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject5.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject4.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject3.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");

                        if(vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            gameObject2.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject5.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject4.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject3.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                        }

                        if (!vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            gameObject2.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject5.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject4.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject3.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                        }

                        boxTransform = gameObject.transform;
                    }

                    boxTransform.transform.LookAt(boxTransform.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
                }
            }
        }

        internal override void OnStateChanged()
        {
            if (State)
                return;

            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != null && !vrrig.isOfflineVRRig && !vrrig.isMyPlayer)
                {
                    var obe = vrrig.transform.Find("box");
                    if (obe != null)
                        GameObject.Destroy(obe.gameObject);
                }
            }
        }
    }
}
