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
    internal class GrappleGun : Module
    {
        internal override string Name => "Grapple Gun";
        internal override string Category => "Movement";
        internal override string ToolTip => "You can grapple around with the grapple hook";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;
        public static LineRenderer lineRenderer = null;
        public static bool locked = false;
        internal override void Update()
        {
            if (State)
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                if (ControllerInputPoller.instance.rightGrab)
                {
                    if (lineRenderer == null)
                    {
                        lineRenderer = new GameObject("line").AddComponent<LineRenderer>();
                        lineRenderer.startColor = Color.black;
                        lineRenderer.endColor = Color.black;
                        lineRenderer.startWidth = 0.01f;
                        lineRenderer.endWidth = 0.01f;
                        lineRenderer.positionCount = 2;
                        lineRenderer.useWorldSpace = true;
                        lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                    }
                    if (!locked)
                        lineRenderer.SetPosition(0, raycastHit.point);

                    lineRenderer.SetPosition(1, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                    if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                    {
                        locked = true;
                        GorillaLocomotion.Player.Instance.AddForce(Vector3.Normalize(lineRenderer.GetPosition(0) - GorillaLocomotion.Player.Instance.bodyCollider.transform.position) * 100f, ForceMode.Acceleration);
                    }
                    else
                    {
                        locked = false;
                    }
                }
                else
                {
                    if (lineRenderer != null)
                    {
                        UnityEngine.Object.Destroy(lineRenderer);
                        lineRenderer = null;
                    }
                }
            }
        }
    }
}
