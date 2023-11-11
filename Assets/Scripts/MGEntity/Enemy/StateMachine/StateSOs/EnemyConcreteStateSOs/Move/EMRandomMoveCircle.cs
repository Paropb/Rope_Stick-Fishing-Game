using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Move SO/Random Move Circle"), fileName = ("Move - RandomMoveCircle"))]
    public class EMRandomMoveCircle : EnemyMoveSOBase
    {
        [SerializeField] private float CircleRadius;
        [SerializeField] private float FinishDistance = 1f;


        private Vector2 _targetPosDir;
        private Vector2 _targetPos;

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            _targetPosDir = Random.insideUnitCircle * CircleRadius;
            _targetPos = _targetPosDir + CurrentPos;
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            Movement.SetVelocity(_targetPosDir, MoveSpeed);
        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if(Vector2.Distance(_targetPos, CurrentPos) < FinishDistance)
            {
                StateMachine.ChangeState(Enemy.IdleState);
            }
        }

        public override void ResetValues()
        {
            base.ResetValues();
        }
    }
}
