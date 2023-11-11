using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public enum RGSoundManagerAllSoundsControlEventTypes
    {
        Pause, Play, Stop, Free, FreeAllButPersistent, FreeAllLooping
    }

    /// <summary>
    /// This event will let you pause/play/stop/free all sounds playing through the RGSoundManager at once
    ///
    /// Example : RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.Stop);
    /// will stop all sounds playing at once
    /// </summary>
    public struct RGSoundManagerAllSoundsControlEvent
    {
        public RGSoundManagerAllSoundsControlEventTypes EventType;

        public RGSoundManagerAllSoundsControlEvent(RGSoundManagerAllSoundsControlEventTypes eventType)
        {
            EventType = eventType;
        }

        static RGSoundManagerAllSoundsControlEvent e;
        public static void Trigger(RGSoundManagerAllSoundsControlEventTypes eventType)
        {
            e.EventType = eventType;
            RGEventManager.TriggerEvent(e);
        }
    }
}
