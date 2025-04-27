using GorillaNetworking;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using PlayFab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.XR;

namespace OMEGA.Backend.Modules
{
    internal class AcceptTOS : Module
    {
        internal override string Name => "Hide TOS";
        internal override string Category => "Player";
        internal override string ToolTip => "Hides the TOS for GUI users";
        internal override bool Pinned => true;
        internal override bool IsTogglable => false;
        internal override bool State { get; set; } = false;

        internal override void OnStateChanged()
        {
            foreach (PrivateUIRoom privateUIRoom in UnityEngine.Object.FindObjectsOfType<PrivateUIRoom>())
            {
                privateUIRoom.gameObject.SetActive(false);
            }
        }
    }

    internal class Retard : ScriptableObject
    {

    }
}
