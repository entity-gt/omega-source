using GorillaNetworking;
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
    internal class JoinPublicRoom : Module
    {
        internal override string Name => "Join Public";
        internal override string ToolTip => "Makes you join a random public room";
        internal override bool Pinned => true;
        internal override string Category => "Room";
        internal override bool IsTogglable => false;
        internal override bool State { get; set; } = false;

        internal override void OnStateChanged()
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.Disconnect();
            PhotonNetworkController.Instance.AttemptToJoinPublicRoom(GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>());
        }
    }
}
