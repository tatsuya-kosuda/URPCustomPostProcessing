using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class CrossHatchingPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<CrossHatching>();

        private CrossHatching _crossHatching;

        protected override string RenderTag => "CrossHatching Pass";

        protected override string ShaderName => "Shader Graphs/CrossHatching";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");
        private static readonly int DENSITY_ID = Shader.PropertyToID("_Density");
        private static readonly int LINE_ID = Shader.PropertyToID("_LineWidth");
        private static readonly int COLOR_ID = Shader.PropertyToID("_LineColor");

        public CrossHatchingPass(RenderPassEvent ev) : base(ev) { }

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

            if (_crossHatching == null) { _crossHatching = CustomPostProcessing as CrossHatching; }

            _mat.SetFloat(INTENSITY_ID, _crossHatching.Intensity.value);
            _mat.SetFloat(DENSITY_ID, _crossHatching.Density.value);
            _mat.SetFloat(LINE_ID, _crossHatching.LineWidth.value);
            _mat.SetColor(COLOR_ID, _crossHatching.LineColor.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
