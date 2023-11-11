using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerNormalStateSO : PlayerStateSOBase
    {

        public override void Initialize(Player player)
        {
            base.Initialize(player);
        }
        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            base.DoFixedUpdateLogic();

            Movement.SimulateVelocityX(Player.HInput.x, Data.RunSpeed, Data.RunAccel, Data.RunDec);
            Player.MoveRotation((int)Player.HInput.x);
            Movement.CheckIfSHouldFlip(Player.transform, (int)Player.HInput.x, Constants.FAST);
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
