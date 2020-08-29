using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class PsycedelicPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<Psycedelic>();

        private Psycedelic _psycedelic;

        protected override string RenderTag => "PSYCEDELIC";

        protected override string ShaderName => "Shader Graphs/Psycedelic";

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");
        private static readonly int NOISE_SCALE_ID = Shader.PropertyToID("_NoiseScale");

        public PsycedelicPass(RenderPassEvent ev) : base(ev)
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

            if (_psycedelic == null) { _psycedelic = CustomPostProcessing as Psycedelic; }

            _mat.SetFloat(INTENSITY_ID, _psycedelic.Intensity.value);
            _mat.SetFloat(NOISE_SCALE_ID, _psycedelic.NoiseScale.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
