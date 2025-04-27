using GorillaLocomotion;
using OMEGA.Backend;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Loading
{
    public static class Loader
    {
        public static GameObject mainObject = new GameObject("Omega");

        public static void Load()
        {
            GameObject.DontDestroyOnLoad(mainObject);
            Player.Instance.StartCoroutine(startCoroutine());
        }

        public static IEnumerator startCoroutine()
        {
            yield return new WaitForSeconds(5f);
            mainObject.AddComponent<Plugin>();
        }
    }
}