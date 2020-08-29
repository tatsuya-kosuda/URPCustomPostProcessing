using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class GlitchPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<Glitch>();

        private Glitch _glitch;

        protected override string RenderTag => "Glitch Pass";

        protected override string ShaderName => "Shader Graphs/Glitch";

        private static readonly int SPEED_ID = Shader.PropertyToID("_Speed");

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");

        public GlitchPass(RenderPassEvent ev) : base(ev) { }

        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            var source = SourceId;
            var dest = TEMP_ID;
            var h = cameraData.camera.scaledPixelHeight;
            var w = cameraData.camera.scaledPixelWidth;
            cmd.GetTemporaryRT(source, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.GetTemporaryRT(dest, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, dest);

            if (_glitch == null) { _glitch = CustomPostProcessing as Glitch; }

            _mat.SetFloat(SPEED_ID, _glitch.Speed.value);
            _mat.SetVector(INTENSITY_ID, _glitch.Intensity.value);
            cmd.Blit(dest, source, _mat, 0);
            cmd.ReleaseTemporaryRT(dest);
        }

    }
}
