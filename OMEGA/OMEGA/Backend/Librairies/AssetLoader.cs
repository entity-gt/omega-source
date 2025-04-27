using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace OMEGA.Backend.Components
{
    internal static class AssetLoader
    {

        private static AssetBundle ClickBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("OMEGA.Resources.click"));
        private static AssetBundle BackgroundBundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("OMEGA.Resources.image"));
        private static AudioSource audioSource;

        public static Texture2D LoadImage()
        {
            return BackgroundBundle.LoadAllAssets<Texture2D>().First();
        }

        public static void LoadClick()
        {
            if (audioSource != null) return;
            audioSource = Librairies.RigManager.self.leftHandPlayer;
            audioSource.clip = ClickBundle.LoadAsset<AudioClip>("click");
        }

        public static void PlayClick()
        {
            LoadClick();
            audioSource.loop = false;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
    }
}
