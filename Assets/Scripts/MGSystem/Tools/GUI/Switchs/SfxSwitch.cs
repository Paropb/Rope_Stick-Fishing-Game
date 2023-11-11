using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGSystem
{
    public class SfxSwitch : MonoBehaviour
    {
        public virtual void On()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.UnmuteTrack, RGSoundManager.RGSoundManagerTracks.Sfx);
        }

        public virtual void Off()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.MuteTrack, RGSoundManager.RGSoundManagerTracks.Sfx);
        }

    }
}
