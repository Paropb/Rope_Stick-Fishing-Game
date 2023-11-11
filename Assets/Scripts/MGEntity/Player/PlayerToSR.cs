using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerToSR : MonoBehaviour
    {
        public Player Player;

        public virtual void AnimationTriggerEvent()
        {
            Player.StateMachine.CurrentState.AnimationTriggerEvent();
        }
    }
}
