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
    internal class CornerESP : Module
    {
        internal override string Name => "Corner ESP";
        internal override string Category => "Visuals";
        internal override string ToolTip => "Adds A corner esp to all players";
        internal override bool Pinned => true;
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
                    Transform cornerTransform = vrrig.transform.Find("cornerESP");
                    if (cornerTransform == null)
                    {
                        GameObject gameObject = new GameObject("cornerESP");
                        gameObject.transform.SetParent(vrrig.transform);
                        gameObject.transform.position = vrrig.transform.position;

                        GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject7 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject8 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject gameObject9 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                        UnityEngine.Object.Destroy(gameObject2.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject3.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject4.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject5.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject6.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject7.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject8.GetComponent<BoxCollider>());
                        UnityEngine.Object.Destroy(gameObject9.GetComponent<BoxCollider>());

                        gameObject2.transform.SetParent(gameObject.transform);
                        gameObject3.transform.SetParent(gameObject.transform);
                        gameObject4.transform.SetParent(gameObject.transform);
                        gameObject5.transform.SetParent(gameObject.transform);
                        gameObject6.transform.SetParent(gameObject.transform);
                        gameObject7.transform.SetParent(gameObject.transform);
                        gameObject8.transform.SetParent(gameObject.transform);
                        gameObject9.transform.SetParent(gameObject.transform);

                        gameObject2.transform.localPosition = gameObject.transform.up * 0.35f + gameObject.transform.right * 0.24f;
                        gameObject2.transform.localScale = new Vector3(0.2f, 0.02f, 0.01f);

                        gameObject3.transform.localPosition = gameObject.transform.right * 0.33f + gameObject.transform.up * 0.26f;
                        gameObject3.transform.localScale = new Vector3(0.02f, 0.2f, 0.01f);

                        gameObject4.transform.localPosition = gameObject.transform.up * 0.35f + gameObject.transform.right * -0.24f;
                        gameObject4.transform.localScale = new Vector3(0.2f, 0.02f, 0.01f);

                        gameObject5.transform.localPosition = gameObject.transform.right * -0.33f + gameObject.transform.up * 0.26f;
                        gameObject5.transform.localScale = new Vector3(0.02f, 0.2f, 0.01f);

                        gameObject6.transform.localPosition = gameObject.transform.up * -0.55f + gameObject.transform.right * -0.24f;
                        gameObject6.transform.localScale = new Vector3(0.2f, 0.02f, 0.01f);

                        gameObject7.transform.localPosition = gameObject.transform.right * -0.33f + gameObject.transform.up * -0.46f;
                        gameObject7.transform.localScale = new Vector3(0.02f, 0.2f, 0.01f);

                        gameObject8.transform.localPosition = gameObject.transform.up * -0.55f + gameObject.transform.right * 0.24f;
                        gameObject8.transform.localScale = new Vector3(0.2f, 0.02f, 0.01f);

                        gameObject9.transform.localPosition = gameObject.transform.right * 0.33f + gameObject.transform.up * -0.46f;
                        gameObject9.transform.localScale = new Vector3(0.02f, 0.2f, 0.01f);

                        gameObject2.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject3.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject4.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject5.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject6.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject7.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject8.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        gameObject9.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");

                        if(vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            gameObject2.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject3.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject4.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject5.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject6.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject7.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject8.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                            gameObject9.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 200);
                        }


                        if (!vrrig.mainSkin.material.name.Contains("fected"))
                        {
                            gameObject2.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject3.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject4.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject5.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject6.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject7.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject8.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                            gameObject9.GetComponent<Renderer>().material.color = Globals.GetMainThemeColor();
                        }

                        cornerTransform = gameObject.transform;
                    }

                    cornerTransform.transform.LookAt(cornerTransform.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
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
                    Transform obe = vrrig.transform.Find("cornerESP");
                    if (obe != null)
                        GameObject.Destroy(obe.gameObject);
                }
            }
        }
    }
}