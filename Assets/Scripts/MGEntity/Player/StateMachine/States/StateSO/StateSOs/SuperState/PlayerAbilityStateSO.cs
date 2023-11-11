using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerAbilityStateSO : PlayerStateSOBase
    {
        [SerializeField] protected float AbilityDuration;

        protected bool _abilityFinished;
        protected float _abilityTimer;
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            _abilityFinished = false;
            _abilityTimer = AbilityDuration;
        }
        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if (StateMachine.ChangedStateAtFrame)
            {
                return;
            }

            if (_abilityTimer <= 0)
            {
                _abilityFinished = true;
            }
            else
            {
                _abilityTimer -= Time.deltaTime;
            }
        }
    }
}
