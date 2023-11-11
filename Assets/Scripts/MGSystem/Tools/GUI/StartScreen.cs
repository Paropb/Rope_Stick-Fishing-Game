using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MyGame.MGSystem
{
    /// <summary>
	/// Simple start screen class.
	/// </summary>
    public class StartScreen : MonoBehaviour
    {
        /// the level to load after the start screen
		[Tooltip("the level to load after the start screen")]
        public string NextLevel;

        /// the delay after which the level should auto skip (if less than 1s, won't autoskip)
        [Tooltip("the delay after which the level should auto skip (if less than 1s, won't autoskip)")]
        public float AutoSkipDelay = 0f;

        [Header("Fades")]
        /// the duration of the fade in of the start screen, in seconds
        [Tooltip("the duration of the fade in of the start screen, in seconds")]
        public float FadeInDuration = 1f;
        /// the duration of the fade out of the start screen, in seconds
        [Tooltip("the duration of the fade out of the start screen, in seconds")]
        public float FadeOutDuration = 1f;
        /// the tween type this fade should happen on
        public RGTweenType Tween;

        [Header("Sound Settings Bindings")]

        /// the switch used to turn music on or off
        [Tooltip("the switch used to turn music on or off")]
        public RGSwitch MusicSwitch;
        /// the switch used to turn sfx on or off
        [Tooltip("the switch used to turn sfx on or off")]
        public RGSwitch SfxSwitch;

        /// <summary>
		/// Initialization
		/// </summary>
		protected virtual void Awake()
        {
            if (AutoSkipDelay > 1f)
            {
                FadeOutDuration = AutoSkipDelay;
                StartCoroutine(LoadFirstLevel());
            }
        }

        /// <summary>
        /// On Start we initialize our switches
        /// </summary>
        protected async void Start()
        {
            RGFadeOutEvent.Trigger(FadeInDuration, Tween, fadeType: RGFader.FadeTypes.CloseDoor);
            await Task.Delay(1);
            if (MusicSwitch != null)
            {
                MusicSwitch.CurrentSwitchState = RGSoundManager.Instance.settingsSo.Settings.MusicOn ? RGSwitch.SwitchStates.Right : RGSwitch.SwitchStates.Left;
                MusicSwitch.InitializeState();
            }
            if (SfxSwitch != null)
            {
                SfxSwitch.CurrentSwitchState = RGSoundManager.Instance.settingsSo.Settings.SfxOn ? RGSwitch.SwitchStates.Right : RGSwitch.SwitchStates.Left;
                SfxSwitch.InitializeState();
            }
        }

        /// <summary>
        /// During update we simply wait for the user to press the "jump" button.
        /// </summary>
        protected virtual void Update()
        {
            
        }

        /// <summary>
        /// What happens when the main button is pressed
        /// </summary>
        public virtual void ButtonPressed()
        {
            RGFadeInEvent.Trigger(FadeOutDuration, Tween, 0, true, fadeType: RGFader.FadeTypes.CloseDoor);
            // if the user presses the "Jump" button, we start the first level.
            StartCoroutine(LoadFirstLevel());
        }


        /// <summary>
        /// Loads the next level.
        /// </summary>
        /// <returns>The first level.</returns>
        protected virtual IEnumerator LoadFirstLevel()
        {
            yield return new WaitForSeconds(FadeOutDuration);
            RGSceneLoadingManager.LoadScene(NextLevel, RGFader.FadeTypes.CloseDoor);
        }
    }
}
