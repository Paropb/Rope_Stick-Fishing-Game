using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.CoreSystem;

namespace MyGame.MGEntity
{
    public class PlayerState: State
    {
        protected Player Player { get; private set; }
        protected Movement Movement { get => Player.Movement.Comp; }
        public PlayerState(Player player): base()
        {
            Player = player;
        }
        public override void MGUpdate()
        {
            base.MGUpdate();
        }
        public override void MGFixedUpdate()
        {
            base.MGFixedUpdate();
        }
        public virtual void AnimationTriggerEvent() { }
        
    }
}
