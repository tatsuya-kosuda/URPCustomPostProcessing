using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class FrameCachePass : CustomPostProcessingPass
    {

        protected override string RenderTag => "Frame Cache Effect";

        protected override string ShaderName => "Shader Graphs/FrameCache";

        protected override CustomPostProcessing CustomPostProcessing => VolumeManager.instance.stack.GetComponent<FrameCache>();

        private FrameCache _frameCache;

        private static readonly int[] CACHE = new int[5]
        {
            Shader.PropertyToID("_Cache1"),
            Shader.PropertyToID("_Cache2"),
            Shader.PropertyToID("_Cache3"),
            Shader.PropertyToID("_Cache4"),
            Shader.PropertyToID("_Cache5"),
        };

        private static readonly int HSV_ID = Shader.PropertyToID("_HSVOffset");
        private static readonly int HSV_SPEED_ID = Shader.PropertyToID("_HSVOffsetSpeed");
        private static readonly int INTENSITY_ID = Shader.PropertyToID("_Intensity");

        private List<RenderTexture> _beforeFrames;

        private int _index = -1;

        public FrameCachePass(RenderPassEvent ev) : base(ev)
        {
        }

        protected override void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            int source = SourceId;
            int tmp = TEMP_ID;
            var h = cameraData.camera.scaledPixelHeight;
            var w = cameraData.camera.scaledPixelWidth;

            if (_frameCache == null) { _frameCache = CustomPostProcessing as FrameCache; }

            int step = _frameCache.Step.value;

            if (_frameCache.Intensity.value > 0)
            {
                cmd.GetTemporaryRT(source, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
                cmd.GetTemporaryRT(tmp, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
                cmd.Blit(source, tmp);
            }

            if (_beforeFrames == null)
            {
                // initialize
                _beforeFrames = new List<RenderTexture>();

                for (int i = 0; i < 5; i++)
                {
                    var elem = new RenderTexture(w / 3, h / 3, 0);

                    if (_frameCache.Intensity.value > 0)
                    {
                        cmd.Blit(tmp, elem);
                    }

                    _beforeFrames.Add(elem);
                }
            }

            if (++_index / step > 5) { _index = 0; }

            if (_index % step == 0)
            {
                var elem = _beforeFrames[_beforeFrames.Count - 1];

                if (_frameCache.Intensity.value > 0)
                {
                    cmd.Blit(tmp, elem);
                }

                _beforeFrames.RemoveAt(_beforeFrames.Count - 1);
                _beforeFrames.Insert(0, elem);
            }

            for (int i = 0; i < 5; i++)
            {
                _mat.SetTexture(CACHE[i], _beforeFrames[i]);
            }

            //_mat.SetVector(HSV_ID, frameCache.HSVOffset.value);
            //_mat.SetFloat(HSV_SPEED_ID, frameCache.HSVOffsetSpeed.value);
            _mat.SetFloat(INTENSITY_ID, _frameCache.Intensity.value);

            if (_frameCache.Intensity.value > 0)
            {
                cmd.Blit(tmp, source, _mat, 0);
                cmd.ReleaseTemporaryRT(tmp);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _index = -1;
            _beforeFrames?.ForEach(x => x?.Release());
            _beforeFrames = null;
        }

    }
}
