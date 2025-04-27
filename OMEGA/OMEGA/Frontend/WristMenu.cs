using HarmonyLib;
using OMEGA.Backend;
using OMEGA.Backend.Components;
using OMEGA.Backend.Modules.System.Config;
using OMEGA.Backend.Modules.System;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Module = OMEGA.Backend.Modules.System.Module;

namespace OMEGA.Frontend
{
    internal class WristMenu
    {
        private static GameObject menuObject;
        private static GameObject pointerObject;
        private static List<GameObject> ButtonsObjects = new List<GameObject>();
        private static List<GameObject> TooltipButtonsObjects = new List<GameObject>();

        private static GameObject BackPageButton;
        private static GameObject ForwardPageButton;
        private static GameObject ReturnHomeButton;

        private static Texture menuTexture = new Texture2D(1779, 1300);
        private static bool textureInitialized = false;

        internal static int pageIndex = 0;
        internal static string currentCategory = "home";
        internal static int pageMax { get => (int)Math.Ceiling((currentCategory == "home" ? ModuleHandler.categories.Count : ModuleHandler.modules.Where(m => m.Category == currentCategory).Count()) / 5f); }
        internal static float PressCooldown = Time.time;
        internal static Color rgbColorSlow = Color.blue;
        internal static Color rgbColor = Color.blue;

        private static float MenuTitleRGBCooldown = Time.time;
        private static int MenuRGBCharacterIndex = 0;

        public static void Init()
        {
            menuTexture = AssetLoader.LoadImage();
            textureInitialized = true;
        }

        public static void Update()
        {
            if (ControllerInputPoller.instance.leftControllerSecondaryButton)
            {
                if (textureInitialized == false)
                    Frontend.WristMenu.Init();

                DrawMenu();
                CreatePointer();
            }
            else
            {
                TryDestroyMenu();
                TryDestroyPointer();
            }

            rgbColor = Color.HSVToRGB(Time.frameCount / 100f % 1f, 1f, 1f);
            rgbColorSlow = Color.HSVToRGB(Time.frameCount / 280f % 1f, 1f, 1f);
        }

        #region Pointer

        public static void CreatePointer()
        {
            if (pointerObject == null)
            {
                pointerObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointerObject.name = "OMEGAPOINTER";

                pointerObject.transform.SetParent(GorillaTagger.Instance.rightHandTransform);
                pointerObject.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                pointerObject.transform.localPosition = new Vector3(0f, -0.10f, 0f);
                pointerObject.transform.localRotation = Quaternion.Euler(0, 45, 0);

                GameObject.Destroy(pointerObject.GetComponent<MeshRenderer>());
            }
        }

        public static void DestroyPointer()
        {
            GameObject.Destroy(pointerObject);
            pointerObject = null;
        }

        public static void TryDestroyPointer()
        {
            if (pointerObject != null)
                DestroyPointer();
        }

        #endregion Pointer

        #region Menu

        public static void DrawMenu()
        {
            if (menuObject == null)
            {
                /* Create Menu Object */
                menuObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                /* MenuObject Scale & Adjust Components */
                menuObject.transform.localScale = new Vector3(0.01f, 0.25f, 0.35f);

                menuObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                menuObject.GetComponent<MeshRenderer>().material.mainTexture = menuTexture;
                menuObject.GetComponent<MeshRenderer>().material.shaderKeywords = new string[] { "_USE_TEXTURE" };

                GameObject canvasObj = new GameObject();
                canvasObj.transform.parent = menuObject.transform;
                canvasObj.transform.localPosition = new Vector3(1f, 0f, 0f);

                Canvas canvas = canvasObj.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

                GameObject menuTitleTextObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                };

                menuTitleTextObject.AddComponent<CanvasRenderer>();
                UnityEngine.UI.Text menuTitleText = menuTitleTextObject.AddComponent<UnityEngine.UI.Text>();

                menuTitleText.text = Globals.MenuTitle;
                menuTitleText.font = Globals.Consolas;
                menuTitleText.alignment = TextAnchor.MiddleCenter;
                menuTitleText.color = Color.white;
                menuTitleText.resizeTextForBestFit = true;
                menuTitleText.resizeTextMinSize = 0;
                menuTitleText.supportRichText = true;

                menuTitleText.transform.localScale = new Vector3(0.12f, 0.12f, 0.02f);
                menuTitleText.transform.localPosition = new Vector3(0.003f, 0f, 0.13f);
                menuTitleText.transform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                GameObject.Destroy(menuObject.GetComponent<Rigidbody>());
                GameObject.Destroy(menuObject.GetComponent<BoxCollider>());
            }

