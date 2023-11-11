using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerDeathStateSO : PlayerStateSOBase
    {
        public override void DoEnterLogic()
        {
            base.DoEnterLogic();

            Movement.SetVelocityZero();
        }
    }
}
