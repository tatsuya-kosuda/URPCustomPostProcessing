using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/VideoFeedback")]
    public class VideoFeedback : CustomPostProcessing
    {

        public ClampedFloatParameter Repeat = new ClampedFloatParameter(1, 1, 10);

        public override bool IsActive()
        {
            return base.IsActive() && Repeat.value > 1;
        }

    }
}
