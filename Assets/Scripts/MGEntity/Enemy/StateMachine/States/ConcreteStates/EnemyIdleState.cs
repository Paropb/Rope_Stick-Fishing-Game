using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(Enemy enemy) : base(enemy)
        {
        }

        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Enemy.enemyIdleInstance.DoCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Enemy.enemyIdleInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.enemyIdleInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Enemy.enemyIdleInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Enemy.enemyIdleInstance.DoUpdateLogic();

            
        }
    }
}
