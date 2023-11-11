using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyCloseRangeState : EnemyState
    {
        public EnemyCloseRangeState(Enemy enemy) : base(enemy)
        {
        }
        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Enemy.enemyCloseRangeInstance.DoCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Enemy.enemyCloseRangeInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.enemyCloseRangeInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Enemy.enemyCloseRangeInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Enemy.enemyCloseRangeInstance.DoUpdateLogic();
        }
    }
}
