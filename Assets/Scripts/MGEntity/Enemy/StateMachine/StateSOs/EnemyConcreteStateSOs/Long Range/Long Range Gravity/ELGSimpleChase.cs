using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Long Range Gravity/Simple Chase"), fileName = ("LongRange G - SimpleChase"))]
    public class ELGSimpleChase : EnemyLongRangeSOGravity
    {
        [SerializeField] private float ChaseSpeed;
        [SerializeField] private float EnterChaseTime;

        protected Vector2 _playerPosDir;
        protected float _enterChaseTimer;

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            _enterChaseTimer = EnterChaseTime;
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();
        }

        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            _playerPosDir = CurrentPlayerPos - CurrentPos;

            if (_enterChaseTimer <= 0)
            {
                if(CurrentPlayerPos.x < CurrentPos.x)
                {
                    Movement.SetVelocityX(-ChaseSpeed);
                }
                else
                {
                    Movement.SetVelocityX(ChaseSpeed);
                }
            }
            else
            {
                Movement.SetVelocityX(0);
            }
        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if (_closeRangeDetected)
            {
                StateMachine.ChangeState(Enemy.CloseRangeState);
            }
            else
            {
                _enterChaseTimer -= Time.deltaTime;
            }
        }
    }
}
