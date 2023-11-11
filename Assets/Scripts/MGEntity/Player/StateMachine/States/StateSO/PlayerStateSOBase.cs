using MyGame.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerStateSOBase : ScriptableObject
    {
        protected CollisionSenses CollisionSenses { get => Player.CollisionSenses.Comp; }
        protected Movement Movement { get => Player.Movement.Comp; }
        protected Player Player { get; private set; }
        protected InputHandler InputHandler { get => Player.InputHandler; }
        protected PlayerDataSO Data { get => Player.Data; }
        protected PlayerStateMachine StateMachine { get => Player.StateMachine; }
        protected Vector2 CurrentPos { get => Player.transform.position.ToVector2(); }
        public virtual void Initialize(Player player)
        {
            Player = player;
        }

        public virtual void DoEnterLogic() { }
        public virtual void DoExitLogic() { ResetValues(); }
        public virtual void DoUpdateLogic() { }
        public virtual void DoFixedUpdateLogic()
        {
        }
        public virtual void DoChangeStateCheckLogic() { }
        public virtual void DoAnimationTriggerLogic() { }
        public virtual void ResetValues()
        {

        }
    }
}
