using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class InvertColorFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new InvertColorPass(RenderPassEvent);
        }

    }
}
