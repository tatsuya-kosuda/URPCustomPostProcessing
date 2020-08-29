﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpp
{
    public class NoiseColorFeature : CustomPostProcessingFeature
    {

        public override void Create()
        {
            _customPostProcessingPass = new NoiseColorPass(RenderPassEvent);
        }

    }
}
