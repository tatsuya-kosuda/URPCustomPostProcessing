using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public abstract class CustomPostProcessingPass : ScriptableRenderPass
    {

        protected virtual string RenderTag => "";

        protected virtual string ShaderName => "";

        protected int SourceId
        {
            get; private set;
        }

        protected static readonly int TEMP_ID = Shader.PropertyToID("_TempTarget");

        protected virtual CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<CustomPostProcessing>();

        protected Material _mat;

        public CustomPostProcessingPass(RenderPassEvent ev)
        {
            renderPassEvent = ev;
            var shader = Shader.Find(ShaderName);

            if (shader == null) { return; }

            _mat = CoreUtils.CreateEngineMaterial(shader);

            switch (ev)
            {
                case RenderPassEvent.AfterRenderingPostProcessing:
                    //SourceId = Shader.PropertyToID("_AfterPostProcessTexture");
                    SourceId = Shader.PropertyToID("_CameraColorAttachmentA");
                    break;
                case RenderPassEvent.BeforeRenderingPostProcessing:
                default:
                    SourceId = Shader.PropertyToID("_CameraColorTexture");
                    break;
            }
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_mat == null ||
                renderingData.cameraData.postProcessEnabled == false) { return; }

            if (CustomPostProcessing == null) { return; }

            if (CustomPostProcessing.IsActive() == false)
            {
                OnDisable();
                return;
            }

            var cmd = CommandBufferPool.Get(RenderTag);
            Render(cmd, ref renderingData);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        protected virtual void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            int source = SourceId;
            int dest = TEMP_ID;
            var h = cameraData.camera.scaledPixelHeight;
            var w = cameraData.camera.scaledPixelWidth;
            cmd.GetTemporaryRT(source, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.GetTemporaryRT(dest, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, dest);
            cmd.Blit(dest, source, _mat, 0);
            cmd.ReleaseTemporaryRT(dest);
        }

        protected virtual void OnDisable()
        {
        }

    }
}
