using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class DisplacementNoiseFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new DisplacementNoisePass(RenderPassEvent);
        }

    }
}
