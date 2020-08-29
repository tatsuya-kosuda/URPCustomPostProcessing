using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class RetroGameFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new RetroGamePass(RenderPassEvent);
        }

    }
}
