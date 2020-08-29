using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace cpp
{
    public abstract class CustomPostProcessing : VolumeComponent, IPostProcessComponent
    {

        public BoolParameter Enable = new BoolParameter(false);

        public virtual bool IsActive() => Enable.value;

        public virtual bool IsTileCompatible() => false;

    }
}
