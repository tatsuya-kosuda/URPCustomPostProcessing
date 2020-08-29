using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class GlitchFreezeFramePass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<GlitchFreezeFrame>();

        private GlitchFreezeFrame _glitchFreezeFrame;

        protected override string RenderTag => "Freeze Frame Glitch Effect";

        protected override string ShaderName => "Shader Graphs/GlitchFreezeFrame";

        private static readonly int FREEZE_FRAME_ID = Shader.PropertyToID("_FreezeFrame");

        private static readonly int GLITCH_STRENGTH_ID = Shader.PropertyToID("_GlitchStrength");

        private static readonly int RANDOM_VALUE_ID = Shader.PropertyToID("_RandomValue");

        private static readonly int BLEND_ALPHA_ID = Shader.PropertyToID("_BlendAlpha");

        private RenderTexture _freezeRT;

        public GlitchFreezeFramePass(RenderPassEvent ev) : base(ev)
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

            if (_glitchFreezeFrame == null) { _glitchFreezeFrame = CustomPostProcessing as GlitchFreezeFrame; }

            _mat.SetFloat(GLITCH_STRENGTH_ID, _glitchFreezeFrame.glitchStrength.value);
            _mat.SetFloat(RANDOM_VALUE_ID, _glitchFreezeFrame.direction.value);
            _mat.SetFloat(BLEND_ALPHA_ID, 1);

            if (_mat.GetTexture(FREEZE_FRAME_ID) == null)
            {
                if (_freezeRT == null) 
                { 
                    _freezeRT = new RenderTexture(w, h, 0, RenderTextureFormat.Default); 
                    _freezeRT.wrapMode = TextureWrapMode.Repeat;
                }

                cmd.Blit(source, _freezeRT);
                _mat.SetTexture(FREEZE_FRAME_ID, _freezeRT);
            }

            cmd.Blit(source, tmp);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

        protected override void OnDisable()
        {
            _mat.SetFloat(BLEND_ALPHA_ID, 0);
            _mat.SetTexture(FREEZE_FRAME_ID, null);

            if (_freezeRT != null)
            {
                _freezeRT.Release();
            }
        }

    }
}
