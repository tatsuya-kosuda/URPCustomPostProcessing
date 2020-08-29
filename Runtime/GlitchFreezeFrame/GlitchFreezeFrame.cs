using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/GlitchFreezeFrame")]
    public class GlitchFreezeFrame : CustomPostProcessing
    {

        public ClampedFloatParameter glitchStrength = new ClampedFloatParameter(0, 0, 1);

        public ClampedFloatParameter direction = new ClampedFloatParameter(0, 0, 1);

        public override bool IsActive()
        {
            return base.IsActive() && glitchStrength.value > 0;
        }

    }
}
