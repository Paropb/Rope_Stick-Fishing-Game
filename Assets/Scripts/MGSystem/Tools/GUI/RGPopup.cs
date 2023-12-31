using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class RGPopup : MonoBehaviour
    {
        /// true if the popup is currently open
        
        [RGReadOnly]
        public bool CurrentlyOpen = false;

        //[Header("Fader")]
        //public float FaderOpenDuration = 0.2f;
        //public float FaderCloseDuration = 0.2f;
        //public float FaderOpacity = 0.8f;
        //public RGTweenType Tween = new RGTweenType(RGTween.RGTweenCurve.EaseInCubic);
        //public int ID = 0;

        protected Animator _animator;

        /// <summary>
		/// On Start, we initialize our popup
		/// </summary>
		protected virtual void Start()
        {
            Initialization();
        }

        /// <summary>
        /// On Init, we grab our animator and store it for future use
        /// </summary>
        protected virtual void Initialization()
        {
            _animator = GetComponent<Animator>();

            
        }

        /// <summary>
		/// On update, we update our animator parameter
		/// </summary>
		protected virtual void Update()
        {
            if (_animator != null)
            {
                _animator.SetBool("Closed", !CurrentlyOpen);
            }
        }

        public virtual void ChangeCurrentOpenStatus()
        {
            if(CurrentlyOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        /// <summary>
        /// Opens the popup
        /// </summary>
        public virtual void Open()
        {
            if (CurrentlyOpen)
            {
                return;
            }
            //RGFadeEvent.Trigger(FaderOpenDuration, FaderOpacity, Tween, ID);
            _animator.SetTrigger("Open");
            CurrentlyOpen = true;

            
        }

        /// <summary>
        /// Closes the popup
        /// </summary>
        public virtual void Close()
        {
            if (!CurrentlyOpen)
            {
                return;
            }
            //RGFadeEvent.Trigger(FaderCloseDuration, 0f, Tween, ID);
            _animator.SetTrigger("Close");
            CurrentlyOpen = false;

            
        }

    }
}
