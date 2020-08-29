using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public abstract class CustomPostProcessingFeature : ScriptableRendererFeature
    {

        protected CustomPostProcessingPass _customPostProcessingPass;

        [SerializeField]
        private RenderPassEvent _renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;

        protected RenderPassEvent RenderPassEvent { get { return _renderPassEvent; } }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_customPostProcessingPass);
        }

    }
}
