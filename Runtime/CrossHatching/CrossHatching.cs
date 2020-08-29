using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/CrossHatching")]
    public class CrossHatching : CustomPostProcessing
    {

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0, 0, 1);

        public FloatParameter Density = new FloatParameter(20);

        public FloatParameter LineWidth = new FloatParameter(10);

        public ColorParameter LineColor = new ColorParameter(Color.white);

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0;
        }

    }
}
