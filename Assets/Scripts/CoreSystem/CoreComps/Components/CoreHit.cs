using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;

namespace MyGame.CoreSystem
{
    [System.Serializable]
    public class AttackData
    {
        public bool UseDamage;

        [ShowIfGroup("UseDamage")]
        [BoxGroup("UseDamage/Damage Data")] public int DamageAmount;

        public bool UseKnockBack;
        [ShowIfGroup("UseKnockBack")]

        [BoxGroup("UseKnockBack/KnockBack Data")] public Vector2 KnockBackDir;
        [BoxGroup("UseKnockBack/KnockBack Data")] public float KnockBackSpeed;
        [BoxGroup("UseKnockBack/KnockBack Data")] public float KnockBackTime;
    }
    public class CoreHit : CoreComponent
    {
        public MMF_Player FeedbackPlayer;
        public MMF_HoldingPause FeedbackPlayer_HoldingPause;
        public MMF_FreezeFrame FeedbackPlayer_FreezeFrame;

        public bool ShowHitParticle;
        [ShowIf("ShowHitParticle")] public MMF_Player MMF_HitParticle;

        public bool ShowHitEffect;
        [ShowIf("ShowHitEffect")] public MMF_Player MMF_HitEffect;
        private void Awake()
        {
            FeedbackPlayer_HoldingPause = FeedbackPlayer.GetFeedbackOfType<MMF_HoldingPause>();
            FeedbackPlayer_FreezeFrame = FeedbackPlayer.GetFeedbackOfType<MMF_FreezeFrame>();

            //FeedbackPlayer.Events.OnComplete.AddListener(() => MMF_HitParticle.PlayFeedbacks());
        }
        public void Hit(CoreGetHit getHit)
        {
            if (getHit.CanGetHit)
            {
                FeedbackPlayer.PlayFeedbacks();

                if (ShowHitParticle)
                {
                    ShowEffect(getHit, MMF_HitParticle);
                }
                if (ShowHitEffect)
                {
                    ShowEffect(getHit, MMF_HitEffect);
                }

                getHit.Gethit(this);
            }
        }
        public void Hit(CoreGetHit getHit, AttackData attackData, Movement movement)
        {
            if (getHit.CanGetHit)
            {
                FeedbackPlayer.PlayFeedbacks();

                if (ShowHitParticle)
                {
                    ShowEffect(getHit, MMF_HitParticle);
                }
                if (ShowHitEffect)
                {
                    ShowEffect(getHit, MMF_HitEffect);
                }

                getHit.Gethit(this, attackData, movement);
            }
        }
        public void ShowEffect(CoreGetHit getHit, MMF_Player effectPlayer)
        {
            effectPlayer.transform.position = getHit.transform.position.ToVector2();
            int mult = getHit.transform.position.x < transform.position.x ? -1 : 1;
            effectPlayer.transform.localScale = new Vector3(effectPlayer.transform.localScale.x.Abs() * mult, effectPlayer.transform.localScale.y, effectPlayer.transform.localScale.z);
            effectPlayer.PlayFeedbacks();
        }
        

    }
}
