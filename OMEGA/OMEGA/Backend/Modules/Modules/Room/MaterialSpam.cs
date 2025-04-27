using GorillaGameModes;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class MatSpam : Module
    {
        internal override string Name => "Material Spam (M)";
        internal override string Category => "Room";
        internal override string ToolTip => "Spams everyone's material in infection";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                PhotonUtils.CheckMaster(this);
                if(GorillaGameManager.instance.GameModeName() != "INFECTION")
                {
                    NotifiLib.Instance.SendNotification("MATSPAM", "Infection gamemode is required!", "red");
                    State = false;
                    return;
                }

                
                int selectedAction = (int)Math.Round((float)UnityEngine.Random.Range(0, 2));
                switch(selectedAction)
                {
                    case 0:
                        foreach (Player player in PhotonNetwork.PlayerListOthers)
                            typeof(GorillaTagManager).GetMethod("AddInfectedPlayer").Invoke(GorillaTagManager.instance, new object[] { (NetPlayer)player, true });
                        break;

                    case 1:
                        GorillaTagManager.instance.Invoke("ClearInfectionState", 0f);
                        break;
                    case 2:
                        typeof(GorillaTagManager).GetMethod("SetisCurrentlyTag").Invoke(GorillaTagManager.instance, new object[] { Math.Round((float)UnityEngine.Random.Range(0, 1)) == 1 ? true : false });
                        foreach (Player player in PhotonNetwork.PlayerListOthers)
                            typeof(GorillaTagManager).GetMethod("SetCurrentIt").Invoke(GorillaTagManager.instance, new object[] { (NetPlayer)player, true });
                        break;
                }
            }
        }
    }
}
