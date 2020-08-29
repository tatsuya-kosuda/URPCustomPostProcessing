using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/UVMirror")]
    public class UVMirror : CustomPostProcessing
    {

        public Vector2Parameter Threshold = new Vector2Parameter(new Vector2(0.5f, 0.5f));

        public override bool IsActive()
        {
            return base.IsActive() && (Threshold.value.x < 1 || Threshold.value.y < 1);
        }

    }
}
