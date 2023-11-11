using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Close Range Gravity/Shoot Bullets"), fileName = ("CloseRange G - ShootBullet"))]
    public class ECGShootBullet: EnemyCloseRangeSOGravity
    {
        [SerializeField] private float EscapeSpeed;
        private Transform Gun;
        private SpriteRenderer GunSR;
        [SerializeField] private MGBullet BulletPrefab;
        [SerializeField] private float BulletSpeed;

        [Header("Colors")]
        [SerializeField] private Color AttackColor;
        [SerializeField] private Color OriginalColor;


        protected Vector2 _playerPosDir;

        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            Movement.SetVelocityZero();

            Gun = Enemy.transform.Find("Gun");
            GunSR = Gun.GetComponentInChildren<SpriteRenderer>();

            GunSR.DOColor(AttackColor, _enterAttackTimer).SetId("GunSR");
        }

        public override void DoExitLogic()
        {
            base.DoExitLogic();

            GunSR.color = OriginalColor;
            DOTween.Kill("GunSR");
        }

        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();


            if (_enterAttack)
            {
                //Movement.SetVelocity(_playerPosDir * -1, EscapeSpeed);
                Gun.LookAt2D(CurrentPlayer.transform);
            }
            else if (_attack)
            {

            }
            else if (_attackRecover)
            {
                Movement.SetVelocityZero();
            }

        }

        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if (!_closeRangeDetected)
            {
                StateMachine.ChangeState(Enemy.IdleState);
            }
        }
        protected override void EnterAttackEvent()
        {
            base.EnterAttackEvent();

            _playerPosDir = CurrentPlayerPos - CurrentPos;
        }
        protected override void AttackEvent()
        {
            base.AttackEvent();

            _playerPosDir = CurrentPlayerPos - CurrentPos;
            GunSR.color = Color.white;
        }
        protected override void DOAfterAttackRoutine()
        {
            base.DOAfterAttackRoutine();

            GunSR.color = OriginalColor;
            MGBullet bullet = Instantiate(BulletPrefab);
            bullet.transform.position = Enemy.transform.position;
            bullet.Movement.Comp.SetVelocity(_playerPosDir, BulletSpeed);
        }
        protected override void AttackRecoverEvent()
        {
            base.AttackRecoverEvent();

            GunSR.DOColor(OriginalColor, 0f);
        }
    }
}
