using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/Glitch")]
    public class Glitch : CustomPostProcessing
    {

        public FloatParameter Speed = new FloatParameter(1);

        public Vector2Parameter Intensity = new Vector2Parameter(new Vector2(0.1f, 0f));

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value.x > 0;
        }

    }
}
