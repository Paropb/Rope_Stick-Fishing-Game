using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyGame.CoreSystem;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Close Range SO/Dash Attack"), fileName = ("CloseRange - DashAttack"))]
    public class ECDashAttack : EnemyCloseRangeSOBase
    {
        [SerializeField] private float AttackSpeed;
        private CoreHit Hit;

        [Header("Colors")]
        [SerializeField] private Color AttackColor;
        [SerializeField] private Color OriginalColor;

        protected Vector2 _playerPosDir;
       
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            Enemy.SR.DOColor(AttackColor, EnterAttackTime - EnterAttackRoutineTime);

            Movement.SetVelocityZero();

            Hit = Enemy.transform.GetComponentInChildren<CoreHit>();
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();

        }

        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            if(_attack)
            {
                Movement.SetVelocity(_playerPosDir, AttackSpeed);

                CollisionSenses.AttackDetection(Movement, Hit, out _detectedEnemies);
            }
            else if(_attackRecover)
            {
                Movement.SetVelocityZero();
            }

        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();
        }
        protected override void DOAfterEnterAttackRoutine()
        {
            base.DOAfterEnterAttackRoutine();

            Enemy.SR.color = Color.white;
            _playerPosDir = CurrentPlayerPos - CurrentPos;
        }
        protected override void AttackEvent()
        {
            base.AttackEvent();

            Enemy.SR.color = AttackColor;
            CollisionSenses.AttackSense.Collider.isTrigger = true;
        }
        protected override void AttackRecoverEvent()
        {
            base.AttackRecoverEvent();

            CollisionSenses.AttackSense.Collider.isTrigger = false;
            Enemy.SR.DOColor(OriginalColor, 0f);
        }
    }
}
