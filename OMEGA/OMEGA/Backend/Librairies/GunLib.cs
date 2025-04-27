using Backtrace.Unity.Model.Breadcrumbs;
using Fusion;
using GorillaExtensions;
using ModIO.Implementation.API;
using Pathfinding.RVO;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OMEGA.Backend.Librairies
{
    internal static class GunLib
    {
        public static GameObject pointerGO;
        private static GameObject linePointerGO;
        private static VRRig lockedPlayer;

        public static bool IsShooting = false;
        public static bool IsPointing = false;
        public static GunType Type;

        private static void DrawLine()
        {
            linePointerGO = new GameObject();
            LineRenderer line = linePointerGO.AddComponent<LineRenderer>();
            line.material.shader = Shader.Find("GUI/Text Shader");
            line.material.color = Globals.GetMainThemeColor();
            line.startWidth = 0.013f;
            line.endWidth = 0.013f;
            line.positionCount = 2;
            line.useWorldSpace = true;
        }

        private static void DestroyLine()
        {
            GameObject.Destroy(linePointerGO);
            linePointerGO = null;
        }

        private static void DrawPointer()
        {
            pointerGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            GameObject.Destroy(pointerGO.GetComponent<SphereCollider>());
            GameObject.Destroy(pointerGO.GetComponent<Rigidbody>());

            pointerGO.GetComponent<MeshRenderer>().material.shader = Shader.Find("GUI/Text Shader");
            pointerGO.GetComponent<MeshRenderer>().material.color = Globals.GetMainThemeColor();

            pointerGO.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        private static void ResetPointerColor() =>
            pointerGO.GetComponent<MeshRenderer>().material.color = Globals.GetMainThemeColor();
        private static void SetPointerDarkerCol() =>
           pointerGO.GetComponent<MeshRenderer>().material.color = new Color32(46, 0, 157, 255);

        private static void ResetLineColor() =>
            linePointerGO.GetComponent<LineRenderer>().material.color = Globals.GetMainThemeColor();

        private static void SetLineDarkerCol() =>
           linePointerGO.GetComponent<LineRenderer>().material.color = new Color32(46, 0, 157, 255);


        private static void ResetPointer() =>
            GameObject.Destroy(pointerGO);

        private static void ProcessHitInfo(RaycastHit hitInfo, ResultType restype, Action<object> callback)
        {
            VRRig rig = hitInfo.collider.GetComponent<VRRig>();
            if(rig && restype != ResultType.Position) lockedPlayer = rig;

            switch (restype)
            {
                case ResultType.Position:
                    callback(hitInfo.point); break;

                case ResultType.VRRig:
                    if (lockedPlayer) callback(lockedPlayer);
                    break;

                case ResultType.Player:
                    if (!lockedPlayer) break;

                    Photon.Realtime.Player player = RigManager.GetPlayerFromRig(lockedPlayer);
                    callback(player);
                    break;
            }
        }

        public static void EmulateGun(ResultType restype, Action<object> callback)
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Type = GunType.VRGun;
                IsPointing = true;
            }

            if (Mouse.current.rightButton.value == 1f)
            {
                Type = GunType.MouseGun;
                IsPointing = true;
            }

            if (IsPointing)
            {
                if (!pointerGO) DrawPointer();

                Ray ray;
                RaycastHit hitInfo;

                switch (Type)
                {
                    case GunType.VRGun:
                        ray = new Ray(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.up);

                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            if (!linePointerGO) DrawLine();
                            LineRenderer line = linePointerGO.GetComponent<LineRenderer>();
                            if (!lockedPlayer)
                            {
                                line.SetPositions(new Vector3[] { GorillaTagger.Instance.rightHandTransform.position, hitInfo.point });
                                pointerGO.transform.position = hitInfo.point;
                            }

                            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.8f)
                            {
                                IsShooting = true;

                                SetPointerDarkerCol();
                                SetLineDarkerCol();

                                if(lockedPlayer)
                                {
                                    line.SetPositions(new Vector3[] { GorillaTagger.Instance.rightHandTransform.position, lockedPlayer.bodyTransform.position });
                                    pointerGO.transform.position = lockedPlayer.bodyTransform.position;
                                }

                                ProcessHitInfo(hitInfo, restype, callback);
                            } else
                            {
                                lockedPlayer = null;
                                IsShooting = false;

                                ResetPointerColor();
                                ResetLineColor();
                            }
                        }
                        else return;

                        break;

                    case GunType.MouseGun:
                        ray = Camera.main.ScreenPointToRay(Mouse.current.position.value.WithZ(0f));
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            if (!linePointerGO) DrawLine();
                            LineRenderer line = linePointerGO.GetComponent<LineRenderer>();
                            if (!lockedPlayer)
                            {
                                line.SetPositions(new Vector3[] { GorillaTagger.Instance.offlineVRRig.transform.position, hitInfo.point });
                                pointerGO.transform.position = hitInfo.point;
                            }

                            if (Mouse.current.leftButton.value == 1f)
                            {
                                if (lockedPlayer)
                                {
                                    line.SetPositions(new Vector3[] { GorillaTagger.Instance.offlineVRRig.transform.position, lockedPlayer.bodyTransform.position });
                                    pointerGO.transform.position = lockedPlayer.bodyTransform.position;
                                }

                                IsShooting = true;

                                SetPointerDarkerCol();
                                SetLineDarkerCol();

                                ProcessHitInfo(hitInfo, restype, callback);
                            }
                            else
                            {
                                lockedPlayer = null;
                                IsShooting = false;

                                ResetPointerColor();
                                ResetLineColor();
                            }
                        }
                        else return;

                        break;

                    default:
                        return;
                }
            }
            else
            {
                ResetPointer();
                DestroyLine();

                lockedPlayer = null;
            }

            if (!ControllerInputPoller.instance.rightGrab && Mouse.current.rightButton.value == 0f)
                IsPointing = false;
        }

        internal enum GunType
        {
            MouseGun,
            VRGun
        }

        internal enum ResultType
        {
            Player,
            VRRig,
            Position
        }
    }
}