            menuObject.transform.position = GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.leftHandTransform.right * 0.05f;
            menuObject.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

            if (Time.time > MenuTitleRGBCooldown)
            {
                if (MenuRGBCharacterIndex != 5)
                    menuObject.GetComponentInChildren<UnityEngine.UI.Text>().text = Globals.MenuTitle.Replace(Globals.MenuTitle[MenuRGBCharacterIndex].ToString(), $"<color=#{ColorUtility.ToHtmlStringRGB(rgbColor)}>{Globals.MenuTitle[MenuRGBCharacterIndex]}</color>");

                MenuTitleRGBCooldown = Time.time + 0.1f;
                if (MenuRGBCharacterIndex == 5)
                {
                    MenuTitleRGBCooldown = Time.time + 1.5f;
                    menuObject.GetComponentInChildren<UnityEngine.UI.Text>().text = Globals.MenuTitle;
                    MenuRGBCharacterIndex = -1;
                }
                MenuRGBCharacterIndex = (MenuRGBCharacterIndex + 1) % 6;
            }

            if (currentCategory == "home")
                DrawCategoryButtons();
            else DrawButtons();

            DrawPageButton(true);
            DrawPageButton(false);
        }

        public static void RefreshMenu()
        {
            TryDestroyMenu();
            DrawMenu();
        }

        public static void DestroyMenu()
        {
            if (menuObject != null)
            {
                GameObject.Destroy(menuObject);
                menuObject = null;
            }
        }

        public static void TryDestroyMenu()
        {
            if (menuObject != null)
                DestroyMenu();

            TryDestroyButtons();
        }

        #endregion Menu

        #region Buttons

        public static void TryDestroyButtons()
        {
            if (ButtonsObjects.Count > 0)
                DestroyButtons();

            if (ReturnHomeButton != null)
            {
                GameObject.Destroy(ReturnHomeButton);
                ReturnHomeButton = null;
            }
        }

        public static void DestroyButtons()
        {
            ButtonsObjects.ForEach(GameObject.Destroy);
            ButtonsObjects.Clear();

            TooltipButtonsObjects.ForEach(GameObject.Destroy);
            TooltipButtonsObjects.Clear();
        }

        public static void RefreshButtons()
        {
            TryDestroyButtons();
            DrawButtons();
        }

        public static void DrawButtons()
        {
            Module[] ModulesToShow = ModuleHandler.modules.Where((m) => m.Category == currentCategory).Skip(pageIndex * 5).Take(5).ToArray();

            for (int i = 0; i < ModulesToShow.Length; i++)
            {
                DrawButton(i, ModulesToShow[i].ModuleId);
                if (Config.showTooltips.Value) DrawTooltipButton(i, ModulesToShow[i].ModuleId);
            }

            if (currentCategory != "home")
                DrawReturnHomeCategoryButton();
        }

        public static void DrawCategoryButtons()
        {
            Category[] CategoriesToShow = ModuleHandler.categories.Skip(pageIndex * 5).Take(5).ToArray();

            for (int i = 0; i < CategoriesToShow.Length; i++)
            {
                DrawCategoryButton(i, CategoriesToShow[i].CategoryId);
            }
        }

