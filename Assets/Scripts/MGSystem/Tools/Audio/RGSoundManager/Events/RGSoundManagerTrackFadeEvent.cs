using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    /// <summary>
    /// This event will let you order the RGSoundManager to fade an entire track's sounds' volume towards the specified FinalVolume
    ///
    /// Example : RGSoundManagerTrackFadeEvent.Trigger(RGSoundManager.RGSoundManagerTracks.Music, 2f, 0.5f, new RGTweenType(RGTween.RGTweenCurve.EaseInCubic));
    /// will fade the volume of the music track towards 0.5, over 2 seconds, using an ease in cubic tween 
    /// </summary>
    public struct RGSoundManagerTrackFadeEvent
    {
        /// the track to fade the volume of
        public RGSoundManager.RGSoundManagerTracks Track;
        /// the duration of the fade, in seconds
        public float FadeDuration;
        /// the final volume to fade towards
        public float FinalVolume;
        /// the tween to use when fading
        public RGTweenType FadeTween;

        public RGSoundManagerTrackFadeEvent(RGSoundManager.RGSoundManagerTracks track, float fadeDuration, float finalVolume, RGTweenType fadeTween)
        {
            Track = track;
            FadeDuration = fadeDuration;
            FinalVolume = finalVolume;
            FadeTween = fadeTween;
        }

        static RGSoundManagerTrackFadeEvent e;
        public static void Trigger(RGSoundManager.RGSoundManagerTracks track, float fadeDuration, float finalVolume, RGTweenType fadeTween)
        {
            e.Track = track;
            e.FadeDuration = fadeDuration;
            e.FinalVolume = finalVolume;
            e.FadeTween = fadeTween;
            RGEventManager.TriggerEvent(e);
        }
    }
}
