using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/FrameCache")]
    public class FrameCache : CustomPostProcessing
    {

        public IntParameter Step = new IntParameter(1);

        public Vector3Parameter HSVOffset = new Vector3Parameter(new Vector3(0, 1, 1));

        public FloatParameter HSVOffsetSpeed = new FloatParameter(0f);

        public ClampedFloatParameter Intensity = new ClampedFloatParameter(0, 0, 1);

    }
}
