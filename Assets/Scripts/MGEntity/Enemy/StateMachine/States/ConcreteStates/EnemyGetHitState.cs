using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyGetHitState : EnemyState
    {
        public EnemyGetHitState(Enemy enemy) : base(enemy)
        {
        }

        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Enemy.enemyGetHitInstance.DoCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Enemy.enemyGetHitInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.enemyGetHitInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Enemy.enemyGetHitInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Enemy.enemyGetHitInstance.DoUpdateLogic();
        }
    }
}
