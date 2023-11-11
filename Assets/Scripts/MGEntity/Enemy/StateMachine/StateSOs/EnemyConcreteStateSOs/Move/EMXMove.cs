using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    [CreateAssetMenu(menuName = ("Enemy Logic/Move SO/X Move"), fileName = ("Move - X Move"))]
    public class EMXMove : EnemyMoveSOBase
    {
        [SerializeField] protected CoreSystem.FacingDirections FacingDirection;

        public override void DoFixedUpdateLogic()
        {
            base.DoFixedUpdateLogic();

            Movement.SetVelocityX(MoveSpeed * (int)FacingDirection);
        }
    }
}