        public static void DrawReturnHomeCategoryButton()
        {
            if (ReturnHomeButton == null)
            {
                ReturnHomeButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                ReturnHomeButton.transform.localScale = new Vector3(0.01f, 0.22f, 0.038f);

                GameObject.Destroy(ReturnHomeButton.GetComponent<Rigidbody>());
                ReturnHomeButton.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                ReturnHomeButton.GetComponent<MeshRenderer>().material.color = Color.black;

                ReturnHomeButton.GetComponent<BoxCollider>().isTrigger = true;
                ReturnHomeButton.AddComponent<HomeButtonComponent>();

                GameObject canvasObj = new GameObject();
                canvasObj.transform.parent = ReturnHomeButton.transform;
                canvasObj.transform.localPosition = new Vector3(0.5f, 0f, 0f);

                Canvas canvas = canvasObj.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

                GameObject buttonTitleTextObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                };

                buttonTitleTextObject.AddComponent<CanvasRenderer>();
                UnityEngine.UI.Text buttonTitleText = buttonTitleTextObject.AddComponent<UnityEngine.UI.Text>();

                buttonTitleText.text = "Home";
                buttonTitleText.font = Globals.Consolas;
                buttonTitleText.alignment = TextAnchor.MiddleCenter;
                buttonTitleText.color = Color.white;
                buttonTitleText.resizeTextForBestFit = true;
                buttonTitleText.resizeTextMinSize = 0;

                buttonTitleText.transform.localScale = new Vector3(0.053f, 0.053f, 0.015f);
                buttonTitleText.transform.localPosition = new Vector3(0.005f, 0f, 0f);
                buttonTitleText.transform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            ReturnHomeButton.transform.position = menuObject.transform.position;
            ReturnHomeButton.transform.rotation = menuObject.transform.rotation;
            ReturnHomeButton.transform.localPosition = new Vector3(1f, 0f, -0.65f);

            ReturnHomeButton.transform.SetParent(menuObject.transform);
        }

        public static void DrawCategoryButton(int buttonIndex, string categoryId)
        {
            Category category = ModuleHandler.GetCategory(categoryId);
            if (category == null) return;

            if (ButtonsObjects.Count < buttonIndex + 1)
            {
                ButtonsObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
                ButtonsObjects[buttonIndex].transform.localScale = new Vector3(0.01f, 0.22f, 0.038f);

                GameObject.Destroy(ButtonsObjects[buttonIndex].GetComponent<Rigidbody>());
                ButtonsObjects[buttonIndex].GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                ButtonsObjects[buttonIndex].GetComponent<MeshRenderer>().material.color = Color.black;

                ButtonsObjects[buttonIndex].GetComponent<BoxCollider>().isTrigger = true;
                ButtonsObjects[buttonIndex].AddComponent<CategoryButtonComponent>().CategoryId = categoryId;

                GameObject canvasObj = new GameObject();
                canvasObj.transform.parent = ButtonsObjects[buttonIndex].transform;
                canvasObj.transform.localPosition = new Vector3(0.5f, 0f, 0f);

                Canvas canvas = canvasObj.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

                GameObject buttonTitleTextObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                };

                buttonTitleTextObject.AddComponent<CanvasRenderer>();
                UnityEngine.UI.Text buttonTitleText = buttonTitleTextObject.AddComponent<UnityEngine.UI.Text>();

                buttonTitleText.text = category.Name;
                buttonTitleText.font = Globals.Consolas;
                buttonTitleText.alignment = TextAnchor.MiddleCenter;
                buttonTitleText.color = Color.white;
                buttonTitleText.resizeTextForBestFit = true;
                buttonTitleText.resizeTextMinSize = 0;

                buttonTitleText.transform.localScale = new Vector3(0.053f, 0.053f, 0.015f);
                buttonTitleText.transform.localPosition = new Vector3(0.005f, 0f, 0f);
                buttonTitleText.transform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            }

            ButtonsObjects[buttonIndex].transform.position = menuObject.transform.position;
            ButtonsObjects[buttonIndex].transform.rotation = menuObject.transform.rotation;
            ButtonsObjects[buttonIndex].transform.localPosition = new Vector3(1f, 0f, 0.2f - (buttonIndex * 0.12f));

