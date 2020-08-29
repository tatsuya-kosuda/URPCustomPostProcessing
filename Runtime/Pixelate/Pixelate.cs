using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/Pixelate")]
    public class Pixelate : CustomPostProcessing
    {

        public IntParameter PixelSize = new IntParameter(10);

        public override bool IsActive()
        {
            return base.IsActive() && PixelSize.value > 1;
        }

    }
}
