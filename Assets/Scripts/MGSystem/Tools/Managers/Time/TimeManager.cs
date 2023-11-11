using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.MGEntity;
using MyGame.MGSystem;

public class TimeManager : PersistentSingleton<TimeManager>
{
    private Coroutine freezeTimeRoutine;
    private Coroutine bulletTimeRoutine;
    private float _defaultTimeScale = 1f;
    private float _defaultFixedDeltaTime;
    private float _initialDefaultTimeScale;
    protected override void Awake()
    {
        base.Awake();

        _defaultTimeScale = Time.timeScale;
        _defaultFixedDeltaTime = Time.fixedDeltaTime;
        _initialDefaultTimeScale = Time.timeScale;
    }
    private IEnumerator FreezeTimeCorotine(float timeScale, float duration, bool disablePlayerInput = false)
    {
        yield return null;
        //if(disablePlayerInput)
        //    PlayerInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("TimeFreeze");
        //Time.timeScale = timeScale;
        //yield return new WaitForSecondsRealtime(duration);
        //Time.timeScale = _defaultTimeScale;
        //if(disablePlayerInput)
        //    PlayerInputHandler.Instance.PlayerInput.SwitchCurrentActionMap("GamePlay");
    }
    private IEnumerator BulletTimeCoroutine(float timeScale, float duration)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime /= timeScale;
        _defaultTimeScale = timeScale;
        yield return new WaitForSecondsRealtime(duration);
        StopBulletTime();
    }
    public bool IsTimeFreezed { get; private set; }
    public void FreezeTime(float duration, bool disablePlayerInput = false)
    {
        if (freezeTimeRoutine != null)
            StopCoroutine(freezeTimeRoutine);
        freezeTimeRoutine = StartCoroutine(FreezeTimeCorotine(0f, duration, disablePlayerInput));
    }
    public void StartBulletTime(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime *= timeScale;
        _defaultTimeScale = timeScale;
    }
    public void StopBulletTime()
    {
        _defaultTimeScale = _initialDefaultTimeScale;
        Time.timeScale = _defaultTimeScale;
        Time.fixedDeltaTime = _defaultFixedDeltaTime;
    }
    public void BulletTime(float timeScale, float duration)
    {
        if(bulletTimeRoutine != null)
            StopCoroutine(bulletTimeRoutine);
        bulletTimeRoutine = StartCoroutine(BulletTimeCoroutine(timeScale, duration));
    }
}