            ButtonsObjects[buttonIndex].transform.SetParent(menuObject.transform);
        }

        public static void DrawButton(int buttonIndex, string moduleId)
        {
            Module module = ModuleHandler.GetModule(moduleId);
            if (module == null) return;

            if (ButtonsObjects.Count < buttonIndex + 1)
            {
                ButtonsObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
                ButtonsObjects[buttonIndex].transform.localScale = new Vector3(0.01f, 0.22f, 0.038f);

                GameObject.Destroy(ButtonsObjects[buttonIndex].GetComponent<Rigidbody>());
                ButtonsObjects[buttonIndex].GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                ButtonsObjects[buttonIndex].GetComponent<MeshRenderer>().material.color = Color.black;

                ButtonsObjects[buttonIndex].GetComponent<BoxCollider>().isTrigger = true;
                ButtonsObjects[buttonIndex].AddComponent<ButtonComponent>().ModuleId = moduleId;

                GameObject canvasObj = new GameObject();
                canvasObj.transform.parent = ButtonsObjects[buttonIndex].transform;
                canvasObj.transform.localPosition = new Vector3(0.5f, 0f, 0f);

                Canvas canvas = canvasObj.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

                GameObject buttonTitleTextObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                };

                buttonTitleTextObject.AddComponent<CanvasRenderer>();
                UnityEngine.UI.Text buttonTitleText = buttonTitleTextObject.AddComponent<UnityEngine.UI.Text>();

                buttonTitleText.text = module.Name;
                buttonTitleText.font = Globals.Consolas;
                buttonTitleText.alignment = TextAnchor.MiddleCenter;
                buttonTitleText.color = module.State ? rgbColorSlow : Color.white;
                buttonTitleText.resizeTextForBestFit = true;
                buttonTitleText.resizeTextMinSize = 0;

                buttonTitleText.transform.localScale = new Vector3(0.053f, 0.053f, 0.015f);
                buttonTitleText.transform.localPosition = new Vector3(0.005f, 0f, 0f);
                buttonTitleText.transform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            }

            ButtonsObjects[buttonIndex].transform.position = menuObject.transform.position;
            ButtonsObjects[buttonIndex].transform.rotation = menuObject.transform.rotation;
            ButtonsObjects[buttonIndex].transform.localPosition = new Vector3(1f, 0f, 0.2f - (buttonIndex * 0.12f));

            ButtonsObjects[buttonIndex].transform.SetParent(menuObject.transform);

            ButtonsObjects[buttonIndex].GetComponentInChildren<UnityEngine.UI.Text>().color = module.State ? rgbColorSlow : Color.white;
        }

        public static void DrawPageButton(bool ForwardButton)
        {
            GameObject currentObject = null;
            if (ForwardButton ? ForwardPageButton == null : BackPageButton == null)
            {
                currentObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                currentObject.transform.localScale = new Vector3(0.01f, 0.110f, 0.05f);

                GameObject.Destroy(currentObject.GetComponent<Rigidbody>());
                currentObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                currentObject.GetComponent<MeshRenderer>().material.color = Color.black;

                currentObject.GetComponent<BoxCollider>().isTrigger = true;
                currentObject.AddComponent<PageButton>().ForwardPage = ForwardButton;

                GameObject canvasObj = new GameObject();
                canvasObj.transform.parent = currentObject.transform;
                canvasObj.transform.localPosition = new Vector3(0.2f, 0f, 0f);

                Canvas canvas = canvasObj.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

                GameObject buttonTitleTextObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                };

                buttonTitleTextObject.AddComponent<CanvasRenderer>();
                UnityEngine.UI.Text buttonTitleText = buttonTitleTextObject.AddComponent<UnityEngine.UI.Text>();

                buttonTitleText.text = ForwardButton ? (pageIndex == pageMax - 1 ? "<color=#E87920>></color>" : ">") : (pageIndex == 0 ? "<color=#E87920><</color>" : "<");
                buttonTitleText.font = Globals.Consolas;
                buttonTitleText.alignment = TextAnchor.MiddleCenter;
                buttonTitleText.color = Color.white;
                buttonTitleText.resizeTextForBestFit = true;
                buttonTitleText.resizeTextMinSize = 0;

                buttonTitleText.transform.localScale = new Vector3(0.08f, 0.08f, 0.02f);
                buttonTitleText.transform.localPosition = new Vector3(0.005f, 0f, 0f);
                buttonTitleText.transform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                if (ForwardButton) ForwardPageButton = currentObject;
                else BackPageButton = currentObject;

            }

            if (currentObject == null)
            {
                if (ForwardButton) currentObject = ForwardPageButton;
                else currentObject = BackPageButton;
            }

            currentObject.transform.position = menuObject.transform.position;
            currentObject.transform.rotation = menuObject.transform.rotation;
            currentObject.transform.localPosition = new Vector3(1f, ForwardButton ? -0.275f : 0.275f, .25f - 0.75f);

            currentObject.GetComponentInChildren<UnityEngine.UI.Text>().text = ForwardButton ? (pageIndex == pageMax - 1 ? "<color=#E87920>></color>" : ">") : (pageIndex == 0 ? "<color=#E87920><</color>" : "<");

            currentObject.transform.SetParent(menuObject.transform);
        }

        public static void DrawTooltipButton(int buttonIndex, string moduleId)
        {
            Module module = ModuleHandler.GetModule(moduleId);
            if (module == null) return;

            if (TooltipButtonsObjects.Count < buttonIndex + 1)
            {
                TooltipButtonsObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
                TooltipButtonsObjects[buttonIndex].transform.localScale = new Vector3(0.01f, 0.038f, 0.038f);

                GameObject.Destroy(TooltipButtonsObjects[buttonIndex].GetComponent<Rigidbody>());
                TooltipButtonsObjects[buttonIndex].GetComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                TooltipButtonsObjects[buttonIndex].GetComponent<MeshRenderer>().material.color = Color.black;

                TooltipButtonsObjects[buttonIndex].GetComponent<BoxCollider>().isTrigger = true;
                TooltipButtonsObjects[buttonIndex].AddComponent<TooltipButtonComponent>().ModuleId = moduleId;

                GameObject canvasObj = new GameObject();
                canvasObj.transform.parent = TooltipButtonsObjects[buttonIndex].transform;
                canvasObj.transform.localPosition = new Vector3(0.5f, 0f, 0f);

                Canvas canvas = canvasObj.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;

                GameObject buttonTitleTextObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObj.transform
                    }
                };

                buttonTitleTextObject.AddComponent<CanvasRenderer>();
                UnityEngine.UI.Text buttonTitleText = buttonTitleTextObject.AddComponent<UnityEngine.UI.Text>();

                buttonTitleText.text = "<color=#00ff6c>?</color>";
                buttonTitleText.font = Globals.Consolas;
                buttonTitleText.alignment = TextAnchor.MiddleCenter;
                buttonTitleText.color = Color.white;
                buttonTitleText.resizeTextForBestFit = true;
                buttonTitleText.resizeTextMinSize = 0;

                buttonTitleText.transform.localScale = new Vector3(0.06f, 0.06f, 0.015f);
                buttonTitleText.transform.localPosition = new Vector3(0.005f, 0f, 0f);
                buttonTitleText.transform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            TooltipButtonsObjects[buttonIndex].transform.position = menuObject.transform.position;
            TooltipButtonsObjects[buttonIndex].transform.rotation = menuObject.transform.rotation;
            TooltipButtonsObjects[buttonIndex].transform.localPosition = new Vector3(1f, -0.6f, 0.2f - (buttonIndex * 0.12f));

            TooltipButtonsObjects[buttonIndex].transform.SetParent(menuObject.transform);

            TooltipButtonsObjects[buttonIndex].GetComponentInChildren<UnityEngine.UI.Text>().color = module.State ? rgbColorSlow : Color.white;
        }

        #endregion Buttons  
    }
}
