using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class FrameCacheFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new FrameCachePass(RenderPassEvent);
        }

    }
}
