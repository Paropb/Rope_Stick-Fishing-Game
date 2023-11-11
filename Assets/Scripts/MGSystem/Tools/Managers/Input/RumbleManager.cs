using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyGame.MGEntity;
namespace MyGame.MGSystem
{
    public class RumbleManager : Singleton<RumbleManager>
    {
        private Gamepad gamepad;

        private Coroutine stopRumbleCoroutine;
	    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
        {
            //TODO: fix that
            //if(PlayerInputHandler.instance.PlayerInput.currentControlScheme == "Gamepad")
            //{
            //    gamepad = Gamepad.current;

            //    if (gamepad != null)
            //    {
            //        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
            //        stopRumbleCoroutine = StartCoroutine(StopRumble(duration));
            //    }
            //}
        }
        private IEnumerator StopRumble(float duration)
        {
            float elapsedTime = 0f;
            while(elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gamepad.SetMotorSpeeds(0f, 0f);
        }
    }
}
