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
    internal class HomeButtonComponent : MonoBehaviour
    {
        public void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "OMEGAPOINTER" && Time.time > WristMenu.PressCooldown)
            {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration / 2f);
                AssetLoader.PlayClick();
                WristMenu.currentCategory = "home";
                WristMenu.pageIndex = 0;
                WristMenu.RefreshButtons();
                WristMenu.PressCooldown = Time.time + 0.3f;
            }
        }
    }
}
