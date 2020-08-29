using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/Psycedelic")]
    public class Psycedelic : CustomPostProcessing
    {

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0, 0, 1);

        public FloatParameter NoiseScale = new FloatParameter(1);

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0;
        }

    }
}
