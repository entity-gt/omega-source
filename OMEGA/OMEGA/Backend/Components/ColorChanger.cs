using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Components
{
    internal class ColorChanger : MonoBehaviour
    {
        public void Update() =>
            this.gameObject.GetComponent<MeshRenderer>().material.color = Globals.GetMainThemeColor();
    }
}
