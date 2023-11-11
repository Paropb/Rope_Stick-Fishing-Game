using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerA : Player
    {
        public override Vector2 HInput => InputHandler.HInputA;
    }
}
