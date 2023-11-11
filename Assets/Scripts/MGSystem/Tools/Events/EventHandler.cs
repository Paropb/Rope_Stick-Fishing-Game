using System;
using UnityEngine;
using UnityEngine.Events;

public static class EventHandler
{
	public static UnityAction playerLandEvent;
	public static void CallPlayerLandEvent()
    {
        playerLandEvent?.Invoke();
    }
    public static UnityAction playerOffLandEvent;
    public static void CallPlayerOffLandEvent()
    {
        playerOffLandEvent?.Invoke();
    }
    //InteractableObjects相关
    public static UnityAction<Vector2, Vector2, Vector2> interactiveHitEvent;
    public static void CallInteractiveHitEvent(Vector2 contactPoint, Vector2 contactNormal, Vector2 interactableObjectVelocity)
    {
        interactiveHitEvent?.Invoke(contactPoint, contactNormal, interactableObjectVelocity);
    }
    public static UnityAction interactiveStayEvent;
    public static void CallInteractiveStayEvent()
    {
        interactiveStayEvent?.Invoke();
    }
    public static UnityAction interactiveExitEvent;
    public static void CallInteractiveExitEvent()
    {
        interactiveExitEvent?.Invoke();
    }
    public static UnityAction<Sprite> playerDashEffectEvent;
    public static void CallPlayerDashEffectEvent(Sprite sprite)
    {
        playerDashEffectEvent?.Invoke(sprite);
    }
    //相机震动
    public static UnityAction<float, float> cameraShakeEvent;
    public static void CallCameraShakeEvent(float cameraShakeAmplitude, float cameraShakeTime)
    {
        cameraShakeEvent?.Invoke(cameraShakeAmplitude, cameraShakeTime);
    }
    //时间停止
    public static UnityAction<float, float> timeScaleEditEvent;
    public static void CallTimeScaleEditEvent(float timeScale, float stopTime)
    {
        timeScaleEditEvent?.Invoke(timeScale, stopTime);
    }
}
