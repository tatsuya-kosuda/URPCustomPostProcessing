using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/InvertColor")]
    public class InvertColor : CustomPostProcessing
    {

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0, 0.6f, 1f);

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0.6f;
        }

    }
}

