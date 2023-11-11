using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyGame.MGEntity
{
    public class EnemyCloseRangeSOBase : EnemyStateSOBase
    {
        [SerializeField, BoxGroup("Enter Attack")] protected float EnterAttackTime;
        [SerializeField, BoxGroup("Enter Attack")] protected float EnterAttackRoutineTime;
        [SerializeField, BoxGroup("Attack")] protected float AttackTime;
        [SerializeField, BoxGroup("Attack")] private float AttackRoutineTime;
        [SerializeField, BoxGroup("Attack Recovery")] protected float AttackRecoverTime;

        protected float _enterAttackTimer;
        protected float _attackTimer;
        protected float _attackRecoverTimer;

        protected bool _enterAttack;
        protected bool _attack;
        protected bool _attackRecover;

        protected bool _enterAttackEventTrigged;
        protected bool _attackEventTriggered;
        protected bool _attackRecoverEventTriggered;

        //Collision
        protected Collider2D[] _detectedEnemies;
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

            _enterAttackTimer = EnterAttackTime;
            _attackTimer = AttackTime;
            _attackRecoverTimer = AttackRecoverTime;

            _enterAttack = true;
            _attack = false;
            _attackRecover = false;
            
            _attackRecoverEventTriggered = false;
            _attackEventTriggered = false;
            _enterAttackEventTrigged = false;
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

            if (_enterAttackTimer > 0f)
            {
                _enterAttackTimer -= Time.deltaTime;

                if(!_enterAttackEventTrigged)
                {
                    EnterAttackEvent();
                }
            }
            else if (_attackTimer > 0f)
            {
                _attackTimer -= Time.deltaTime;

                if (!_attackEventTriggered)
                {
                    AttackEvent();
                }
            }
            else if (_attackRecoverTimer > 0f)
            {
                _attackRecoverTimer -= Time.deltaTime;

                if (!_attackRecoverEventTriggered)
                {
                    AttackRecoverEvent();
                }
            }
            else if (_attackRecoverTimer <= 0)
            {
                StateMachine.ChangeState(Enemy.IdleState);
            }


        }
        protected virtual void EnterAttackEvent()
        {
            _enterAttackEventTrigged = true;
            _enterAttack = true;

            MGSystem.RGCoroutineCaller.Instance.StartCoroutine(EnterAttackRoutine(EnterAttackRoutineTime));
        }
        protected virtual IEnumerator EnterAttackRoutine(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            DOAfterEnterAttackRoutine();
        }
        protected virtual void DOAfterEnterAttackRoutine()
        {
        }
        protected virtual void AttackEvent()
        {
            _attackEventTriggered = true;
            _enterAttack = false;
            _attack = true;

            MGSystem.RGCoroutineCaller.Instance.StartCoroutine(AttackRoutine(AttackRoutineTime));
        }
        protected virtual IEnumerator AttackRoutine(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            DOAfterAttackRoutine();
        }
        protected virtual void DOAfterAttackRoutine()
        {
        }
        protected virtual void AttackRecoverEvent()
        {
            _attackRecoverEventTriggered = true;
            _attack = false;
            _attackRecover = true;
        }
        public override void ResetValues()
        {
            base.ResetValues();
        }
    }
}
