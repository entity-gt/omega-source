using OMEGA.Backend.Modules.System;
using Photon.Pun;
using GorillaLocomotion;
using UnityEngine.UI;
using UnityEngine;

namespace OMEGA.Backend.Modules.Modules.Player
{
	internal class SpazHands : Module
	{
		internal override string Name => "Spaz Hands (RG)";
		internal override string Category => "Player";
		internal override string ToolTip => "Spazzes hands aggresivley";
		internal override bool Pinned => false;
		internal override bool IsTogglable => true;
		internal override bool State { get; set; } = false;

		internal bool ToggleAntiRepeat = false;

		internal override void Update()
		{
			if (State)
			{
				if (ControllerInputPoller.instance.rightGrab)
				{
					GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
					GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                }

				else
				{
					GorillaTagger.Instance.offlineVRRig.leftHandTransform.rotation = Quaternion.identity;
					GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation = Quaternion.identity;
                }

			}

		}
	}
}
