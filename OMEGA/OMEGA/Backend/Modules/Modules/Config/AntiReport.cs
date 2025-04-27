using GorillaNetworking;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

namespace OMEGA.Backend.Modules.Modules.Player
{
    internal class AntiReport : Module
    {
        internal override string Name => "Anti Report";
        internal override string Category => "Config";
        internal override string ToolTip => "Makes you leave when someone is close to the board";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = true;

        internal override void Update()
        {
            if (State)
            {
                GameObject reportButton = GameObject.Find("ReportButton");
                if (reportButton == null) return;

                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (Librairies.RigManager.self != i)
                    {
                        if (Vector3.Distance(reportButton.transform.position, i.rightHandTransform.transform.position) < 1f || Vector3.Distance(reportButton.transform.position, i.transform.position) < 1f || Vector3.Distance(reportButton.transform.position, i.leftHandTransform.transform.position) < 1f)
                        {
                            NotifiLib.Instance.SendNotification("ANTIREPORT", "Someone tried to report you! Leaving", "red");
                            PhotonNetwork.Disconnect();
                        }
                    }
                }
            }

        }
    }
}
