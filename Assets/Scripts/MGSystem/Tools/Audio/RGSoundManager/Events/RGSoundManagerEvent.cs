using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public enum RGSoundManagerEventTypes
    {
        SaveSettings,
        LoadSettings,
        ResetSettings
    }


    /// <summary>
    /// This event will let you trigger a save/load/reset on the RGSoundManager settings
    ///
    /// Example : RGSoundManagerEvent.Trigger(RGSoundManagerEventTypes.SaveSettings);
    /// will save settings. 
    /// </summary>
    public struct RGSoundManagerEvent
    {
        public RGSoundManagerEventTypes EventType;

        public RGSoundManagerEvent(RGSoundManagerEventTypes eventType)
        {
            EventType = eventType;
        }

        static RGSoundManagerEvent e;
        public static void Trigger(RGSoundManagerEventTypes eventType)
        {
            e.EventType = eventType;
            RGEventManager.TriggerEvent(e);
        }
    }

}
