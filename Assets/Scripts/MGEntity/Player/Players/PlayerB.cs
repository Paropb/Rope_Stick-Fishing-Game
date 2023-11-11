using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class PlayerB: Player
    {
        public override Vector2 HInput => InputHandler.HInputB;
    }
}
