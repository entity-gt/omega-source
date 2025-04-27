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
    internal class PageButton : MonoBehaviour
    {
        internal bool ForwardPage = false;
        public void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "OMEGAPOINTER" && Time.time > WristMenu.PressCooldown)
            {
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength, GorillaTagger.Instance.tagHapticDuration / 2f);
                AssetLoader.PlayClick();

                if (ForwardPage)
                    if (WristMenu.pageIndex == WristMenu.pageMax - 1)
                        WristMenu.pageIndex = 0;
                    else WristMenu.pageIndex++;
                else
                    if (WristMenu.pageIndex == 0)
                        WristMenu.pageIndex = WristMenu.pageMax - 1;
                    else WristMenu.pageIndex--;

                WristMenu.RefreshButtons();


                WristMenu.PressCooldown = Time.time + 0.3f;
            }
        }
    }
}
