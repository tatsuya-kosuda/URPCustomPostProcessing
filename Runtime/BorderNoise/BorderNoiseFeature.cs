using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class BorderNoiseFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new BorderNoisePass(RenderPassEvent);
        }

    }
}
