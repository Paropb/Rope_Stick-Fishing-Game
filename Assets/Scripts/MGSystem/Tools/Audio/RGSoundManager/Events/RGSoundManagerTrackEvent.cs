using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public enum RGSoundManagerTrackEventTypes
    {
        MuteTrack,
        UnmuteTrack,
        SetVolumeTrack,
        PlayTrack,
        PauseTrack,
        StopTrack,
        FreeTrack
    }

    /// <summary>
    /// This feedback will let you mute, unmute, play, pause, stop, free or set the volume of a selected track
    ///
    /// Example :  RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.PauseTrack,RGSoundManager.RGSoundManagerTracks.UI);
    /// will pause the entire UI track
    /// </summary>
    public struct RGSoundManagerTrackEvent
    {
        /// the order to pass to the track
        public RGSoundManagerTrackEventTypes TrackEventType;
        /// the track to pass the order to
        public RGSoundManager.RGSoundManagerTracks Track;
        /// if in SetVolume mode, the volume to which to set the track to
        public float Volume;

        public RGSoundManagerTrackEvent(RGSoundManagerTrackEventTypes trackEventType, RGSoundManager.RGSoundManagerTracks track = RGSoundManager.RGSoundManagerTracks.Master, float volume = 1f)
        {
            TrackEventType = trackEventType;
            Track = track;
            Volume = volume;
        }

        static RGSoundManagerTrackEvent e;

        public static void Trigger(RGSoundManagerTrackEventTypes trackEventType, RGSoundManager.RGSoundManagerTracks track = RGSoundManager.RGSoundManagerTracks.Master, float volume = 1f)
        {
            e.TrackEventType = trackEventType;
            e.Track = track;
            e.Volume = volume;
            RGEventManager.TriggerEvent(e);
        }

    }
}
