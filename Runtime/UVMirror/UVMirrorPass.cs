using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class UVMirrorPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<UVMirror>();

        private UVMirror _uvMirror;

        protected override string RenderTag => "UV Mirror Pass";

        protected override string ShaderName => "Shader Graphs/UVMirror";

        private static readonly int THRESHOLD_ID = Shader.PropertyToID("_MirrorThreshold");

        public UVMirrorPass(RenderPassEvent ev) : base(ev) { }

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

            if (_uvMirror == null) { _uvMirror = CustomPostProcessing as UVMirror; }

            _mat.SetVector(THRESHOLD_ID, _uvMirror.Threshold.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
