using MyGame.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyGetHitSOBase: EnemyStateSOBase
    {
        protected float _knockBackTimer;
        protected bool _knockBacked;
        public virtual void KnockBack(Vector2 knockBackDir, float knockBackSpeed, float knockBackTime, Movement movement)
        {
            Movement.SetVelocity(movement.Directionize(knockBackDir), knockBackSpeed);
            _knockBackTimer = knockBackTime;
            _knockBacked = true;
        }
        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            if (_knockBacked)
            {
                if (_knockBackTimer <= 0)
                {
                    Movement.SimulateVelocityX(0f, Enemy.KnockBackable.Comp.KnockBackRecoverAccel);
                    Movement.SimulateVelocityY(0f, Enemy.KnockBackable.Comp.KnockBackRecoverAccel);

                    if(Movement.Velocity.magnitude < 0.1f)
                    {
                        StateMachine.ChangeState(Enemy.IdleState);
                    }
                }
            }
        }
        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if(_knockBacked)
            {
                _knockBackTimer -= Time.deltaTime;
            }
        }
        public override void ResetValues()
        {
            base.ResetValues();

            _knockBacked = false;
            _knockBackTimer = 0f;
        }
    }
}
