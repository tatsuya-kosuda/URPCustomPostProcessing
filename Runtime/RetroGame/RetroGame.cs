using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/RetroGame")]
    public class RetroGame : CustomPostProcessing
    {

        public FloatParameter PixelSize = new FloatParameter(4);

        public FloatParameter BrightnessThreshold = new FloatParameter(0.8f);

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(1, 0, 1);

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0;
        }

    }
}
