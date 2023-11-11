using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyLongRangeState : EnemyState
    {
        public EnemyLongRangeState(Enemy enemy) : base(enemy)
        {
        }
        public override void DoChangeStateChecks()
        {
            base.DoChangeStateChecks();

            Enemy.enemyLongRangeInstance.DoCheckLogic();
        }

        public override void Enter()
        {
            base.Enter();

            Enemy.enemyLongRangeInstance.DoEnterLogic();
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.enemyLongRangeInstance.DoExitLogic();
        }

        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();

            Enemy.enemyLongRangeInstance.DoFixedUpdateLogic();
        }

        public override void MGUpdate()
        {
            base.MGUpdate();

            Enemy.enemyLongRangeInstance.DoUpdateLogic();
        }
    }
}
