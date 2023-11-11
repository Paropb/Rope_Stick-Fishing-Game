using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using static MyGame.MGSystem.RGSoundManager;
using UnityEngine.SceneManagement;

namespace MyGame.MGSystem
{
    public class TestSoundManager : MonoBehaviour
    {
        [Header("RGSoundManagerSoundPlayEvent")]
        public AudioClip SoundClipLoop;
        public RGSoundManager.RGSoundManagerTracks UseTracks;
        public AudioMixerGroup OtherAudioGroup;

        [RGInspectorButton("TestSoundManagerSoundPlayEvent")]
        public bool TestSoundManagerSoundPlayEventButton;
        protected virtual void TestSoundManagerSoundPlayEvent()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = true;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = UseTracks;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipLoop, options);
        }
        [RGInspectorButton("TestUseOtherAudioGrup")]
        public bool TestUseOtherAudioGrupButton;
        protected virtual void TestUseOtherAudioGrup()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = true;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = RGSoundManager.RGSoundManagerTracks.Music;
            options.AudioGroup = OtherAudioGroup;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipLoop, options);
        }
        [RGInspectorButton("TestSoundManagerSoundPlayNoloop")]
        public bool TestSoundManagerSoundPlayNoloopButton;
        protected virtual void TestSoundManagerSoundPlayNoloop()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = true;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = UseTracks;
            options.Loop = false;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipLoop, options);
        }
        [RGInspectorButton("TestFaderSound")]
        public bool TestFaderSoundButton;
        protected virtual void TestFaderSound()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = true;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = UseTracks;
            options.Fade = true; 
            options.FadeInitialVolume = 1;
            options.Volume = 0;
            options.FadeDuration = 10;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipLoop, options);

        }
        public AudioClip SoundClipNoLoop;
        [RGInspectorButton("TestSoundManagerNoLoop")]
        public bool TestSoundManagerNoLoopButton;

        protected virtual void TestSoundManagerNoLoop()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = false;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = RGSoundManager.RGSoundManagerTracks.Sfx;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipNoLoop, options);
        }
        [RGInspectorButton("TestSoloSingleTrack")]
        public bool TestSoloSingleTrackButton;
        protected virtual void TestSoloSingleTrack()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = false;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = RGSoundManager.RGSoundManagerTracks.Music;
            options.SoloSingleTrack = true;
            options.AutoUnSoloOnEnd = true;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipNoLoop, options);
        }
        [RGInspectorButton("TestSoloAllTracks")]
        public bool TestSoloAllTracksButton;
        protected virtual void TestSoloAllTracks()
        {
            RGSoundManagerPlayOptions options = RGSoundManagerPlayOptions.Default;
            options.Loop = false;
            options.Location = Vector3.zero;
            options.RgSoundManagerTrack = RGSoundManager.RGSoundManagerTracks.Sfx;
            options.SoloAllTracks = true;
            options.AutoUnSoloOnEnd = true;
            RGSoundManagerSoundPlayEvent.Trigger(SoundClipNoLoop, options);
        }
        [Header("RGSoundManagerTrackEvent")]
        public RGSoundManager.RGSoundManagerTracks RGSoundManagerTrackEventTestTracks = RGSoundManagerTracks.Music;
        public float TestSetVolume = 1f;

        [RGInspectorButton("MuteTrack")]
        public bool MuteTrackButton;
        protected virtual void MuteTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.MuteTrack, RGSoundManagerTracks.Music);
        }
        [RGInspectorButton("UnmuteTrack")]
        public bool UnmuteTrackButton;
        protected virtual void UnmuteTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.UnmuteTrack, RGSoundManagerTracks.Music, 0);
        }

        [RGInspectorButton("SetVolumeTrack")]
        public bool SetVolumeTrackButton;
        protected virtual void SetVolumeTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.SetVolumeTrack, RGSoundManagerTrackEventTestTracks, TestSetVolume);
        }


        [RGInspectorButton("PauseTrack")]
        public bool PauseTrackButton;
        protected virtual void PauseTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.PauseTrack, RGSoundManagerTracks.Music, 0.5f);
        }

        [RGInspectorButton("StopTrack")]
        public bool StopTrackButton;
        protected virtual void StopTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.StopTrack, RGSoundManagerTracks.Music, 0.5f);
        }

        [RGInspectorButton("PlayTrack")]
        public bool PlayTrackButton;
        protected virtual void PlayTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.PlayTrack, RGSoundManagerTracks.Music, 0.5f);
        }

        [RGInspectorButton("FreeTrack")]
        public bool FreeTrackButton;
        protected virtual void FreeTrack()
        {
            RGSoundManagerTrackEvent.Trigger(RGSoundManagerTrackEventTypes.FreeTrack, RGSoundManagerTracks.Music, 0.5f);
        }

        [Header("RGSoundManagerEvent")]
        [RGInspectorButton("SaveSettings")]
        public bool SaveSettingsButton;
        protected virtual void SaveSettings()
        {
            RGSoundManagerEvent.Trigger(RGSoundManagerEventTypes.SaveSettings);
        }

        [RGInspectorButton("LoadSettings")]
        public bool LoadSettingsButton;
        protected virtual void LoadSettings()
        {
            RGSoundManagerEvent.Trigger(RGSoundManagerEventTypes.LoadSettings);
        }

        [RGInspectorButton("ResetSettings")]
        public bool ResetSettingsButton;
        protected virtual void ResetSettings()
        {
            RGSoundManagerEvent.Trigger(RGSoundManagerEventTypes.ResetSettings);
        }

        [Header("RGSoundManagerSoundControlEvent")]
        [RGInspectorButton("Pause")]
        public bool PauseButton;
        protected virtual void Pause()
        {
            RGSoundManagerSoundControlEvent.Trigger(RGSoundManagerSoundControlEventTypes.Pause, 1);
        }

        [RGInspectorButton("Resume")]
        public bool ResumeButton;
        protected virtual void Resume()
        {
            RGSoundManagerSoundControlEvent.Trigger(RGSoundManagerSoundControlEventTypes.Resume, 1);
        }

        [RGInspectorButton("Stop")]
        public bool StopButton;
        protected virtual void Stop()
        {
            RGSoundManagerSoundControlEvent.Trigger(RGSoundManagerSoundControlEventTypes.Stop, 1);
        }

        [RGInspectorButton("Free")]
        public bool FreeButton;
        protected virtual void Free()
        {
            RGSoundManagerSoundControlEvent.Trigger(RGSoundManagerSoundControlEventTypes.Free, 1);
        }
        [Header("RGSoundManagerSoundFadeEvent")]
        public RGTweenType TestSoundFadeEventTweenType;
        [RGInspectorButton("TestSoundFadeEvent")]
        public bool TestSoundFadeEventButton;
        protected virtual void TestSoundFadeEvent()
        {
            RGSoundManagerSoundFadeEvent.Trigger(0, 10, 0, TestSoundFadeEventTweenType);
        }

        [Header("RGSoundManagerAllSoundsControlEvent")]
        [RGInspectorButton("AllSoundsPause")]
        public bool AllSoundsPauseButton;
        protected virtual void AllSoundsPause()
        {
            RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.Pause);
        }

        [RGInspectorButton("AllSoundsPlay")]
        public bool AllSoundsPlayButton;
        protected virtual void AllSoundsPlay()
        {
            RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.Play);
        }

        [RGInspectorButton("AllSoundsStop")]
        public bool AllSoundsStopButton;
        protected virtual void AllSoundsStop()
        {
            RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.Stop);
        }

        [RGInspectorButton("AllSoundsFree")]
        public bool AllSoundsFreeButton;
        protected virtual void AllSoundsFree()
        {
            RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.Free);
        }

        [RGInspectorButton("AllSoundsFreeAllButPersistent")]
        public bool AllSoundsFreeAllButPersistentButton;
        protected virtual void AllSoundsFreeAllButPersistent()
        {
            RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.FreeAllButPersistent);
        }

        [RGInspectorButton("AllSoundsFreeAllLooping")]
        public bool AllSoundsFreeAllLoopingButton;
        protected virtual void AllSoundsFreeAllLooping()
        {
            RGSoundManagerAllSoundsControlEvent.Trigger(RGSoundManagerAllSoundsControlEventTypes.FreeAllLooping);
        }


        [Header("RGSoundManagerTrackFadeEvent")]

        public RGSoundManager.RGSoundManagerTracks TrackFadeEventTracks = RGSoundManagerTracks.Music;
        public RGTweenType TrackFadeEventTweenType;

        [RGInspectorButton("TestTrackFadeEvent")]
        public bool TestTrackFadeEventButton;
        protected virtual void TestTrackFadeEvent()
        {
            RGSoundManagerTrackFadeEvent.Trigger(TrackFadeEventTracks, 10, 0, TrackFadeEventTweenType);
        }
        [Header("RGSfxEvent")]
        public AudioClip OnRGSfxEventClip;
        [RGInspectorButton("TestRGSfxEvent")]
        public bool TestRGSfxEventButton;
        protected virtual void TestRGSfxEvent()
        {
            RGSfxEvent.Trigger(OnRGSfxEventClip);
        }

        [Header("SceneChange")]
        public string NextSceneName;
        [RGInspectorButton("TestSceneChange")]
        public bool TestSceneChangeButton;
        protected virtual void TestSceneChange()
        {
            SceneManager.LoadScene(NextSceneName);
        }
        public void GoToNextLevel()
        {
            SceneManager.LoadScene(NextSceneName);
        }


    }
}
