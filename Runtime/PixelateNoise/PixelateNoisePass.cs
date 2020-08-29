using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class PixelateNoisePass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<PixelateNoise>();

        private PixelateNoise _pixelateNoise;

        protected override string RenderTag => "Pixelate Noise Pass";

        protected override string ShaderName => "Shader Graphs/PixelateNoise";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");
        private static readonly int HSV_ID = Shader.PropertyToID("_HSVOffset");

        public PixelateNoisePass(RenderPassEvent ev) : base(ev) { }

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

            if (_pixelateNoise == null) { _pixelateNoise = CustomPostProcessing as PixelateNoise; }

            _mat.SetFloat(INTENSITY_ID, _pixelateNoise.Intensity.value);
            _mat.SetVector(HSV_ID, _pixelateNoise.HSV.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
