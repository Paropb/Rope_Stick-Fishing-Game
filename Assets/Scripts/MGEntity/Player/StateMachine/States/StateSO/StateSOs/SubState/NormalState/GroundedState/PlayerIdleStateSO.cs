using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerIdleStateSO : PlayerGroundedStateSO
    {
        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if (StateMachine.ChangedStateAtFrame)
            {
                return;
            }

            if (Player.HInput.x != 0)
            {
                StateMachine.ChangeState(Player.GroundMoveState);
            }
        }
        
    }
}
