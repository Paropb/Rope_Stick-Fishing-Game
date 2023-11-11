using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player) : base(player)
        {
        }

        public override void AnimationTriggerEvent()
        {
            base.AnimationTriggerEvent();

            Player.IdleInstance.DoAnimationTriggerLogic();
        }

        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Player.IdleInstance.DoChangeStateCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Player.IdleInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Player.IdleInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Player.IdleInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Player.IdleInstance.DoUpdateLogic();
        }
    }
}
