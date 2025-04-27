using BepInEx;
using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Fusion;
using TMPro;

namespace OMEGA.Backend.Librairies
{
    internal class NotifiLib : MonoBehaviour
    {
        private static GameObject HUDObj, HUDObj2, MainCamera;
        private static Text notificationText;
        private static readonly float notificationDelay = 1f;
        private static Dictionary<string, float> notificationTimestamps = new Dictionary<string, float>();
        public static string PreviousNotification;
        public static bool IsEnabled = true;
        private bool hasInitialized;

        private static NotifiLib instance;
        public static NotifiLib Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NotifiLib>() ?? new GameObject("NotificationLib").AddComponent<NotifiLib>();
                }
                return instance;
            }
        }
        private void Init()
        {
            if (MainCamera != null) return;

            MainCamera = GameObject.Find("Main Camera");

            HUDObj2 = new GameObject("HUD_Notification_Parent");
            HUDObj = new GameObject("HUD_Notification");

            var canvas = HUDObj.AddComponent<Canvas>();
            var canvasScaler = HUDObj.AddComponent<CanvasScaler>();
            canvasScaler.dynamicPixelsPerUnit = 10;
            HUDObj.AddComponent<GraphicRaycaster>();

            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = MainCamera.GetComponent<Camera>();

            var rectTransform = HUDObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(5f, 5f);
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = new Vector3(0f, 0f, 1.6f);
            rectTransform.rotation = Quaternion.Euler(0f, -270f, 0f);

            HUDObj.transform.parent = HUDObj2.transform;
            HUDObj2.transform.position = MainCamera.transform.position + new Vector3(-1.5f, 0f, -4.5f);

            notificationText = CreateTextElement("NotificationText", HUDObj, new Vector3(-1.2f, -0.9f, -0.22f), new Vector2(260f, 70f), 7);
            notificationText.font = Globals.Consolas;
            notificationText.material = new Material(Shader.Find("GUI/Text Shader"));

            hasInitialized = true;
        }

        private Text CreateTextElement(string name, GameObject parent, Vector3 position, Vector2 size, int fontSize)
        {
            var textObject = new GameObject(name);
            textObject.transform.parent = parent.transform;

            var text = textObject.AddComponent<Text>();
            text.fontSize = fontSize;
            text.alignment = TextAnchor.MiddleCenter;
            text.rectTransform.sizeDelta = size;
            text.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
            text.rectTransform.localPosition = position;
            return text;
        }

        private void FixedUpdate()
        {
            if (!hasInitialized && GameObject.Find("Main Camera") != null)
            {
                Init();
            }

            if (HUDObj2 != null && MainCamera != null)
            {
                HUDObj2.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
            }

            var now = Time.time;

            foreach (var notification in notificationTimestamps.Keys.ToList())
            {
                if (now - notificationTimestamps[notification] > notificationDelay)
                {
                    var lines = notificationText.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Where(line => line.Trim() != notification).ToArray();

                    notificationText.text = string.Join(Environment.NewLine, lines);
                    notificationTimestamps.Remove(notification);
                }
            }
        }

        public void SendNotification(string icon, string content, string color = "green")
        {
            content = $"<color=grey>[</color><color={color}>{icon}</color><color=grey>]</color> {content}";

            if (!IsEnabled || content == PreviousNotification || notificationText == null) return;
            if (!content.Contains(Environment.NewLine))
                content += Environment.NewLine;

            notificationText.text += content;
            notificationText.color = Color.white;
            PreviousNotification = content;
            notificationTimestamps[content.Trim()] = Time.time;
        }

        public static void ClearAllNotifications()
        {
            notificationText.text = string.Empty;
            notificationTimestamps.Clear();
        }

        public static void ClearPastNotifications(int amount)
        {
            var lines = notificationText.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Skip(amount).Where(line => !string.IsNullOrEmpty(line)).ToArray();
            notificationText.text = string.Join(Environment.NewLine, lines);
        }

        private void OnDestroy()
        {
            if (notificationText.material != null)
            {
                Destroy(notificationText.material);
            }

            Destroy(HUDObj);
            Destroy(HUDObj2);
        }
    }
}
