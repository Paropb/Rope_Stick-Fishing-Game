using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MyGame.MGSystem
{
    public class RGAudioEvents
    {

    }

    /// <summary>
    /// A struct used to trigger sounds
    /// </summary>
    public struct RGSfxEvent
    {
        public delegate void Delegate(AudioClip clipToPlay, AudioMixerGroup audioGroup = null, float volume = 1f, float pitch = 1f);
        static private event Delegate OnEvent;


        static public void Register(Delegate callback)
        {
            OnEvent += callback;
        }

        static public void Unregister(Delegate callback)
        {
            OnEvent -= callback;
        }

        static public void Trigger(AudioClip clipToPlay, AudioMixerGroup audioGroup = null, float volume = 1f, float pitch = 1f)
        {
            OnEvent?.Invoke(clipToPlay, audioGroup, volume, pitch);
        }
    }
}
