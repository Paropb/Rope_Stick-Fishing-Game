using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerGroundMoveState : PlayerGroundedState
    {
        public PlayerGroundMoveState(Player player) : base(player)
        {
        }
        public override void AnimationTriggerEvent()
        {
            base.AnimationTriggerEvent();

            Player.GroundMoveInstance.DoAnimationTriggerLogic();
        }

        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Player.GroundMoveInstance.DoChangeStateCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Player.GroundMoveInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Player.GroundMoveInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Player.GroundMoveInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Player.GroundMoveInstance.DoUpdateLogic();
        }
    }
}
