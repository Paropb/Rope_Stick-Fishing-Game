using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.CoreSystem;

namespace MyGame.MGEntity
{
    public class MGBullet: MonoBehaviour
    {
        [SerializeField] private float LifeTime;

        protected Core Core;

        public CoreComp<CollisionSenses> CollisionSenses;
        public CoreComp<Movement> Movement;
        public CoreComp<CoreHit> Hit;

        protected Collider2D[] _detectedEnemies;
        protected float _lifeTimer;
        private void Awake()
        {
            Core = GetComponentInChildren<Core>();

            CollisionSenses = new CoreComp<CollisionSenses>(Core);
            Movement = new CoreComp<Movement>(Core);
            Hit = new CoreComp<CoreHit>(Core);

            _lifeTimer = LifeTime;
        }
        private void FixedUpdate()
        {
            CollisionSenses.Comp.AttackDetection(Movement.Comp.FacingDirectionInt, Hit.Comp, out _detectedEnemies);

            if(_detectedEnemies.Length > 0)
            {
                Destroy(gameObject);
            }

            if(CollisionSenses.Comp.Grounded)
            {
                Destroy(gameObject);
            }
            Movement.Comp.UpdatePosition(transform);
        }
        
        private void Update()
        {
            if(_lifeTimer < 0)
            {
                Destroy(gameObject);
            }
            else
            {
                _lifeTimer -= Time.deltaTime;
            }
        }
    }
}
