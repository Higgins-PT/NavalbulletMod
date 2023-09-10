using System;
using Modding;
using Navalmod;
using UnityEngine;

namespace Navalmod
{
    // Token: 0x0200000B RID: 11
    internal class SeaSurfacer : MonoBehaviour
    {
        // Token: 0x06000050 RID: 80 RVA: 0x00002E3F File Offset: 0x0000103F
        private void Awake()
        {
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00004A14 File Offset: 0x00002C14
        private void Start()
        {
            base.gameObject.name = "SeaSurfacer Object";
            RuntimePlatform platform = Application.platform;
            AssetBundle assetBundle;
            if (platform != RuntimePlatform.OSXPlayer)
            {
                if (platform != RuntimePlatform.WindowsPlayer)
                {
                    return;
                }
                assetBundle = ModResource.GetAssetBundle("water_asset_Windows").AssetBundle;
            }
            else
            {
                assetBundle = ModResource.GetAssetBundle("water_asset_OSXUniversal").AssetBundle;
            }
            this.material = assetBundle.LoadAsset<Material>("WaterProDaytime.mat");
            this.material.SetFloat("_WaveScale", 0.05f);
            this.material.SetFloat("_ReflDistort", 0.44f);
            this.material.SetFloat("_RefrDistort", 0.4f);
            this.water = base.gameObject.AddComponent<Water>();
            this.water.waterMode = Water.WaterMode.Refractive;
            this.water.clipPlaneOffset = -0.01f;
            this.water.reflectLayers = -8388609;
            this.mesh = base.gameObject.AddComponent<MeshFilter>().mesh;
            base.gameObject.AddComponent<MeshRenderer>().material = this.material;
            this.as_wave = base.gameObject.AddComponent<AudioSource>();
            this.as_wave.clip = ModResource.GetAudioClip("wave_sound");
            this.as_wave.loop = true;
            this.as_wave.volume = 0f;
            this.as_wave.Play();
            this.as_water = base.gameObject.AddComponent<AudioSource>();
            this.as_water.clip = ModResource.GetAudioClip("water_sound");
            this.as_water.loop = true;
            this.as_water.volume = 0f;
            this.as_water.Play();
            this.main_camera = Camera.main.gameObject;
            this.post_effect = this.main_camera.AddComponent<PostEffect>();
            this.post_effect.shader = assetBundle.LoadAsset<Shader>("UnderwaterPostEffect");
            this.post_effect.mat = new Material(this.post_effect.shader);
            this.post_effect.mat.SetFloat("_Cleanness", 50f);
            this.post_effect.mat.SetFloat("_Height", SingleInstance<Sea>.Instance.navalhe.seahigh);
            this.post_effect.mat.SetFloat("_Coefficient", 2f);
            this.post_effect.mat.SetInt("_InWater", (this.main_camera.transform.position.y < SingleInstance<Sea>.Instance.navalhe.seahigh) ? 1 : 0);
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00004CF4 File Offset: 0x00002EF4
        private void Update()
        {

                Vector3 position = base.transform.position;
                position.y = SingleInstance<Sea>.Instance.navalhe.seahigh;
                base.transform.position = position;

                    this.material.SetColor("_RefrColor", new Color(0.3f, 0.3f, 0.5f, 1f));
                    this.post_effect.mat.DisableKeyword("HeightFog");
                
                this.water.clipPlaneOffset = -0.01f;
                Vector3 position2 = this.main_camera.transform.position;
                float waterHeight = SingleInstance<Sea>.Instance.navalhe.seahigh;
                bool flag3 = position2.y < waterHeight;
                this.post_effect.mat.SetInt("_InWater", flag3 ? 1 : 0);

            
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00005018 File Offset: 0x00003218

        // Token: 0x04000051 RID: 81
        private const int x_num = 200;

        // Token: 0x04000052 RID: 82
        private const int z_num = 200;

        // Token: 0x04000053 RID: 83
        private const float scale = 10f;

        // Token: 0x04000054 RID: 84
        private Water water;

        // Token: 0x04000055 RID: 85
        private Mesh mesh;

        // Token: 0x04000056 RID: 86
        private Material material;

        // Token: 0x04000057 RID: 87
        private Vector3[] vertexes;

        // Token: 0x04000058 RID: 88
        private int[] index;

        // Token: 0x04000059 RID: 89
        private AudioSource as_wave;

        // Token: 0x0400005A RID: 90
        private AudioSource as_water;

        // Token: 0x0400005B RID: 91
        private GameObject main_camera;

        // Token: 0x0400005C RID: 92
        private PostEffect post_effect;
    }
}
