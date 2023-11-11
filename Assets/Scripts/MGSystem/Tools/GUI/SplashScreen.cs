using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGame.MGSystem
{
    public class SplashScreen: MonoBehaviour
    {
        /// the level to load after the start screen
        [Tooltip("the level to load after the start screen")]
        public string NextLevel;
        /// the delay after which the level should auto skip (if less than 1s, won't autoskip)
        [Tooltip("the delay after which the level should auto skip (if less than 1s, won't autoskip)")]
        public float AutoSkipDelay = 0f;
        /// If want to skip lading scene, you can set SkipLading true
        [Tooltip("If want to skip lading scene, you can set SkipLoding true")]
        public bool SkipLoading = true;


        [Header("Fades")]
        /// the duration of the fade in of the start screen, in seconds
        [Tooltip("the duration of the fade in of the start screen, in seconds")]
        public float FadeInDuration = 1f;
        /// the duration of the fade out of the start screen, in seconds
        [Tooltip("the duration of the fade out of the start screen, in seconds")]
        public float FadeOutDuration = 1f;
        /// whick fadeid should be excuted
        [Tooltip("whick fadeid should be excuted")]
        public int FadeId = 0;
        /// before load next scene,active fadein effect
        [Tooltip("before load next scene,active fadein effect")]
        public bool UseFadein = true;
        /// the tween type this fade should happen on
        public RGTweenType Tween;

        /// <summary>
		/// Initialization
		/// </summary>
		protected virtual void Awake()
        {
            if (AutoSkipDelay > 0f)
            {
                StartCoroutine(LoadNextLevel());
            }

        }


        /// <summary>
		/// Loads the next level.
		/// </summary>
		/// <returns>The first level.</returns>
		protected virtual IEnumerator LoadNextLevel()
        {
            yield return new WaitForSeconds(AutoSkipDelay);
            if (UseFadein)
            {
                RGFadeInEvent.Trigger(FadeInDuration, Tween, FadeId, true);
                yield return new WaitForSeconds(FadeInDuration + 0.5f);
            }
            if (SkipLoading)
            {
                SceneManager.LoadScene(NextLevel);
            }
            else
            {
                RGSceneLoadingManager.LoadScene(NextLevel);
            }
        }
    }
}
