using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyGame.CoreSystem
{
    public class KnockBackable: CoreComponent
    {
        [field: SerializeReference] public float KnockBackRecoverAccel { get; private set; }
	    public virtual void KnockBack(Vector2 knockBackDir, float knockBackSpeed, float knockBackTime, Movement movement)
        {
            Core.Entity.KnockBack(knockBackDir, knockBackSpeed, knockBackTime, movement);
        }
    }
}
