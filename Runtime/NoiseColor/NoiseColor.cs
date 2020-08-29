using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/NoiseColor")]
    public class NoiseColor : CustomPostProcessing
    {

        public ClampedFloatParameter Threshold = new ClampedFloatParameter(0.1f, 0f, 1f);

        public ClampedFloatParameter WaveScale = new ClampedFloatParameter(0.01f, 0.01f, 0.1f);

        public FloatParameter NoiseDetail = new FloatParameter(500);

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0, 0, 1);

        public Vector3Parameter HSVOffset = new Vector3Parameter(new Vector3(0, 1, 1));

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0;
        }

    }
}
