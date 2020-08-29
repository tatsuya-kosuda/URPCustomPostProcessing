using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class GlitchFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new GlitchPass(RenderPassEvent);
        }

    }
}
