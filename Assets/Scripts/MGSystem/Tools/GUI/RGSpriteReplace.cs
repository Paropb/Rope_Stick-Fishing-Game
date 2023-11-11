using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.MGSystem
{
    /// <summary>
	/// A class to add to an Image or SpriteRenderer to have it act like a button with a different sprite for on and off states
	/// </summary>
    public class RGSpriteReplace : MonoBehaviour
    {
        [Header("Sprites")]
        [RGInformation("Add this to an Image or a SpriteRenderer to be able to swap between two sprites.", RGInformationAttribute.InformationType.Info, false)]

        /// the sprite to use when in the "on" state
        public Sprite OnSprite;
        /// the sprite to use when in the "off" state
        public Sprite OffSprite;

        [Header("Start settings")]
        /// if this is true, the button will start if "on" state
        public bool StartsOn = true;


        [Header("Debug")]
        [RGInspectorButton("Swap")]
        public bool SwapButton;
        [RGInspectorButton("SwitchToOffSprite")]
        public bool SwitchToOffSpriteButton;
        [RGInspectorButton("SwitchToOnSprite")]
        public bool SwitchToOnSpriteButton;

        /// the current state of the button
        public bool CurrentValue { get { return (_image.sprite == OnSprite); } }

        protected Image _image;
        protected SpriteRenderer _spriteRenderer;
        protected RGTouchButton _mmTouchButton;

        /// <summary>
        /// On Start we initialize our button
        /// </summary>
        protected virtual void Start()
        {
            Initialization();
        }

        /// <summary>
		/// On init, we grab our image component, and set our sprite in its initial state
		/// </summary>
		protected virtual void Initialization()
        {
            // grabs components
            _image = GetComponent<Image>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            // grabs button
            _mmTouchButton = GetComponent<RGTouchButton>();
            if (_mmTouchButton != null)
            {
                Debug.Log("RGSpriteReplace----Initialization----1");
                _mmTouchButton.ReturnToInitialSpriteAutomatically = false;
                _mmTouchButton.ButtonPressedFirstTime.AddListener(Swap);
            }
            // handles start
            if ((OnSprite == null) || (OffSprite == null))
            {
                Debug.Log("RGSpriteReplace----Initialization----2");
                return;
            }
            if (_image != null)
            {
                if (StartsOn)
                {
                    _image.sprite = OnSprite;
                }
                else
                {
                    Debug.Log("RGSpriteReplace----Initialization----4");
                    _image.sprite = OffSprite;
                }
            }
            if (_spriteRenderer != null)
            {
                if (StartsOn)
                {
                    Debug.Log("RGSpriteReplace----Initialization----5");
                    _spriteRenderer.sprite = OnSprite;
                }
                else
                {
                    Debug.Log("RGSpriteReplace----Initialization----6");
                    _spriteRenderer.sprite = OffSprite;
                }
            }
        }

        /// <summary>
		/// A public method to change the sprite 
		/// </summary>
		public virtual void Swap()
        {
            if (_image != null)
            {
                if (_image.sprite != OnSprite)
                {
                    SwitchToOnSprite();
                }
                else
                {
                    SwitchToOffSprite();
                }
            }
            if (_spriteRenderer != null)
            {
                Debug.Log("RGSpriteReplace----Swap");
                if (_spriteRenderer.sprite != OnSprite)
                {
                    SwitchToOnSprite();
                }
                else
                {
                    SwitchToOffSprite();
                }
            }
        }

        /// <summary>
		/// a public method to switch to off sprite directly
		/// </summary>
		public virtual void SwitchToOffSprite()
        {
            if ((_image == null) && (_spriteRenderer == null))
            {
                Debug.Log("RGSpriteReplace----SwitchToOffSprite---1");
                return;
            }
            if (OffSprite == null)
            {
                Debug.Log("RGSpriteReplace----SwitchToOffSprite0----2");
                return;
            }

            SpriteOff();
        }

        /// <summary>
		/// sets the image's sprite to off
		/// </summary>
		protected virtual void SpriteOff()
        {
            if (_image != null)
            {
                _image.sprite = OffSprite;
            }
            if (_spriteRenderer != null)
            {
                Debug.Log("RGSpriteReplace----SpriteOff----3");
                _spriteRenderer.sprite = OffSprite;
            }
        }

        /// <summary>
		/// a public method to switch to on sprite directly
		/// </summary>
		public virtual void SwitchToOnSprite()
        {
            if ((_image == null) && (_spriteRenderer == null))
            {
                Debug.Log("RGSpriteReplace----SwitchToOnSprite---1");
                return;
            }
            if (OnSprite == null)
            {
                Debug.Log("RGSpriteReplace----SwitchToOnSprite---2");
                return;
            }
            SpriteOn();
        }

        /// <summary>
		/// sets the image's sprite to on
		/// </summary>
		protected virtual void SpriteOn()
        {
            if (_image != null)
            {
                _image.sprite = OnSprite;
            }
            if (_spriteRenderer != null)
            {
                Debug.Log("RGSpriteReplace----SpriteOn");
                _spriteRenderer.sprite = OnSprite;
            }
        }

    }
}
