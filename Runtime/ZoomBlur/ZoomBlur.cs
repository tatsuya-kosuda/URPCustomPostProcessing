using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace cpp
{
    [VolumeComponentMenu("Post-processing/Custom/ZoomBlur")]
    public class ZoomBlur : CustomPostProcessing
    {

        public ClampedFloatParameter focusPower = new ClampedFloatParameter(0, 0, 100);

        public ClampedIntParameter focusDetail = new ClampedIntParameter(5, 0, 10);

        public Vector2Parameter focusScreenPosition = new Vector2Parameter(Vector2.zero);

        public override bool IsActive() => focusPower.value > 0;

    }
}
