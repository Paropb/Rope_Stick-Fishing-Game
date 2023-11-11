using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGame.MGSystem
{
    public class RGVerticalCarousel : MonoBehaviour
    {
        [Header("Binding")]
        /// the layout group that contains all carousel's elements
        public VerticalLayoutGroup Content;
        public Camera UICamera;

        [Header("Optional Buttons Binding")]
        /// the button that moves the carousel to the left
        public RGTouchButton UpButton;
        /// the button that moves the carousel to the right
        public RGTouchButton DownButton;

        [Header("Carousel Setup")]
        /// the initial and current index
        public int CurrentIndex = 0;
        /// the number of items in the carousel that should be moved every time
        public int Pagination = 1;
        /// the percentage of distance that, when reached, will stop movement
        public float ThresholdInPercent = 1f;

        [Header("Speed")]
        /// the duration (in seconds) of the carousel's movement 
        public float MoveDuration = 0.05f;

        [Header("Focus")]
        public bool UseFocus = false;
        /// Bind here the carousel item that should have focus initially
        public GameObject InitialFocus;
        /// if this is true, the mouse will be forced back on Start
        public bool ForceMouseVisible = true;

        [Header("Keyboard/Gamepad")]
        /// the number 
        //public int 

        //protected float ElementWidth { get { return (Content.minWidth - (Content.spacing * (Content.flexibleWidth - 1))) / Content.flexibleWidth; }}
        protected float _elementHeight;
        protected int _contentLength = 0;
        protected float _spacing;
        protected Vector2 _initialPosition;
        protected RectTransform _rectTransform;

        protected bool _lerping = false;
        protected float _lerpStartedTimestamp;
        protected Vector2 _startPosition;
        protected Vector2 _targetPosition;

        protected Dictionary<int, float> _indexToHeightDict = new Dictionary<int, float>();



        /// <summary>
		/// On Start we initialize our carousel
		/// </summary>
		protected virtual void Start()
        {
            Initialization();
        }

        /// <summary>
		/// Initializes the carousel, grabs the rect transform, computes the elements' dimensions, and inits position
		/// </summary>
		protected virtual void Initialization()
        {
            _rectTransform = Content.gameObject.GetComponent<RectTransform>();
            _initialPosition = _rectTransform.anchoredPosition;

            Canvas.ForceUpdateCanvases();
            // we compute the Content's element width
            _contentLength = 0;
            foreach (Transform tr in Content.transform)
            {
                _elementHeight = tr.gameObject.RGGetComponentNoAlloc<RectTransform>().rect.width;
                Debug.Log(tr.gameObject.RGGetComponentNoAlloc<RectTransform>().rect.width);
                _indexToHeightDict.Add(_contentLength, _elementHeight);
                _contentLength++;
            }
            _spacing = Content.spacing;

            // we position our carousel at the desired initial index
            _rectTransform.anchoredPosition = DeterminePosition();
            if (InitialFocus != null)
            {
                EventSystem.current.SetSelectedGameObject(InitialFocus, null);
            }
            if (ForceMouseVisible)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }

        /// <summary>
		/// Moves the carousel to the left.
		/// </summary>
		public virtual void MoveUp()
        {
            if (!CanMoveUp())
            {
                return;
            }
            else
            {
                CurrentIndex -= Pagination;
                MoveToCurrentIndex();
            }
        }

        /// <summary>
        /// Moves the carousel to the right.
        /// </summary>
        public virtual void MoveDown()
        {
            if (!CanMoveDown())
            {
                return;
            }
            else
            {
                CurrentIndex += Pagination;
                MoveToCurrentIndex();
            }
        }

        /// <summary>
        /// Initiates movement to the current index
        /// </summary>
        protected virtual void MoveToCurrentIndex()
        {
            _startPosition = _rectTransform.anchoredPosition;
            _targetPosition = DeterminePosition();
            _lerping = true;
            _lerpStartedTimestamp = Time.time;
        }

        /// <summary>
        /// Determines the target position based on the current index value.
        /// </summary>
        /// <returns>The position.</returns>
        protected virtual Vector2 DeterminePosition()
        {
            Vector2 distance = new Vector2();
            for (int i = 0; i < CurrentIndex; i++)
            {
                distance += (_indexToHeightDict[i] + _spacing) * Vector2.up;
            }
            return _initialPosition - distance;
        }

        public virtual bool CanMoveUp()
        {
            return (CurrentIndex - Pagination >= 0);

        }

        /// <summary>
        /// Determines whether this carousel can move right.
        /// </summary>
        /// <returns><c>true</c> if this instance can move right; otherwise, <c>false</c>.</returns>
        public virtual bool CanMoveDown()
        {
            return (CurrentIndex + Pagination < _contentLength);
        }

        /// <summary>
		/// On Update we move the carousel if required, and handles button states
		/// </summary>
		protected virtual void Update()
        {
            if (_lerping)
            {
                LerpPosition();
            }
            HandleButtons();
            HandleFocus();

        }

        protected virtual void HandleFocus()
        {
            if (!UseFocus)
                return;
            if (!_lerping && Time.timeSinceLevelLoad > 0.5f)
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    if (RectTransformUtility.WorldToScreenPoint(UICamera, EventSystem.current.currentSelectedGameObject.transform.position).y < 0)
                    {
                        MoveUp();
                    }
                    if (RectTransformUtility.WorldToScreenPoint(UICamera, EventSystem.current.currentSelectedGameObject.transform.position).y > Screen.height)
                    {
                        MoveDown();

                    }

                }
            }

        }

        /// <summary>
        /// Handles the buttons, enabling and disabling them if needed
        /// </summary>
        protected virtual void HandleButtons()
        {
            if (UpButton != null)
            {
                if (CanMoveUp())
                {
                    UpButton.EnableButton();
                }
                else
                {
                    UpButton.DisableButton();
                }
            }
            if (DownButton != null)
            {
                if (CanMoveDown())
                {
                    DownButton.EnableButton();
                }
                else
                {
                    DownButton.DisableButton();
                }

            }

        }

        /// <summary>
        /// Lerps the carousel's position.
        /// </summary>
        protected virtual void LerpPosition()
        {
            float timeSinceStarted = Time.time - _lerpStartedTimestamp;
            float percentageComplete = timeSinceStarted / MoveDuration;
            _rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, _targetPosition, percentageComplete);
            //When we've completed the lerp, we set _isLerping to false
            if (percentageComplete >= ThresholdInPercent)
            {
                _lerping = false;
            }
        }
    }
}
