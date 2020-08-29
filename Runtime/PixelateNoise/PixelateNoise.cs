using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/PixelateNoise")]
    public class PixelateNoise : CustomPostProcessing
    {

        public FloatParameter Intensity = new FloatParameter(0);

        public Vector3Parameter HSV = new Vector3Parameter(new Vector3(0, 1, 1));

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0;
        }

    }
}
