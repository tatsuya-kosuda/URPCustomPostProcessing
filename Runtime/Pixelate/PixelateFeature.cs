using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class PixelateFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new PixelatePass(RenderPassEvent);
        }

    }
}
