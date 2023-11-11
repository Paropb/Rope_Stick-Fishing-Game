using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Move SO Gravity/Simple Patrol"), fileName = ("Move G - Simple Patrol"))]
    public class EMGSimplePatrol : EnemyMoveSOGravity
    {
        protected bool _wallInFront;
        protected bool _ledgeInFront;
        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            _wallInFront = CollisionSenses.WallInFront(Movement);
            _ledgeInFront = CollisionSenses.LedgeInFront(Movement);

            if(_wallInFront ||_ledgeInFront)
            {
                Movement.SetVelocityX(0);
            }
            else
            {
                Movement.SetVelocityX(MoveSpeed * Movement.FacingDirectionInt);
            }
        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if(_closeRangeDetected)
            {
                StateMachine.ChangeState(Enemy.CloseRangeState);
            }
            else if(_wallInFront || _ledgeInFront)
            {
                StateMachine.ChangeState(Enemy.IdleState);
            }
        }
    }
}
