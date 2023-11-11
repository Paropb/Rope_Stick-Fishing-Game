using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Idle SO Gravity/Simple Idle"), fileName = ("Idle G - SimpleIdle"))]
    public class EIGSimpleIdle : EnemyIdleSOGravity
    {
        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if (_closeRangeDetected)
            {
                StateMachine.ChangeState(Enemy.CloseRangeState);
            }
            else if(_longRangeDetected)
            {
                StateMachine.ChangeState(Enemy.LongRangeState);
            }
            else if (_idleTimeOver)
            {
                StateMachine.ChangeState(Enemy.MoveState);
                Movement.Flip(Enemy.transform, Constants.FAST);
            }
        }
        public override void DoExitLogic()
        {
            base.DoExitLogic();

        }

    }
}
