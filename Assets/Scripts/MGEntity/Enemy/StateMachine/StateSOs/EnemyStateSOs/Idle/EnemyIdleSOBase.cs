using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyIdleSOBase : EnemyStateSOBase
    {
        [SerializeField] protected float MinIdleTime;
        [SerializeField] protected float MaxIdleTime;

        protected float _idleTimer;

        protected bool _idleTimeOver;
        public override void DoAnimationTriggerLogic()
        {
            base.DoAnimationTriggerLogic();
        }

        public override void DoCheckLogic()
        {
            base.DoCheckLogic();
        }

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            Movement.SetVelocityZero();

            _idleTimer = Random.Range(MinIdleTime, MaxIdleTime);
            _idleTimeOver = false;
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

            if(_idleTimer > 0)
            {
                _idleTimer -= Time.deltaTime;
                _idleTimeOver = false;
            }
            else
            {
                _idleTimeOver = true;
            }
        }

        public override void ResetValues()
        {
            base.ResetValues();
        }
    }
}
