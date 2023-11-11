using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public enum RGSoundManagerSoundControlEventTypes
    {
        Pause,
        Resume,
        Stop,
        Free
    }

    /// <summary>
    /// An event used to control a specific sound on the RGSoundManager.
    /// You can either search for it by ID, or directly pass an audiosource if you have it.
    ///
    /// Example : RGSoundManagerSoundControlEvent.Trigger(RGSoundManagerSoundControlEventTypes.Stop, 33);
    /// will cause the sound(s) with an ID of 33 to stop playing
    /// </summary>
    public struct RGSoundManagerSoundControlEvent
    {
        /// the ID of the sound to control (has to match the one used to play it)
        public int SoundID;
        /// the control mode
        public RGSoundManagerSoundControlEventTypes RGSoundManagerSoundControlEventType;
        /// the audiosource to control (if specified)
        public AudioSource TargetSource;
        public RGSoundManagerSoundControlEvent(RGSoundManagerSoundControlEventTypes eventType, int soundID, AudioSource source = null)
        {
            SoundID = soundID;
            TargetSource = source;
            RGSoundManagerSoundControlEventType = eventType;
        }

        static RGSoundManagerSoundControlEvent e;
        public static void Trigger(RGSoundManagerSoundControlEventTypes eventType, int soundID, AudioSource source = null)
        {
            e.SoundID = soundID;
            e.TargetSource = source;
            e.RGSoundManagerSoundControlEventType = eventType;
            RGEventManager.TriggerEvent(e);
        }
    }
}
