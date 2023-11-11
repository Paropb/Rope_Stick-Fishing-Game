using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Idle SO/Simple Idle"), fileName = ("Idle - SimpleIdle"))]
    public class EISimpleIdle : EnemyIdleSOBase
    {
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();
        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if(_longRangeDetected)
            {
                StateMachine.ChangeState(Enemy.LongRangeState);
            }
            else if (_idleTimeOver)
            {
                StateMachine.ChangeState(Enemy.MoveState);
            }
        }

        public override void ResetValues()
        {
            base.ResetValues();
        }
    }
}
