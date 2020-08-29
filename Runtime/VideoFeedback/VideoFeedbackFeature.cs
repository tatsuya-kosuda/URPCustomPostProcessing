using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class VideoFeedbackFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new VideoFeedbackPass(RenderPassEvent);
        }

    }
}
