using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navalmod
{
    // Token: 0x0200002D RID: 45
    public class Water : MonoBehaviour
    {
        // Token: 0x06000137 RID: 311 RVA: 0x00012848 File Offset: 0x00010A48
        public void OnWillRenderObject()
        {
            bool flag = !base.enabled || !base.GetComponent<Renderer>() || !base.GetComponent<Renderer>().sharedMaterial || !base.GetComponent<Renderer>().enabled;
            if (!flag)
            {
                Camera current = Camera.current;
                bool flag2 = !current;
                if (!flag2)
                {
                    bool flag3 = Water.s_InsideWater;
                    if (!flag3)
                    {
                        Water.s_InsideWater = true;
                        this.m_HardwareWaterSupport = this.FindHardwareWaterSupport();
                        Water.WaterMode waterMode = this.GetWaterMode();
                        Camera camera;
                        Camera camera2;
                        this.CreateWaterObjects(current, out camera, out camera2);
                        Vector3 position = base.transform.position;
                        Vector3 up = base.transform.up;
                        int pixelLightCount = QualitySettings.pixelLightCount;
                        bool flag4 = this.disablePixelLights;
                        if (flag4)
                        {
                            QualitySettings.pixelLightCount = 0;
                        }
                        this.UpdateCameraModes(current, camera);
                        this.UpdateCameraModes(current, camera2);
                        bool flag5 = waterMode >= Water.WaterMode.Reflective;
                        if (flag5)
                        {
                            float w = -Vector3.Dot(up, position) - this.clipPlaneOffset;
                            Vector4 plane = new Vector4(up.x, up.y, up.z, w);
                            Matrix4x4 zero = Matrix4x4.zero;
                            Water.CalculateReflectionMatrix(ref zero, plane);
                            Vector3 position2 = current.transform.position;
                            Vector3 position3 = zero.MultiplyPoint(position2);
                            camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
                            Vector4 clipPlane = this.CameraSpacePlane(camera, position, up, 1f);
                            camera.projectionMatrix = current.CalculateObliqueMatrix(clipPlane);
                            camera.cullingMatrix = current.projectionMatrix * current.worldToCameraMatrix;
                            camera.cullingMask = (-17 & this.reflectLayers.value);
                            camera.targetTexture = this.m_ReflectionTexture;
                            bool invertCulling = GL.invertCulling;
                            GL.invertCulling = !invertCulling;
                            camera.transform.position = position3;
                            Vector3 eulerAngles = current.transform.eulerAngles;
                            camera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
                            camera.Render();
                            camera.transform.position = position2;
                            GL.invertCulling = invertCulling;
                            base.GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
                        }
                        bool flag6 = waterMode >= Water.WaterMode.Refractive;
                        if (flag6)
                        {
                            camera2.worldToCameraMatrix = current.worldToCameraMatrix;
                            Vector4 clipPlane2 = this.CameraSpacePlane(camera2, position, up, -1f);
                            camera2.projectionMatrix = current.CalculateObliqueMatrix(clipPlane2);
                            camera2.cullingMatrix = current.projectionMatrix * current.worldToCameraMatrix;
                            camera2.cullingMask = (-17 & this.refractLayers.value);
                            camera2.targetTexture = this.m_RefractionTexture;
                            camera2.transform.position = current.transform.position;
                            camera2.transform.rotation = current.transform.rotation;
                            camera2.Render();
                            base.GetComponent<Renderer>().sharedMaterial.SetTexture("_RefractionTex", this.m_RefractionTexture);
                        }
                        bool flag7 = this.disablePixelLights;
                        if (flag7)
                        {
                            QualitySettings.pixelLightCount = pixelLightCount;
                        }
                        switch (waterMode)
                        {
                            case Water.WaterMode.Simple:
                                Shader.EnableKeyword("WATER_SIMPLE");
                                Shader.DisableKeyword("WATER_REFLECTIVE");
                                Shader.DisableKeyword("WATER_REFRACTIVE");
                                break;
                            case Water.WaterMode.Reflective:
                                Shader.DisableKeyword("WATER_SIMPLE");
                                Shader.EnableKeyword("WATER_REFLECTIVE");
                                Shader.DisableKeyword("WATER_REFRACTIVE");
                                break;
                            case Water.WaterMode.Refractive:
                                Shader.DisableKeyword("WATER_SIMPLE");
                                Shader.DisableKeyword("WATER_REFLECTIVE");
                                Shader.EnableKeyword("WATER_REFRACTIVE");
                                break;
                        }
                        Water.s_InsideWater = false;
                    }
                }
            }
        }

        // Token: 0x06000138 RID: 312 RVA: 0x00012C08 File Offset: 0x00010E08
        private void OnDisable()
        {
            bool flag = this.m_ReflectionTexture;
            if (flag)
            {
                UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
                this.m_ReflectionTexture = null;
            }
            bool flag2 = this.m_RefractionTexture;
            if (flag2)
            {
                UnityEngine.Object.DestroyImmediate(this.m_RefractionTexture);
                this.m_RefractionTexture = null;
            }
            foreach (KeyValuePair<Camera, Camera> keyValuePair in this.m_ReflectionCameras)
            {
                UnityEngine.Object.DestroyImmediate(keyValuePair.Value.gameObject);
            }
            this.m_ReflectionCameras.Clear();
            foreach (KeyValuePair<Camera, Camera> keyValuePair2 in this.m_RefractionCameras)
            {
                UnityEngine.Object.DestroyImmediate(keyValuePair2.Value.gameObject);
            }
            this.m_RefractionCameras.Clear();
        }

        // Token: 0x06000139 RID: 313 RVA: 0x00012D20 File Offset: 0x00010F20
        private void Update()
        {
            bool flag = !base.GetComponent<Renderer>();
            if (!flag)
            {
                Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
                bool flag2 = !sharedMaterial;
                if (!flag2)
                {
                    Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
                    float @float = sharedMaterial.GetFloat("_WaveScale");
                    Vector4 vector2 = new Vector4(@float, @float, @float * 0.4f, @float * 0.45f);
                    double num = (double)Time.timeSinceLevelLoad / 20.0;
                    Vector4 vector3 = new Vector4((float)Math.IEEERemainder((double)(vector.x * vector2.x) * num, 1.0), (float)Math.IEEERemainder((double)(vector.y * vector2.y) * num, 1.0), (float)Math.IEEERemainder((double)(vector.z * vector2.z) * num, 1.0), (float)Math.IEEERemainder((double)(vector.w * vector2.w) * num, 1.0));
                    sharedMaterial.SetVector("_WaveOffset", vector3);
                    sharedMaterial.SetVector("_WaveScale4", vector2);
                }
            }
        }

        // Token: 0x0600013A RID: 314 RVA: 0x00012E4C File Offset: 0x0001104C
        private void UpdateCameraModes(Camera src, Camera dest)
        {
            bool flag = dest == null;
            if (!flag)
            {
                dest.clearFlags = src.clearFlags;
                dest.backgroundColor = src.backgroundColor;
                bool flag2 = src.clearFlags == CameraClearFlags.Skybox;
                if (flag2)
                {
                    Skybox component = src.GetComponent<Skybox>();
                    Skybox component2 = dest.GetComponent<Skybox>();
                    bool flag3 = !component || !component.material;
                    if (flag3)
                    {
                        component2.enabled = false;
                    }
                    else
                    {
                        component2.enabled = true;
                        component2.material = component.material;
                    }
                }
                dest.farClipPlane = src.farClipPlane;
                dest.nearClipPlane = src.nearClipPlane;
                dest.orthographic = src.orthographic;
                dest.fieldOfView = src.fieldOfView;
                dest.aspect = src.aspect;
                dest.orthographicSize = src.orthographicSize;
            }
        }

        // Token: 0x0600013B RID: 315 RVA: 0x00012F34 File Offset: 0x00011134
        private void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera, out Camera refractionCamera)
        {
            Water.WaterMode waterMode = this.GetWaterMode();
            reflectionCamera = null;
            refractionCamera = null;
            bool flag = waterMode >= Water.WaterMode.Reflective;
            if (flag)
            {
                bool flag2 = !this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.textureSize;
                if (flag2)
                {
                    bool flag3 = this.m_ReflectionTexture;
                    if (flag3)
                    {
                        UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
                    }
                    this.m_ReflectionTexture = new RenderTexture(this.textureSize, this.textureSize, 16);
                    this.m_ReflectionTexture.name = "__WaterReflection" + base.GetInstanceID().ToString();
                    this.m_ReflectionTexture.isPowerOfTwo = true;
                    this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
                    this.m_OldReflectionTextureSize = this.textureSize;
                }
                this.m_ReflectionCameras.TryGetValue(currentCamera, out reflectionCamera);
                bool flag4 = !reflectionCamera;
                if (flag4)
                {
                    GameObject gameObject = new GameObject("Water Refl Camera id" + base.GetInstanceID().ToString() + " for " + currentCamera.GetInstanceID().ToString(), new Type[]
                    {
                        typeof(Camera),
                        typeof(Skybox)
                    });
                    reflectionCamera = gameObject.GetComponent<Camera>();
                    reflectionCamera.enabled = false;
                    reflectionCamera.transform.position = base.transform.position;
                    reflectionCamera.transform.rotation = base.transform.rotation;
                    reflectionCamera.gameObject.AddComponent<FlareLayer>();
                    gameObject.hideFlags = HideFlags.HideAndDontSave;
                    this.m_ReflectionCameras[currentCamera] = reflectionCamera;
                }
            }
            bool flag5 = waterMode >= Water.WaterMode.Refractive;
            if (flag5)
            {
                bool flag6 = !this.m_RefractionTexture || this.m_OldRefractionTextureSize != this.textureSize;
                if (flag6)
                {
                    bool flag7 = this.m_RefractionTexture;
                    if (flag7)
                    {
                        UnityEngine.Object.DestroyImmediate(this.m_RefractionTexture);
                    }
                    this.m_RefractionTexture = new RenderTexture(this.textureSize, this.textureSize, 16);
                    this.m_RefractionTexture.name = "__WaterRefraction" + base.GetInstanceID().ToString();
                    this.m_RefractionTexture.isPowerOfTwo = true;
                    this.m_RefractionTexture.hideFlags = HideFlags.DontSave;
                    this.m_OldRefractionTextureSize = this.textureSize;
                }
                this.m_RefractionCameras.TryGetValue(currentCamera, out refractionCamera);
                bool flag8 = !refractionCamera;
                if (flag8)
                {
                    GameObject gameObject2 = new GameObject("Water Refr Camera id" + base.GetInstanceID().ToString() + " for " + currentCamera.GetInstanceID().ToString(), new Type[]
                    {
                        typeof(Camera),
                        typeof(Skybox)
                    });
                    refractionCamera = gameObject2.GetComponent<Camera>();
                    refractionCamera.enabled = false;
                    refractionCamera.transform.position = base.transform.position;
                    refractionCamera.transform.rotation = base.transform.rotation;
                    refractionCamera.gameObject.AddComponent<FlareLayer>();
                    gameObject2.hideFlags = HideFlags.HideAndDontSave;
                    this.m_RefractionCameras[currentCamera] = refractionCamera;
                }
            }
        }

        // Token: 0x0600013C RID: 316 RVA: 0x0001328C File Offset: 0x0001148C
        private Water.WaterMode GetWaterMode()
        {
            bool flag = this.m_HardwareWaterSupport < this.waterMode;
            Water.WaterMode hardwareWaterSupport;
            if (flag)
            {
                hardwareWaterSupport = this.m_HardwareWaterSupport;
            }
            else
            {
                hardwareWaterSupport = this.waterMode;
            }
            return hardwareWaterSupport;
        }

        // Token: 0x0600013D RID: 317 RVA: 0x000132C0 File Offset: 0x000114C0
        private Water.WaterMode FindHardwareWaterSupport()
        {
            bool flag = !SystemInfo.supportsRenderTextures || !base.GetComponent<Renderer>();
            Water.WaterMode result;
            if (flag)
            {
                result = Water.WaterMode.Simple;
            }
            else
            {
                Material sharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
                bool flag2 = !sharedMaterial;
                if (flag2)
                {
                    result = Water.WaterMode.Simple;
                }
                else
                {
                    string tag = sharedMaterial.GetTag("WATERMODE", false);
                    bool flag3 = tag == "Refractive";
                    if (flag3)
                    {
                        result = Water.WaterMode.Refractive;
                    }
                    else
                    {
                        bool flag4 = tag == "Reflective";
                        if (flag4)
                        {
                            result = Water.WaterMode.Reflective;
                        }
                        else
                        {
                            result = Water.WaterMode.Simple;
                        }
                    }
                }
            }
            return result;
        }

        // Token: 0x0600013E RID: 318 RVA: 0x00013350 File Offset: 0x00011550
        private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
        {
            Vector3 v = pos + normal * this.clipPlaneOffset;
            Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
            Vector3 lhs = worldToCameraMatrix.MultiplyPoint(v);
            Vector3 vector = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
            return new Vector4(vector.x, vector.y, vector.z, -Vector3.Dot(lhs, vector));
        }

        // Token: 0x0600013F RID: 319 RVA: 0x000133C0 File Offset: 0x000115C0
        private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
        {
            reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
            reflectionMat.m01 = -2f * plane[0] * plane[1];
            reflectionMat.m02 = -2f * plane[0] * plane[2];
            reflectionMat.m03 = -2f * plane[3] * plane[0];
            reflectionMat.m10 = -2f * plane[1] * plane[0];
            reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
            reflectionMat.m12 = -2f * plane[1] * plane[2];
            reflectionMat.m13 = -2f * plane[3] * plane[1];
            reflectionMat.m20 = -2f * plane[2] * plane[0];
            reflectionMat.m21 = -2f * plane[2] * plane[1];
            reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
            reflectionMat.m23 = -2f * plane[3] * plane[2];
            reflectionMat.m30 = 0f;
            reflectionMat.m31 = 0f;
            reflectionMat.m32 = 0f;
            reflectionMat.m33 = 1f;
        }

        // Token: 0x0400014E RID: 334
        public Water.WaterMode waterMode = Water.WaterMode.Refractive;

        // Token: 0x0400014F RID: 335
        public bool disablePixelLights = true;

        // Token: 0x04000150 RID: 336
        public int textureSize = 256;

        // Token: 0x04000151 RID: 337
        public float clipPlaneOffset = 0.07f;

        // Token: 0x04000152 RID: 338
        public LayerMask reflectLayers = -1;

        // Token: 0x04000153 RID: 339
        public LayerMask refractLayers = -1;

        // Token: 0x04000154 RID: 340
        private Dictionary<Camera, Camera> m_ReflectionCameras = new Dictionary<Camera, Camera>();

        // Token: 0x04000155 RID: 341
        private Dictionary<Camera, Camera> m_RefractionCameras = new Dictionary<Camera, Camera>();

        // Token: 0x04000156 RID: 342
        private RenderTexture m_ReflectionTexture;

        // Token: 0x04000157 RID: 343
        private RenderTexture m_RefractionTexture;

        // Token: 0x04000158 RID: 344
        private Water.WaterMode m_HardwareWaterSupport = Water.WaterMode.Refractive;

        // Token: 0x04000159 RID: 345
        private int m_OldReflectionTextureSize;

        // Token: 0x0400015A RID: 346
        private int m_OldRefractionTextureSize;

        // Token: 0x0400015B RID: 347
        private static bool s_InsideWater;

        // Token: 0x0200003D RID: 61
        public enum WaterMode
        {
            // Token: 0x040001B2 RID: 434
            Simple,
            // Token: 0x040001B3 RID: 435
            Reflective,
            // Token: 0x040001B4 RID: 436
            Refractive
        }
    }
}
