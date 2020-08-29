using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class ZoomBlurPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<ZoomBlur>();

        private ZoomBlur _zoomBlur;

        protected override string RenderTag => "ZoomBlur Effect";

        protected override string ShaderName => "Shader Graphs/ZoomBlur";

        private static readonly int FOCUS_POWER_ID = Shader.PropertyToID("_FocusPower");

        private static readonly int FOCUS_DETAIL_ID = Shader.PropertyToID("_FocusDetail");

        private static readonly int FOCUS_SCREEN_POSITION_ID = Shader.PropertyToID("_FocusScreenPosition");

        public ZoomBlurPass(RenderPassEvent ev) : base(ev)
        {
        }

        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            var source = SourceId;
            int tmp = TEMP_ID;
            var h = cameraData.camera.scaledPixelHeight;
            var w = cameraData.camera.scaledPixelWidth;
            cmd.GetTemporaryRT(source, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.GetTemporaryRT(tmp, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, tmp);

            if (_zoomBlur == null) { _zoomBlur = CustomPostProcessing as ZoomBlur; }

            _mat.SetFloat(FOCUS_POWER_ID, _zoomBlur.focusPower.value);
            _mat.SetFloat(FOCUS_DETAIL_ID, _zoomBlur.focusDetail.value);
            _mat.SetVector(FOCUS_SCREEN_POSITION_ID, _zoomBlur.focusScreenPosition.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}

