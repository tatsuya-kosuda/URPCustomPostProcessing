using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/Voronoi")]
    public class Voronoi : CustomPostProcessing
    {

        public FloatParameter CellSize = new FloatParameter(10);

        public Vector2Parameter Offset = new Vector2Parameter(Vector2.zero);

        public FloatParameter Speed = new FloatParameter(1);

        public FloatParameter Speed2 = new FloatParameter(1);

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0, 0, 1);

        public override bool IsActive()
        {
            return base.IsActive() && Intensity.value > 0;
        }

    }
}
