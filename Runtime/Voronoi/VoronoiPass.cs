using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class VoronoiPass : CustomPostProcessingPass
    {

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<Voronoi>();

        private Voronoi _voronoi;

        protected override string RenderTag => "Voronoi Pass";

        protected override string ShaderName => "Shader Graphs/Voronoi";

        private static readonly int CELL_SIZE_ID = Shader.PropertyToID("_CellSize");

        private static readonly int SPEED_ID = Shader.PropertyToID("_Speed");
        private static readonly int SPEED2_ID = Shader.PropertyToID("_Speed2");

        private static readonly int OFFSET_ID = Shader.PropertyToID("_Offset");

        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");

        public VoronoiPass(RenderPassEvent ev) : base(ev) { }

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

            if (_voronoi == null) { _voronoi = CustomPostProcessing as Voronoi; }

            _mat.SetFloat(CELL_SIZE_ID, _voronoi.CellSize.value);
            _mat.SetVector(OFFSET_ID, _voronoi.Offset.value);
            _mat.SetFloat(SPEED_ID, _voronoi.Speed.value);
            _mat.SetFloat(SPEED2_ID, _voronoi.Speed2.value);
            _mat.SetFloat(INTENSITY_ID, _voronoi.Intensity.value);
            cmd.Blit(tmp, source, _mat, 0);
            cmd.ReleaseTemporaryRT(tmp);
        }

    }
}
