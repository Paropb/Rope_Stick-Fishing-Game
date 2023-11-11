using MyGame.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class EnemyState: State
    {
	    protected Enemy Enemy { get; private set; }
        protected EnemyStateMachine StateMachine { get => Enemy.StateMachine; }
        protected Movement Movement { get => Enemy.Movement.Comp; }
        protected CollisionSenses CollisionSenses { get => Enemy.CollisionSenses.Comp; }
        protected CoreHit Hit { get => Enemy.Hit.Comp; }
        public EnemyState(Enemy enemy)
        {
            Enemy = enemy;
        }
    }
}
