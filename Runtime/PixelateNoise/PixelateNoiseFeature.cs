using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class PixelateNoiseFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new PixelateNoisePass(RenderPassEvent);
        }

    }
}
