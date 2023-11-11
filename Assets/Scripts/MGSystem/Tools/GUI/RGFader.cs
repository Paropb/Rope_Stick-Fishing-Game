using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace MyGame.MGSystem
{
    /// <summary>
    /// An event used to stop fades
    /// </summary>
    public struct RGFadeStopEvent
    {
        /// an ID that has to match the one on the fader
        public int ID;

        static RGFadeStopEvent e;
        public static void Trigger(int id = 0)
        {
            e.ID = id;
            RGEventManager.TriggerEvent(e);
        }
    }

    /// <summary>
    /// Events used to trigger faders on or off
    /// </summary>
    public struct RGFadeEvent
    {
        /// an ID that has to match the one on the fader
        public int ID;
        /// the duration of the fade, in seconds
        public float Duration;
        /// the alpha to aim for
        public float TargetAlpha;
        /// the curve to apply to the fade
        public RGTweenType Curve;
        /// whether or not this fade should ignore timescale
        public bool IgnoreTimeScale;

        static RGFadeEvent e;

        public static void Trigger(float duration, float targetAlpha, RGTweenType tween, int id = 0,
            bool ignoreTimeScale = true)
        {
            e.ID = id;
            e.Duration = duration;
            e.TargetAlpha = targetAlpha;
            e.Curve = tween;
            e.IgnoreTimeScale = ignoreTimeScale;
            RGEventManager.TriggerEvent(e);
        }
    }

    public struct RGFadeInEvent
    {
        /// an ID that has to match the one on the fader
        public int ID;
        /// the duration of the fade, in seconds
        public float Duration;
        /// the curve to apply to the fade
        public RGTweenType Curve;
        /// whether or not this fade should ignore timescale
        public bool IgnoreTimeScale;
        /// a world position for a target object. Useless for regular fades, but can be useful for alt implementations (circle fade for example)
        public Vector3 WorldPosition;

        public RGFader.FadeTypes FadeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoreMountains.RGInterface.RGFadeInEvent"/> struct.
        /// </summary>
        /// <param name="duration">Duration.</param>
        public RGFadeInEvent(float duration, RGTweenType tween, int id = 0,
            bool ignoreTimeScale = true, Vector3 worldPosition = new Vector3(), RGFader.FadeTypes fadeType = RGFader.FadeTypes.Fade)
        {
            ID = id;
            Duration = duration;
            Curve = tween;
            IgnoreTimeScale = ignoreTimeScale;
            WorldPosition = worldPosition;
            FadeType = fadeType;
        }


        static RGFadeInEvent e;
        public static void Trigger(float duration, RGTweenType tween, int id = 0,
            bool ignoreTimeScale = true, Vector3 worldPosition = new Vector3(), RGFader.FadeTypes fadeType = RGFader.FadeTypes.Fade)
        {
            e.ID = id;
            e.Duration = duration;
            e.Curve = tween;
            e.IgnoreTimeScale = ignoreTimeScale;
            e.FadeType = fadeType;
            RGEventManager.TriggerEvent(e);
        }
    }

    public struct RGFadeOutEvent
    {
        /// an ID that has to match the one on the fader
        public int ID;
        /// the duration of the fade, in seconds
        public float Duration;
        /// the curve to apply to the fade
        public RGTweenType Curve;
        /// whether or not this fade should ignore timescale
        public bool IgnoreTimeScale;

        public RGFader.FadeTypes FadeType;


        static RGFadeOutEvent e;

        public static void Trigger(float duration, RGTweenType tween, int id = 0,
            bool ignoreTimeScale = true, RGFader.FadeTypes fadeType = RGFader.FadeTypes.Fade)
        {
            e.ID = id;
            e.Duration = duration;
            e.Curve = tween;
            e.IgnoreTimeScale = ignoreTimeScale;
            e.FadeType = fadeType;
            RGEventManager.TriggerEvent(e);
        }

    }
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Robot Game/System/GUI/RGFader")]
    public class RGFader : MonoBehaviour, IRGEventListener<RGFadeEvent>, IRGEventListener<RGFadeInEvent>, IRGEventListener<RGFadeOutEvent>, IRGEventListener<RGFadeStopEvent>
    {
        [Header("FadeType")]
        public FadeTypes FadeType = FadeTypes.Fade;
        public enum ForcedInitStates { None, Active, Inactive }
        public enum FadeTypes { Fade, CloseDoor }
        [ShowIfGroup("FadeType", Value = FadeTypes.Fade), BoxGroup("FadeType/Identification")]
        /// the ID for this fader (0 is default), set more IDs if you need more than one fader
        public int ID;
        /// the opacity the fader should be at when inactive
        [BoxGroup("FadeType/Opacity")]
        public float InactiveAlpha = 0f;
        /// the opacity the fader should be at when active
        [BoxGroup("FadeType/Opacity")]
        public float ActiveAlpha = 1f;
        /// determines whether a state should be forced on init
        public ForcedInitStates ForcedInitState = ForcedInitStates.Inactive;
        [Header("Timing")]
        /// the default duration of the fade in/out
        public float DefaultDuration = 0.2f;
        /// the default curve to use for this fader
        public RGTweenType DefaultTween = new RGTweenType(RGTween.RGTweenCurve.LinearTween);
        /// whether or not the fade should happen in unscaled time 
        public bool IgnoreTimescale = true;
        [Header("Interaction")]
        /// whether or not the fader should block raycasts when visible
        public bool ShouldBlockRaycasts = false;

        [BoxGroup("CloseDoor")]
        public Image LeftPanel;
        [BoxGroup("CloseDoor")]
        public Image RightPanel;
        [BoxGroup("CloseDoor")]
        public CanvasGroup LogoCanvas;
        [BoxGroup("CloseDoor")]
        public Image LogoImage;
        [BoxGroup("CloseDoor")]
        public float panelInactiveScaleX = 0f;
        [BoxGroup("CloseDoor")]
        public float leftPanelActiveScaleX = -0.5f;
        [BoxGroup("CloseDoor")]
        public float rightPanelActiveScaleX = 0.5f;
        [BoxGroup("CloseDoor")]
        public float LogoInActiveSize = 20f;
        [BoxGroup("CloseDoor")]
        public float LogoActiveSize = 1f;



        [Header("Debug")]
        [RGInspectorButton("FadeIn1Second")]
        public bool FadeIn1SecondButton;
        [RGInspectorButton("FadeOut1Second")]
        public bool FadeOut1SecondButton;
        [RGInspectorButton("DefaultFade")]
        public bool DefaultFadeButton;
        [RGInspectorButton("ResetFader")]
        public bool ResetFaderButton;
        [RGInspectorButton("StartFader")]
        public bool StartFaderButton;

        protected CanvasGroup _canvasGroup;
        protected CanvasGroup[] _childrenCanvasGroups;
        protected Image _image;
        protected float _initialAlpha;
        protected float _currentTargetAlpha;
        protected float _currentDuration;
        protected Vector3 _panelInactiveScale;
        protected Vector3 _leftPanelActiveScale;
        protected Vector3 _rightPanelActiveScale;

        protected Vector3 _leftPanelTargetScale;
        protected Vector3 _rightPanelTargetScale;
        protected Vector3 _leftPanelInitialScale;
        protected Vector3 _rightPanelInitialScale;

        protected float _logoInitialSize;
        protected float _logoTargetSize;
        protected float _logoInitialAlpha;
        protected float _logoTargetAlpha;

        protected RGTweenType _currentCurve;

        protected bool _fading = false;
        protected float _fadeStartedAt;
        protected bool _frameCountOne;

        protected virtual void StartFader()
        {
            StartFading(InactiveAlpha, ActiveAlpha, DefaultDuration, DefaultTween, ID, IgnoreTimescale);
        }

        protected virtual void ResetFader()
        {
            _canvasGroup.alpha = InactiveAlpha;
        }


        /// <summary>
        /// Test method triggered by an inspector button
        /// </summary>
        protected virtual void DefaultFade()
        {
            if (ForcedInitState == ForcedInitStates.Inactive)
            {
                RGFadeEvent.Trigger(DefaultDuration, InactiveAlpha, DefaultTween, ID);
            }
            else if (ForcedInitState == ForcedInitStates.Active)
            {
                RGFadeEvent.Trigger(DefaultDuration, ActiveAlpha, DefaultTween, ID);
            }
        }

        /// <summary>
        /// Test method triggered by an inspector button
        /// </summary>
        protected virtual void FadeIn1Second()
        {
            RGFadeInEvent.Trigger(1f, new RGTweenType(RGTween.RGTweenCurve.LinearTween), ID);
        }

        /// <summary>
        /// Test method triggered by an inspector button
        /// </summary>
        protected virtual void FadeOut1Second()
        {
            RGFadeOutEvent.Trigger(1f, new RGTweenType(RGTween.RGTweenCurve.LinearTween), ID);
        }
        /// <summary>
        /// On Start, we initialize our fader
        /// </summary>
        protected virtual void Awake()
        {
            Initialization();
        }

        /// <summary>
        /// On init, we grab our components, and disable/hide everything
        /// </summary>
        protected virtual void Initialization()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();

            _childrenCanvasGroups = GetComponentsInChildren<CanvasGroup>();
            foreach (var canvasGroup in _childrenCanvasGroups)
            {
                canvasGroup.alpha = 1f;
            }

            if (ForcedInitState == ForcedInitStates.Inactive)
            {
                _canvasGroup.alpha = InactiveAlpha;
                _image.enabled = false;
            }
            else if (ForcedInitState == ForcedInitStates.Active)
            {
                _canvasGroup.alpha = ActiveAlpha;
                _image.enabled = true;
            }



            _rightPanelActiveScale = new Vector3(rightPanelActiveScaleX, 1f, 1f);
            _leftPanelActiveScale = new Vector3(leftPanelActiveScaleX, 1f, 1f);
            _panelInactiveScale = new Vector3(panelInactiveScaleX, 1f, 1f);

            if (ForcedInitState == ForcedInitStates.Inactive)
            {
                RightPanel.rectTransform.localScale = _panelInactiveScale;
                LeftPanel.rectTransform.localScale = _panelInactiveScale;
                LogoImage.rectTransform.localScale = LogoInActiveSize * Vector3.one;
                LogoCanvas.alpha = InactiveAlpha;
                _canvasGroup.alpha = InactiveAlpha;
                _image.enabled = false;
            }
            else if (ForcedInitState == ForcedInitStates.Active)
            {
                RightPanel.rectTransform.localScale = _rightPanelActiveScale;
                LeftPanel.rectTransform.localScale = _leftPanelActiveScale;
                LogoImage.rectTransform.localScale = LogoActiveSize * Vector3.one;
                LogoCanvas.alpha = ActiveAlpha;
                _canvasGroup.alpha = ActiveAlpha;
                _image.enabled = true;
            }




        }
        /// <summary>
        /// On Update, we update our alpha 
        /// </summary>
        protected virtual void Update()
        {
            if (_canvasGroup == null) { return; }
            if (_fading)
            {
                Fade();
            }

        }
        /// <summary>
        /// Fades the canvasgroup towards its target alpha
        /// </summary>
        protected virtual void Fade()
        {
            float currentTime = IgnoreTimescale ? Time.unscaledTime : Time.time;
            //Scene刚刚打开，当前是画面的第一帧
            if (_frameCountOne)
            {
                if (Time.frameCount <= 2)
                {
                    _canvasGroup.alpha = _initialAlpha;
                    return;
                }
                _fadeStartedAt = IgnoreTimescale ? Time.unscaledTime : Time.time;
                currentTime = _fadeStartedAt;
                _frameCountOne = false;
            }
            float endTime = _fadeStartedAt + _currentDuration;
            if (currentTime - _fadeStartedAt < _currentDuration)
            {
                switch (FadeType)
                {
                    case FadeTypes.Fade:
                        float result = RGTween.Tween(currentTime, _fadeStartedAt, endTime, _initialAlpha, _currentTargetAlpha, _currentCurve);
                        _canvasGroup.alpha = result;
                        break;
                    case FadeTypes.CloseDoor:
                        float canvasResult = RGTween.Tween(currentTime, _fadeStartedAt, endTime, _initialAlpha, _currentTargetAlpha, _currentCurve);
                        _canvasGroup.alpha = canvasResult;
                        Vector3 leftResult = RGTween.Tween(currentTime, _fadeStartedAt, endTime, _leftPanelInitialScale.x, _leftPanelTargetScale.x, _currentCurve) * Vector3.right;
                        leftResult.Set(leftResult.x, 1f, 1f);
                        LeftPanel.rectTransform.localScale = leftResult;
                        Vector3 rightResult = RGTween.Tween(currentTime, _fadeStartedAt, endTime, _rightPanelInitialScale.x, _rightPanelTargetScale.x, _currentCurve) * Vector3.right;
                        rightResult.Set(rightResult.x, 1f, 1f);
                        RightPanel.rectTransform.localScale = rightResult;
                        //logo
                        float logoResult = RGTween.Tween(currentTime, _fadeStartedAt, endTime, _logoInitialSize, _logoTargetSize, _currentCurve);
                        float logoAlphaResult = RGTween.Tween(currentTime, _fadeStartedAt, endTime, _logoInitialAlpha, _logoTargetAlpha, _currentCurve);
                        LogoImage.rectTransform.localScale = logoResult * Vector3.one;
                        LogoCanvas.alpha = logoAlphaResult;
                        break;

                }

            }
            else
            {
                StopFading();
            }

        }
        /// <summary>
        /// Stops the fading.
        /// </summary>
        protected virtual void StopFading()
        {
            switch (FadeType)
            {
                case FadeTypes.Fade:
                    _canvasGroup.alpha = _currentTargetAlpha;
                    break;
                case FadeTypes.CloseDoor:
                    _canvasGroup.alpha = _currentTargetAlpha;
                    LeftPanel.rectTransform.localScale = _leftPanelTargetScale;
                    RightPanel.rectTransform.localScale = _rightPanelTargetScale;
                    LogoCanvas.alpha = _logoTargetAlpha;
                    LogoImage.rectTransform.localScale = _logoTargetSize * Vector3.one;
                    break;
            }
            _fading = false;
            if (_canvasGroup.alpha == InactiveAlpha)
            {
                DisableFader();
            }

        }

        /// <summary>
        /// Disables the fader.
        /// </summary>
        protected virtual void DisableFader()
        {
            _image.enabled = false;
            if (ShouldBlockRaycasts)
            {
                _canvasGroup.blocksRaycasts = false;
            }
        }
        /// <summary>
        /// Enables the fader.
        /// </summary>
        protected virtual void EnableFader()
        {
            _image.enabled = true;
            if (ShouldBlockRaycasts)
            {
                _canvasGroup.blocksRaycasts = true;
            }
        }
        /// <summary>
        /// Starts fading this fader from the specified initial alpha to the target
        /// </summary>
        /// <param name="initialAlpha"></param>
        /// <param name="endAlpha"></param>
        /// <param name="duration"></param>
        /// <param name="curve"></param>
        /// <param name="id"></param>
        /// <param name="ignoreTimeScale"></param>
        protected virtual void StartFading(float initialAlpha, float endAlpha, float duration, RGTweenType curve, int id, bool ignoreTimeScale)
        {
            if (id != ID)
            {
                return;
            }
            IgnoreTimescale = ignoreTimeScale;
            EnableFader();
            _fading = true;
            _initialAlpha = initialAlpha;
            _currentTargetAlpha = endAlpha;
            _fadeStartedAt = IgnoreTimescale ? Time.unscaledTime : Time.time;
            _currentCurve = curve;
            _currentDuration = duration;
            if (Time.frameCount == 1)
            {
                _frameCountOne = true;
            }
        }
        protected virtual void StartFading(Vector3 leftInitialScale, Vector3 leftEndScale, Vector3 rightInitialScale, Vector3 rightEndScale,
            float logoInitialSize, float logoEndSize, float logoInitialAlpha, float logoEndAlpha,
            float duration, RGTweenType curve, int id, bool ignoreTimeScale)
        {
            if (id != ID)
            {
                return;
            }
            IgnoreTimescale = ignoreTimeScale;
            EnableFader();
            _fading = true;
            _leftPanelInitialScale = leftInitialScale;
            _rightPanelInitialScale = rightInitialScale;
            _leftPanelTargetScale = leftEndScale;
            _rightPanelTargetScale = rightEndScale;
            //Start Fade Meanwhile
            _initialAlpha = logoInitialAlpha;
            _currentTargetAlpha = logoEndAlpha;

            _logoInitialSize = logoInitialSize;
            _logoTargetSize = logoEndSize;
            _logoInitialAlpha = logoInitialAlpha;
            _logoTargetAlpha = logoEndAlpha;
            _fadeStartedAt = IgnoreTimescale ? Time.unscaledTime : Time.time;
            _currentCurve = curve;
            _currentDuration = duration;
            if (Time.frameCount == 1)
            {
                _frameCountOne = true;
            }
        }
        public void OnRGEvent(RGFadeEvent fadeEvent)
        {
            _currentTargetAlpha = (fadeEvent.TargetAlpha == -1) ? ActiveAlpha : fadeEvent.TargetAlpha;
            switch (FadeType)
            {
                case FadeTypes.Fade:
                    StartFading(_canvasGroup.alpha, _currentTargetAlpha, fadeEvent.Duration, fadeEvent.Curve, fadeEvent.ID, fadeEvent.IgnoreTimeScale);
                    break;
                case FadeTypes.CloseDoor:
                    //StartFading(_leftPanelActiveLocalPos, _leftPanelInactiveLocalPos, _rightPanelActiveLocalPos, _rightPanelInactiveLocalPos,
                    //    LogoActiveSize, LogoInActiveSize, ActiveAlpha, InactiveAlpha,
                    //    fadeEvent.Duration, fadeEvent.Curve, fadeEvent.ID, fadeEvent.IgnoreTimeScale);
                    break;
            }
        }

        public void OnRGEvent(RGFadeInEvent fadeEvent)
        {
            switch (fadeEvent.FadeType)
            {
                case FadeTypes.Fade:
                    StartFading(InactiveAlpha, ActiveAlpha, fadeEvent.Duration, fadeEvent.Curve, fadeEvent.ID, fadeEvent.IgnoreTimeScale);
                    break;
                case FadeTypes.CloseDoor:
                    StartFading(_panelInactiveScale, _leftPanelActiveScale, _panelInactiveScale, _rightPanelActiveScale,
                        LogoInActiveSize, LogoActiveSize, InactiveAlpha, ActiveAlpha,
                        fadeEvent.Duration, fadeEvent.Curve, fadeEvent.ID, fadeEvent.IgnoreTimeScale);
                    break;
            }
        }

        public void OnRGEvent(RGFadeOutEvent fadeEvent)
        {
            switch (fadeEvent.FadeType)
            {
                case FadeTypes.Fade:
                    StartFading(ActiveAlpha, InactiveAlpha, fadeEvent.Duration, fadeEvent.Curve, fadeEvent.ID, fadeEvent.IgnoreTimeScale);
                    break;
                case FadeTypes.CloseDoor:
                    StartFading(_leftPanelActiveScale, _panelInactiveScale, _rightPanelActiveScale, _panelInactiveScale,
                        LogoActiveSize, LogoInActiveSize, ActiveAlpha, InactiveAlpha,
                        fadeEvent.Duration, fadeEvent.Curve, fadeEvent.ID, fadeEvent.IgnoreTimeScale);
                    break;
            }
        }

        public void OnRGEvent(RGFadeStopEvent fadeStopEvent)
        {
            if (fadeStopEvent.ID == ID)
            {
                _fading = false;
            }
        }

        /// <summary>
        /// On enable, we start listening to events
        /// </summary>
        protected virtual void OnEnable()
        {
            this.RGEventStartListening<RGFadeEvent>();
            this.RGEventStartListening<RGFadeStopEvent>();
            this.RGEventStartListening<RGFadeInEvent>();
            this.RGEventStartListening<RGFadeOutEvent>();
        }

        /// <summary>
        /// On disable, we stop listening to events
        /// </summary>
        protected virtual void OnDisable()
        {
            this.RGEventStopListening<RGFadeEvent>();
            this.RGEventStopListening<RGFadeStopEvent>();
            this.RGEventStopListening<RGFadeInEvent>();
            this.RGEventStopListening<RGFadeOutEvent>();
        }

    }
}
