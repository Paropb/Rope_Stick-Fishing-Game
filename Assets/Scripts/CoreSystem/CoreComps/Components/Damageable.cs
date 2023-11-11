using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyGame.CoreSystem
{
    public class Damageable : CoreComponent
    {
	    [field: SerializeReference] public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public override void MGAwake(Core core)
        {
            base.MGAwake(core);

            CurrentHealth = MaxHealth;
        }
        public virtual void Damage(float damageAmount)
        {
            CurrentHealth -= damageAmount;

            if(CurrentHealth <= 0)
            {
                Die();
            }
            else
            {
                Debug.Log("Core.Entity.Damage");
                Core.Entity.Damage();
            }
        }
        public virtual void Die()
        {
            Core.Entity.Die();
            Destroy(Core.Entity.gameObject);
        }
    }
}
