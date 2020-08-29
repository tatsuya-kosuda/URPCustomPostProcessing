using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class GlitchFreezeFrameFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new GlitchFreezeFramePass(RenderPassEvent);
        }

    }
}
