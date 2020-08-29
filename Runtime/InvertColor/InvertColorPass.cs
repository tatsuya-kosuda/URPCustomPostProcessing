using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class InvertColorPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<InvertColor>();

        private InvertColor _invertColor;

        protected override string RenderTag => "Invert Color Pass";

        protected override string ShaderName => "Shader Graphs/InvertColor";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");

        public InvertColorPass(RenderPassEvent ev) : base(ev) { }

        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            int source = SourceId;
            int tmp = TEMP_ID;
            var h = cameraData.camera.scaledPixelHeight;
            var w = cameraData.camera.scaledPixelWidth;
            cmd.GetTemporaryRT(source, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.GetTemporaryRT(tmp, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);

            if (_invertColor == null) { _invertColor = CustomPostProcessing as InvertColor; }

            _mat.SetFloat(INTENSITY_ID, _invertColor.Intensity.value);
            cmd.Blit(source, tmp);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}

