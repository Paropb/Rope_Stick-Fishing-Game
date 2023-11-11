using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class RGTween : MonoBehaviour
    {
        /// <suRGary>
        /// A list of all the possible curves you can tween a value along
        /// </summary>
        public enum RGTweenCurve
        {
            LinearTween,
            EaseInQuadratic, EaseOutQuadratic, EaseInOutQuadratic,
            EaseInCubic, EaseOutCubic, EaseInOutCubic,
            EaseInQuartic, EaseOutQuartic, EaseInOutQuartic,
            EaseInQuintic, EaseOutQuintic, EaseInOutQuintic,
            EaseInSinusoidal, EaseOutSinusoidal, EaseInOutSinusoidal,
            EaseInBounce, EaseOutBounce, EaseInOutBounce,
            EaseInOverhead, EaseOutOverhead, EaseInOutOverhead,
            EaseInExponential, EaseOutExponential, EaseInOutExponential,
            EaseInElastic, EaseOutElastic, EaseInOutElastic,
            EaseInCircular, EaseOutCircular, EaseInOutCircular,
            AntiLinearTween, AlmostIdentity
        }
        // Core methods ---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Moves a value between a startValue and an endValue based on a currentTime, along the specified tween curve
        /// </summary>
        /// <param name="currentTime"></param>
        /// <param name="initialTime"></param>
        /// <param name="endTime"></param>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="curve"></param>
        /// <returns></returns>
        public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, RGTweenCurve curve)
        {
            //������[initialTime��endTime]�е�ֵcurrentTime��ӳ��Ϊ����[0f��1f]�еı���ֵ
            currentTime = RGMaths.Remap(currentTime, initialTime, endTime, 0f, 1f);
            switch (curve)
            {
                //�����˶�������CurrentTime�������κα仯
                case RGTweenCurve.LinearTween:
                    currentTime = RGTweenDefinitions.Linear_Tween(currentTime); break;
                case RGTweenCurve.AntiLinearTween:
                    currentTime = RGTweenDefinitions.LinearAnti_Tween(currentTime); break;

                case RGTweenCurve.EaseInQuadratic:
                    currentTime = RGTweenDefinitions.EaseIn_Quadratic(currentTime); break;
                case RGTweenCurve.EaseOutQuadratic:
                    currentTime = RGTweenDefinitions.EaseOut_Quadratic(currentTime); break;
                case RGTweenCurve.EaseInOutQuadratic:
                    currentTime = RGTweenDefinitions.EaseInOut_Quadratic(currentTime); break;

                case RGTweenCurve.EaseInCubic:
                    currentTime = RGTweenDefinitions.EaseIn_Cubic(currentTime); break;
                case RGTweenCurve.EaseOutCubic:
                    currentTime = RGTweenDefinitions.EaseOut_Cubic(currentTime); break;
                case RGTweenCurve.EaseInOutCubic:
                    currentTime = RGTweenDefinitions.EaseInOut_Cubic(currentTime); break;

                case RGTweenCurve.EaseInQuartic:
                    currentTime = RGTweenDefinitions.EaseIn_Quartic(currentTime); break;
                case RGTweenCurve.EaseOutQuartic:
                    currentTime = RGTweenDefinitions.EaseOut_Quartic(currentTime); break;
                case RGTweenCurve.EaseInOutQuartic:
                    currentTime = RGTweenDefinitions.EaseInOut_Quartic(currentTime); break;

                case RGTweenCurve.EaseInQuintic:
                    currentTime = RGTweenDefinitions.EaseIn_Quintic(currentTime); break;
                case RGTweenCurve.EaseOutQuintic:
                    currentTime = RGTweenDefinitions.EaseOut_Quintic(currentTime); break;
                case RGTweenCurve.EaseInOutQuintic:
                    currentTime = RGTweenDefinitions.EaseInOut_Quintic(currentTime); break;

                case RGTweenCurve.EaseInSinusoidal:
                    currentTime = RGTweenDefinitions.EaseIn_Sinusoidal(currentTime); break;
                case RGTweenCurve.EaseOutSinusoidal:
                    currentTime = RGTweenDefinitions.EaseOut_Sinusoidal(currentTime); break;
                case RGTweenCurve.EaseInOutSinusoidal:
                    currentTime = RGTweenDefinitions.EaseInOut_Sinusoidal(currentTime); break;

                case RGTweenCurve.EaseInBounce:
                    currentTime = RGTweenDefinitions.EaseIn_Bounce(currentTime); break;
                case RGTweenCurve.EaseOutBounce:
                    currentTime = RGTweenDefinitions.EaseOut_Bounce(currentTime); break;
                case RGTweenCurve.EaseInOutBounce:
                    currentTime = RGTweenDefinitions.EaseInOut_Bounce(currentTime); break;

                case RGTweenCurve.EaseInOverhead:
                    currentTime = RGTweenDefinitions.EaseIn_Overhead(currentTime); break;
                case RGTweenCurve.EaseOutOverhead:
                    currentTime = RGTweenDefinitions.EaseOut_Overhead(currentTime); break;
                case RGTweenCurve.EaseInOutOverhead:
                    currentTime = RGTweenDefinitions.EaseInOut_Overhead(currentTime); break;

                case RGTweenCurve.EaseInExponential:
                    currentTime = RGTweenDefinitions.EaseIn_Exponential(currentTime); break;
                case RGTweenCurve.EaseOutExponential:
                    currentTime = RGTweenDefinitions.EaseOut_Exponential(currentTime); break;
                case RGTweenCurve.EaseInOutExponential:
                    currentTime = RGTweenDefinitions.EaseInOut_Exponential(currentTime); break;

                case RGTweenCurve.EaseInElastic:
                    currentTime = RGTweenDefinitions.EaseIn_Elastic(currentTime); break;
                case RGTweenCurve.EaseOutElastic:
                    currentTime = RGTweenDefinitions.EaseOut_Elastic(currentTime); break;
                case RGTweenCurve.EaseInOutElastic:
                    currentTime = RGTweenDefinitions.EaseInOut_Elastic(currentTime); break;

                case RGTweenCurve.EaseInCircular:
                    currentTime = RGTweenDefinitions.EaseIn_Circular(currentTime); break;
                case RGTweenCurve.EaseOutCircular:
                    currentTime = RGTweenDefinitions.EaseOut_Circular(currentTime); break;
                case RGTweenCurve.EaseInOutCircular:
                    currentTime = RGTweenDefinitions.EaseInOut_Circular(currentTime); break;

                case RGTweenCurve.AlmostIdentity:
                    currentTime = RGTweenDefinitions.AlmostIdentity(currentTime); break;

            }
            return startValue + currentTime * (endValue - startValue);
        }

        // Animation curve methods --------------------------------------------------------------------------------------------------------------

        public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, AnimationCurve curve)
        {
            currentTime = RGMaths.Remap(currentTime, initialTime, endTime, 0f, 1f);
            currentTime = curve.Evaluate(currentTime);
            return startValue + currentTime * (endValue - startValue);
        }
        // Tween type methods ------------------------------------------------------------------------------------------------------------------------
        public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, RGTweenType tweenType)
        {
            if (tweenType.RGTweenDefinitionType == RGTweenDefinitionTypes.RGTween)
            {
                return Tween(currentTime, initialTime, endTime, startValue, endValue, tweenType.RGTweenCurve);
            }
            if (tweenType.RGTweenDefinitionType == RGTweenDefinitionTypes.AnimationCurve)
            {
                return Tween(currentTime, initialTime, endTime, startValue, endValue, tweenType.Curve);
            }
            return 0f;
        }
    }
}
