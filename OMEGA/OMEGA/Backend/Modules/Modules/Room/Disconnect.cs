using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.Backend.Modules
{
    internal class Disconnect : Module
    {
        internal override string Name => "Disconnect";
        internal override string Category => "Room";
        internal override string ToolTip => "Disconnects you from the room";
        internal override bool Pinned => true;
        internal override bool IsTogglable => false;
        internal override bool State { get; set; } = false;

        internal override void OnStateChanged()
        {
            PhotonNetwork.Disconnect();
            NotifiLib.Instance.SendNotification("SUCCESS", "Disconnected!");
        }
    }
}