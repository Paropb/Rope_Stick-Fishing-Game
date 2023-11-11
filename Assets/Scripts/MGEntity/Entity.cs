using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.CoreSystem;

namespace MyGame.MGEntity
{
    public class Entity : MonoBehaviour
    {
        public Core Core { get; protected set; }
        public GameObject Base { get; protected set; }
        public Rigidbody2D RB { get; protected set; }
        public SpriteRenderer SR { get; protected set; }

        public virtual void MGAwake() { }
        protected virtual void Awake()
        {
            Core = GetComponentInChildren<Core>();

            Core.MGAwake(this);

            Base = transform.GetChild(0).transform.gameObject;
            RB = GetComponent<Rigidbody2D>();
            SR = transform.Find("SR").GetComponent<SpriteRenderer>();
        }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void Start() { }
        protected virtual void Update() { Core.MGUpdate(); }
        protected virtual void FixedUpdate() { Core.MGFixedUpdate(); }
        public virtual void Damage() { }
        public virtual void Die() { }
        public virtual void KnockBack(Vector2 knockBackDir, float knockBackSpeed, float knockBackTime, Movement movement) { }
    }
}
