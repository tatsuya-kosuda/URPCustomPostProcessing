using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class VideoFeedbackPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<VideoFeedback>();

        private VideoFeedback _videoFeedback;

        protected override string RenderTag => "VideoFeedback Pass";

        protected override string ShaderName => "Shader Graphs/VideoFeedback";

        private static readonly int REPEAT_ID = Shader.PropertyToID("_Repeat");

        public VideoFeedbackPass(RenderPassEvent ev) : base(ev) { }

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

            if (_videoFeedback == null) { _videoFeedback = CustomPostProcessing as VideoFeedback; }

            _mat.SetFloat(REPEAT_ID, _videoFeedback.Repeat.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
