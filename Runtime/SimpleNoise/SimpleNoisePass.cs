using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class SimpleNoisePass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<SimpleNoise>();

        private SimpleNoise _simpleNoise;

        protected override string RenderTag => "SimpleNoise Pass";

        protected override string ShaderName => "Shader Graphs/SimpleNoise";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");
        private static readonly int NOISE_SACLE_ID = Shader.PropertyToID("_NoiseScale");

        public SimpleNoisePass(RenderPassEvent ev) : base(ev) { }

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

            if (_simpleNoise == null) { _simpleNoise = CustomPostProcessing as SimpleNoise; }

            _mat.SetFloat(INTENSITY_ID, _simpleNoise.Intensity.value);
            _mat.SetFloat(NOISE_SACLE_ID, _simpleNoise.NoiseScale.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
