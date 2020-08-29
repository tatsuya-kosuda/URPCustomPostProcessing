using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class NoiseColorPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<NoiseColor>();

        private NoiseColor _noiseColor;

        protected override string RenderTag => "Noise Color Pass";

        protected override string ShaderName => "Shader Graphs/NoiseColor";

        private static int THRESHOLD_ID = Shader.PropertyToID("_Threshold");
        private static int WAVE_SCALE_ID = Shader.PropertyToID("_WaveScale");
        private static int NOISE_DETAIL_ID = Shader.PropertyToID("_NoiseDetail");
        private static int INTENSITY_ID = Shader.PropertyToID("_Intensity");
        private static int HSV_OFFSET_ID = Shader.PropertyToID("_HSVOffset");

        public NoiseColorPass(RenderPassEvent ev) : base(ev)
        {
        }

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

            if (_noiseColor == null) { _noiseColor = CustomPostProcessing as NoiseColor; }

            _mat.SetFloat(THRESHOLD_ID, _noiseColor.Threshold.value);
            _mat.SetFloat(WAVE_SCALE_ID, _noiseColor.WaveScale.value);
            _mat.SetFloat(NOISE_DETAIL_ID, _noiseColor.NoiseDetail.value);
            _mat.SetFloat(INTENSITY_ID, _noiseColor.Intensity.value);
            _mat.SetVector(HSV_OFFSET_ID, _noiseColor.HSVOffset.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }
    }
}
