using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public class PsycedelicFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new PsycedelicPass(RenderPassEvent);
        }

    }
}
