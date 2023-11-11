using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Close Range SO Gravity/Attack With Anim"), fileName = ("CloseRange G - AttackWithAnim"))]
    public class ECGAttackWithAnim : EnemyCloseRangeSOGravity
    {
        protected Animator Anim;
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            Movement.SetVelocityZero();
            Movement.FlipTo(Enemy.transform, CurrentPlayer.transform, Constants.FAST);
        }
        public override void DoExitLogic()
        {
            base.DoExitLogic();

            Anim.Play("None");
        }
        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            if(_attack)
            {
                CollisionSenses.AttackDetection(Movement, Enemy.Hit.Comp, out _detectedEnemies);
            }
        }
        public override void Initialize(Enemy enemy)
        {
            base.Initialize(enemy);

            Anim = Enemy.transform.Find("Weapon SR").GetComponent<Animator>();
        }
        protected override void AttackEvent()
        {
            base.AttackEvent();

            Anim.SetTrigger("Attack");
        }
    }
}
