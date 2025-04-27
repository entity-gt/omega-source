using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using OMEGA.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Components
{
    internal class TooltipButtonComponent : MonoBehaviour
    {
        internal string ModuleId = null;

        public void OnTriggerEnter(Collider collider)
        {
            if (ModuleId != null && collider.gameObject.name == "OMEGAPOINTER" && Time.time > WristMenu.PressCooldown)
            {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration / 2f);
                AssetLoader.PlayClick();
                NotifiLib.Instance.SendNotification("TOOLTIP", ModuleHandler.GetModule(ModuleId).ToolTip, "grey");
                WristMenu.PressCooldown = Time.time + 0.3f;
            }
        }
    }
}
