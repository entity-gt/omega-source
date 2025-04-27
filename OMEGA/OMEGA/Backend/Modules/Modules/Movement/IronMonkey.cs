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
    internal class IronMonkey : Module
    {
        internal override string Name => "Iron Monkey";
        internal override string Category => "Movement";
        internal override string ToolTip => "turns you into iron man";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;
        internal override void Update()
        {
            if (State)
            {
                if (ControllerInputPoller.instance.leftGrab)
                {
                    GorillaLocomotion.Player.Instance.AddForce(8f * GorillaLocomotion.Player.Instance.rightControllerTransform.right, ForceMode.Acceleration);
                    GameObject LeftHandSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    LeftHandSphere.layer = 2;

                    UnityEngine.Object.Destroy(LeftHandSphere.GetComponent<BoxCollider>());

                    Rigidbody LeftHandSphereRigidBody = LeftHandSphere.AddComponent<Rigidbody>();
                    LeftHandSphereRigidBody.velocity = -GorillaLocomotion.Player.Instance.leftControllerTransform.right;
                    
                    LeftHandSphere.GetComponent<Renderer>().material.color = Color.white;

                    LeftHandSphere.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0, 0.1f, 0);
                    LeftHandSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    
                    UnityEngine.Object.Destroy(LeftHandSphere, 0.7f);
                    GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength, GorillaTagger.Instance.tapHapticDuration);
                }

                if (ControllerInputPoller.instance.rightGrab)
                {
                    GorillaLocomotion.Player.Instance.AddForce(8f * GorillaLocomotion.Player.Instance.rightControllerTransform.right, ForceMode.Acceleration);
                    GameObject RightHandSPhere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    RightHandSPhere.layer = 2;

                    UnityEngine.Object.Destroy(RightHandSPhere.GetComponent<BoxCollider>());
                    
                    Rigidbody RightHandSphereRigidBody = RightHandSPhere.AddComponent<Rigidbody>();
                    RightHandSphereRigidBody.velocity = -GorillaLocomotion.Player.Instance.leftControllerTransform.right;

                    RightHandSPhere.GetComponent<Renderer>().material.color = Color.white;

                    RightHandSPhere.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0, 0.1f, 0);
                    RightHandSPhere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    
                    UnityEngine.Object.Destroy(RightHandSPhere, 0.7f);
                    GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength, GorillaTagger.Instance.tapHapticDuration);
                }
            }
        }
    }
}
