using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class BorderNoisePass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<BorderNoise>();

        private BorderNoise _borderNoise;

        protected override string RenderTag => "BorderNoise Pass";

        protected override string ShaderName => "Shader Graphs/BorderNoise";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");
        private static readonly int NOISE_SCALE_ID = Shader.PropertyToID("_NoiseScale");

        public BorderNoisePass(RenderPassEvent ev) : base(ev) { }

        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            var source = SourceId;
            var tmp = TEMP_ID;
            var h = cameraData.camera.scaledPixelHeight;
            var w = cameraData.camera.scaledPixelWidth;
            cmd.GetTemporaryRT(source, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.GetTemporaryRT(tmp, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, tmp);

            if (_borderNoise == null) { _borderNoise = CustomPostProcessing as BorderNoise; }

            _mat.SetFloat(INTENSITY_ID, _borderNoise.Intensity.value);
            _mat.SetFloat(NOISE_SCALE_ID, _borderNoise.NoiseScale.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
