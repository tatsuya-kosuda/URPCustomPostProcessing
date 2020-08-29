using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class RetroGamePass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<RetroGame>();

        private RetroGame _retroGame;

        protected override string RenderTag => "RetroGame Pass";

        protected override string ShaderName => "Shader Graphs/RetroGameCrush";

        private static readonly int PIXEL_SIZE_ID = Shader.PropertyToID("_PixelSize");

        private static readonly int BRIGHTNESS_TH_ID = Shader.PropertyToID("_BrightnessThreshold");

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");

        public RetroGamePass(RenderPassEvent ev) : base(ev) { }

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

            if (_retroGame == null) { _retroGame = CustomPostProcessing as RetroGame; }

            _mat.SetFloat(PIXEL_SIZE_ID, _retroGame.PixelSize.value);
            _mat.SetFloat(BRIGHTNESS_TH_ID, _retroGame.BrightnessThreshold.value);
            _mat.SetFloat(INTENSITY_ID, _retroGame.Intensity.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
