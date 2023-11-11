using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MyGame.MGEntity;

namespace MyGame.CoreSystem
{
    public class CoreGetHit: CoreComponent
    {
        public Damageable Damageable { get; private set; }
        public KnockBackable KnockBackable { get; private set;}
        public bool CanGetHit { get; private set; }

        public MMF_Player FeedbackPlayer;
        public Collider2D GetHitCollider;

        public float InvincibleTime;
        protected float _invincibleTimer;
        private void Awake()
        {
            Damageable = GetComponent<Damageable>();
            KnockBackable = GetComponent<KnockBackable>();
        }
        public void Update()
        {
            if(_invincibleTimer > 0)
            {
                _invincibleTimer -= Time.deltaTime;
                CanGetHit = false;
            }
            else
            {
                CanGetHit=true;
            }
        }
        public void Gethit(CoreHit hit, AttackData attackData, Movement movement)
        {
            Gethit(hit);

            if (Damageable != null)
            {
                if(attackData.UseDamage)
                {
                    Damageable.Damage(attackData.DamageAmount);
                }
            }

            if (KnockBackable != null)
            {
                if(attackData.UseKnockBack)
                {
                    KnockBackable.KnockBack(attackData.KnockBackDir, attackData.KnockBackSpeed, attackData.KnockBackTime, movement);
                }
            }
        }
        public void Gethit(CoreHit hit)
        {
            _invincibleTimer = InvincibleTime;
            CanGetHit = false;
            FeedbackPlayer.PlayFeedbacks();
        }
    }
}
