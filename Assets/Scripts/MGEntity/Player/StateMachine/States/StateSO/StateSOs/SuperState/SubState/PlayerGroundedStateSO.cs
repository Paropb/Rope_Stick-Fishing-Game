using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerGroundedStateSO : PlayerNormalStateSO
    {
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();
        }
        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();
        }
        public override void DoUpdateLogic()
        {
            base.DoUpdateLogic();

            if (StateMachine.ChangedStateAtFrame)
            {
                return;
            }
        }
    }
}
