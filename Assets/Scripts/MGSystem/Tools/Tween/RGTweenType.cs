using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyGame.MGSystem
{
    public enum RGTweenDefinitionTypes { RGTween, AnimationCurve }

    [Serializable]
    public class RGTweenType
    {
        public RGTweenDefinitionTypes RGTweenDefinitionType = RGTweenDefinitionTypes.RGTween;
        public RGTween.RGTweenCurve RGTweenCurve = RGTween.RGTweenCurve.EaseInCubic;
        public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1f));
        public RGTweenType(RGTween.RGTweenCurve newCurve)
        {
            RGTweenCurve = newCurve;
            RGTweenDefinitionType = RGTweenDefinitionTypes.RGTween;
        }

        public RGTweenType(AnimationCurve newCurve)
        {
            Debug.Log("RGTweenType-----1");
            Curve = newCurve;
            RGTweenDefinitionType = RGTweenDefinitionTypes.AnimationCurve;
        }

    }
}
