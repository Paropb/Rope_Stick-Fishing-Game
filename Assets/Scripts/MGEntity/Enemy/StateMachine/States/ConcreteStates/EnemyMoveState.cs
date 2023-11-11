using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyMoveState : EnemyState
    {
        public EnemyMoveState(Enemy enemy) : base(enemy)
        {
        }

        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Enemy.enemyMoveInstance.DoCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Enemy.enemyMoveInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.enemyMoveInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Enemy.enemyMoveInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Enemy.enemyMoveInstance.DoUpdateLogic();
        }
    }
}
