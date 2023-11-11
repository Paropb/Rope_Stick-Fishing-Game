using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyGame.MGEntity
{
    public class EnemyCloseRangeSOGravity : EnemyCloseRangeSOBase
    {
        protected bool _grounded;
        protected bool _useGravity;
        public override void Initialize(Enemy enemy)
        {
            base.Initialize(enemy);
            _useGravity = true;
        }
        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            _grounded = CollisionSenses.Grounded;

            if (_grounded)
            {
                Movement.SetVelocityY(0);
            }
            else
            {
                if (_useGravity)
                {
                    Movement.SimulateGravity();
                }
            }

        }
    }
}

