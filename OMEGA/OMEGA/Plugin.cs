using BepInEx;
using Cinemachine;
using Fusion.Sockets;
using HarmonyLib;
using ModIO.Implementation.API;
using Omega.Frontend.Interface;
using OMEGA.Backend.Librairies;
using OMEGA.Backend.Listeners;
using OMEGA.Frontend;
using OMEGA.Frontend.AudioPlayer;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;


namespace OMEGA.Backend
{
    [BepInPlugin("fr.omegateam.omega", "OMEGA", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            /* Backend Init */
            Backend.Modules.System.Config.Config.InitConfig();
            Backend.Modules.System.ModuleHandler.Awake();
            new Harmony("fr.omegateam.omega").PatchAll();

            /* Frontend Init */
            OmegaUI.Start();

            GameObject notifiLibObject = new GameObject("NotifLib");
            notifiLibObject.AddComponent<NotifiLib>();
            GameObject.DontDestroyOnLoad(notifiLibObject);
        }

        void Update()
        {
            /* Backend */
            Backend.Modules.System.ModuleHandler.Update();

            /* Frontend */
            Frontend.WristMenu.Update();
            Frontend.BoardModifier.Update();
        }

        void OnGUI()
        {
            OmegaUI.OnGui();
            AudioPlayerUI.OnGui();
        }
    }
}
