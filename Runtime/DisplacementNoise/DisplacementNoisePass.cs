using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class DisplacementNoisePass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<DisplacementNoise>();

        private DisplacementNoise _displacementNoise;

        protected override string RenderTag => "DisplacementNoise Pass";

        protected override string ShaderName => "Shader Graphs/DisplacementNoise";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");

        private static readonly int SPEED_ID = Shader.PropertyToID("_Speed");

        private static readonly int NOISE_SCALE_ID = Shader.PropertyToID("_NoiseScale");

        public DisplacementNoisePass(RenderPassEvent ev) : base(ev) { }

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

            if (_displacementNoise == null) { _displacementNoise = CustomPostProcessing as DisplacementNoise; }

            _mat.SetFloat(INTENSITY_ID, _displacementNoise.Intensity.value);
            _mat.SetFloat(SPEED_ID, _displacementNoise.Speed.value);
            _mat.SetFloat(NOISE_SCALE_ID, _displacementNoise.NoiseScale.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
