using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.MGSystem;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Move SO/Move To Player"), fileName = ("Move - MoveToPlayer"))]
    public class EMMoveToPlayer : EnemyMoveSOBase
    {
        protected Vector2 _playerPosDir;

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

            _playerPosDir = CurrentPlayerPos - CurrentPos;

            Movement.SetVelocity(_playerPosDir, MoveSpeed);
        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if(_longRangeDetected)
            {
                StateMachine.ChangeState(Enemy.LongRangeState);
            }
        }
    }
}
