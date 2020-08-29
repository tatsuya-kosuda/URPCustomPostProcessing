using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class CrossHatchingFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new CrossHatchingPass(RenderPassEvent);
        }

    }
}
