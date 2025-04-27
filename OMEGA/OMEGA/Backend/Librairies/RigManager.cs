using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.Backend.Librairies
{
    public static class RigManager
    {
        public static VRRig self { get => GorillaTagger.Instance.offlineVRRig; }
        public static PhotonView GetView(VRRig rig) =>
            Traverse.Create(rig).Field("photonView").GetValue<PhotonView>();

        public static VRRig GetRigFromPlayer(Photon.Realtime.Player player) =>
            GorillaGameManager.StaticFindRigForPlayer(player);

        internal static Player GetPlayerFromRig(VRRig rig) =>
            rig.Creator.GetPlayerRef();
    }
}
