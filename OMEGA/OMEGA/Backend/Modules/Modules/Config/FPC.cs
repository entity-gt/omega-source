using Cinemachine;
using Cinemachine.Utility;
using GorillaNetworking;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class FPC : Module
    {
        internal override string Name => "FPC";
        internal override string Category => "Config";
        internal override string ToolTip => "Enables a First Person Camera";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;
        internal override void OnStateChanged()
        {
            if(State)
            {
                GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().enabled = false;
                GameObject.Find("Shoulder Camera").GetComponent<Camera>().transform.position = GorillaLocomotion.Player.Instance.headCollider.transform.position;
                GameObject.Find("Shoulder Camera").GetComponent<Camera>().transform.rotation = GorillaLocomotion.Player.Instance.headCollider.transform.rotation;
                GameObject.Find("Shoulder Camera").GetComponent<Camera>().fieldOfView = 125f;
            } else
            {
                GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().enabled = true;
                GameObject.Find("Shoulder Camera").GetComponent<Camera>().enabled = false;
            }
        }
    }
}
