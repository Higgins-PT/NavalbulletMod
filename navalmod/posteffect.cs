using System;
using UnityEngine;

namespace Navalmod
{
    // Token: 0x02000009 RID: 9
    internal class PostEffect : MonoBehaviour
    {
        // Token: 0x06000042 RID: 66 RVA: 0x00004709 File Offset: 0x00002909
        private void Start()
        {
            this._camera = base.GetComponent<Camera>();
            this._camera.depthTextureMode |= DepthTextureMode.Depth;
        }

        // Token: 0x06000043 RID: 67 RVA: 0x0000472C File Offset: 0x0000292C
        private void Update()
        {
            Matrix4x4 worldToCameraMatrix = this._camera.worldToCameraMatrix;
            Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this._camera.projectionMatrix, false);
            Matrix4x4 matrix = gpuprojectionMatrix * worldToCameraMatrix;
            this.mat.SetMatrix("_ViewProj", matrix);
            this.mat.SetMatrix("_InvViewProj", matrix.inverse);
        }

        // Token: 0x06000044 RID: 68 RVA: 0x0000478A File Offset: 0x0000298A
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, this.mat);
        }

        // Token: 0x0400004A RID: 74
        public Shader shader;

        // Token: 0x0400004B RID: 75
        public Material mat;

        // Token: 0x0400004C RID: 76
        private Camera _camera;
    }
}
