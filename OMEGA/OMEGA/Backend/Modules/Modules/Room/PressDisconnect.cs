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
    internal class PressDisconnect : Module
    {
        internal override string Name => "Diconnect [B]";
        internal override string Category => "Room";
        internal override string ToolTip => "Disconnects when B is pressed";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State && ControllerInputPoller.instance.rightControllerSecondaryButton && PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
                NotifiLib.Instance.SendNotification("SUCCESS", "Disconnected!");
            }
        }
    }
}
