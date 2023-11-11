using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.CoreSystem;
using MyGame.MGSystem;

namespace MyGame.MGEntity
{
    public class EnemyStateSOBase : ScriptableObject
    {
        protected CollisionSenses CollisionSenses { get => Enemy.CollisionSenses.Comp; }
        protected Movement Movement { get => Enemy.Movement.Comp;}

        protected bool _longRangeDetected;
        protected bool _closeRangeDetected;
        protected Enemy Enemy { get; private set; }
        protected EnemyStateMachine StateMachine { get => Enemy.StateMachine; }
        protected Vector2 CurrentPos { get => Enemy.transform.position.ToVector2(); }
        protected Vector2 CurrentPlayerPos { get => GameManager.Instance.Player.transform.position; }
        protected Player CurrentPlayer { get => GameManager.Instance.Player; }
        public virtual void Initialize(Enemy enemy)
        {
            Enemy = enemy;
        }

        public virtual void DoEnterLogic() { }
        public virtual void DoExitLogic() { ResetValues(); }
        public virtual void DoUpdateLogic() { }
        public virtual void DoFixedUpdateLogic() 
        {
            _longRangeDetected = CollisionSenses.LongRangeDetected();
            _closeRangeDetected = CollisionSenses.CloseRangeDetected();
        }
        public virtual void DoCheckLogic() { }
        public virtual void DoAnimationTriggerLogic() { }
        public virtual void ResetValues() 
        {
            
        }
    }
}
