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
    internal class Noclip : Module
    {
        internal override string Name => "Noclip [RT]";
        internal override string Category => "Movement";
        internal override string ToolTip => "Makes you noclip through the walls/floor";
        internal override bool Pinned => true;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        internal override void Update()
        {
            if (State)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat < 0.1f)
                {
                    foreach (MeshCollider collider in GameObject.FindObjectsOfType<MeshCollider>())
                    {
                        collider.enabled = true;
                    }
                }

                else
                {
                    foreach (MeshCollider collider in GameObject.FindObjectsOfType<MeshCollider>())
                    {
                        collider.enabled = false;
                    }
                }
            }


        }
    }
}
