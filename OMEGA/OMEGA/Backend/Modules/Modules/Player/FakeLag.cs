using OMEGA.Backend.Librairies;
using OMEGA.Backend.Modules.System;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace OMEGA.Backend.Modules
{
    internal class FakeLag : Module
    {
        internal override string Name => "Fake Lag";
        internal override string Category => "Player";
        internal override string ToolTip => "Imitates high latency";
        internal override bool Pinned => false;
        internal override bool IsTogglable => true;
        internal override bool State { get; set; } = false;

        private Thread FakeLagThread;

        internal override void OnStateChanged()
        {
            if (State)
                FakeLagThread = new Thread(FakeLagDelegate);
            else
                FakeLagThread.Abort();
        }

        internal void FakeLagDelegate()
        {
            while(true)
            {
                Thread.Sleep((int)(UnityEngine.Random.Range(0.5f, 1.2f) * 1000));
                RigManager.self.enabled = !RigManager.self.enabled;
            }
        }
    }
}
