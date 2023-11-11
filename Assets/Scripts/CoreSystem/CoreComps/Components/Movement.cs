using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace MyGame.CoreSystem
{
    public enum FacingDirections
    {
        Left = -1,
        Right = 1
    }
    public class Movement : CoreComponent
    {
        [BoxGroup("Data")] public float Gravity = 50f;
        [BoxGroup("Data")] public float MaxFall = -20f;
        public Vector2 Velocity { get => _velocity; }
        protected Vector2 _velocity;

        public bool Canflip { get; private set; }
        public FacingDirections FacingDirection { get; private set; }
        public FacingDirections OppositeDirection { get => (FacingDirections)(-FacingDirectionInt); }
        public int FacingDirectionInt { get => (int)FacingDirection; }
        public int OppositeDirectionInt { get => -FacingDirectionInt; }
        public override void MGAwake(Core core)
        {
            base.MGAwake(core);

            Canflip = true;
            FacingDirection = FacingDirections.Right;
        }
        public override void MGUpdate()
        {
            base.MGUpdate();

            Core.Entity.RB.velocity = Vector3.zero;
        }
        #region Set Functions
        public void SetVelocityZero() => _velocity = Vector3.zero;
        public void SetVelocity(float xValue, float yValue)
        {
            _velocity.Set(xValue, yValue);
        }
        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }
        public void SetVelocity(Vector2 dir, float speed)
        {
            _velocity = dir.normalized * speed;
        }
        public void SetVelocityMulti(float multi)
        {
            _velocity = _velocity * multi;
        }
        public void SetVelocityX(float xValue) => _velocity.Set(xValue, Velocity.y);
        public void SetVelocityY(float yValue) => _velocity.Set(Velocity.x, yValue);
        #endregion
        #region Simulate Functions
        public void SimulateVelocityX(float inputX, float speed, float acceleration, float decceleration)
        {
            if (inputX != 0 && inputX * _velocity.x < 0)
            {
                SetVelocityX(Mathf.MoveTowards(_velocity.x, inputX * speed, decceleration * Time.deltaTime));
            }
            else if (inputX != 0)
            {
                SetVelocityX(Mathf.MoveTowards(_velocity.x, inputX * speed, acceleration * Time.deltaTime));
            }
            else
            {
                SetVelocityX(Mathf.MoveTowards(_velocity.x, 0f, decceleration * Time.deltaTime));
            }
        }
        public void SimulateVelocityX(float finalVelocityX, float acceleration)
        {
            SetVelocityX(Mathf.MoveTowards(_velocity.x, finalVelocityX, acceleration * Time.deltaTime));
        }
        public void SimulateVelocityY(float inputY, float speed, float acceleration, float decceleration)
        {
            SetVelocityY(Mathf.MoveTowards(_velocity.y, 0f, decceleration * Time.deltaTime));
        }
        public void SimulateVelocityY(float finalVelocityY, float acceleration)
        {
            SetVelocityY(Mathf.MoveTowards(_velocity.y, finalVelocityY, acceleration * Time.deltaTime));
        }
        public void SimulateVelocity(float input, Vector3 velocity, float acceleration, float decceleration)
        {
            if (input != 0)
            {
                SetVelocity(Vector3.MoveTowards(_velocity, velocity, acceleration * Time.deltaTime));
            }
            else
            {
                SetVelocity(Vector3.MoveTowards(_velocity, Vector3.zero, decceleration * Time.deltaTime));
            }
        }
        public void SimulateVelocity(Vector3 velocity, float acceleration, float decceleration)
        {
            SetVelocity(Vector3.MoveTowards(_velocity, velocity, acceleration * Time.deltaTime));
        }
        public void SimulateGravity()
        {
            SetVelocityY(Mathf.MoveTowards(_velocity.y, MaxFall, Gravity * Time.deltaTime));
        }
        #endregion
        #region Flip Functions
        public void Flip(Transform toFlip, float flipTime, FacingDirections facingDirection)
        {
            if (!Canflip)
                return;

            FacingDirection = facingDirection;

            toFlip.DOScale(new Vector3(toFlip.localScale.x.Abs() * FacingDirectionInt, toFlip.localScale.y, toFlip.localScale.z), flipTime)
                .SetId(toFlip.gameObject.name + "flip tween")
                .onComplete += () => Canflip = true;

            Canflip = false;
        }
        public void Flip(Transform toFlip, float flipTime)
        {
            if (!Canflip)
                return;

            FacingDirection = OppositeDirection;

            toFlip.DOScale(new Vector3(toFlip.localScale.x.Abs() * FacingDirectionInt, toFlip.localScale.y, toFlip.localScale.z), flipTime)
                .SetId(toFlip.gameObject.name + "flip tween")
                .onComplete += () => Canflip = true;

            Canflip = false;
        }
        public bool CheckIfSHouldFlip(Transform toFlip, int xInput, float flipTime)
        {
            if (xInput != 0 && xInput != (FacingDirectionInt))
            {
                Flip(toFlip, flipTime);
                return true;
            }
            return false;
        }
        public void FlipTo(Transform toFLip, Transform flipTo, float flipTime)
        {
            if(toFLip.position.x < flipTo.position.x)
            {
                Flip(toFLip, flipTime, FacingDirections.Right);
            }
            else
            {
                Flip(toFLip, flipTime, FacingDirections.Left);
            }
        }
        public Vector2 Directionize(Vector2 vector2)
        {
            return new Vector2(vector2.x.Abs() * FacingDirectionInt, vector2.y);
        }
        #endregion
        public void UpdatePosition(Rigidbody2D RB, CollisionSenses collisionSenses)
        {
            RB.MovePosition(_velocity * Time.deltaTime + (Vector2) RB.transform.position);
        }
        public void UpdatePosition(Transform transform)
        {
            transform.position += (Vector3)_velocity * Time.deltaTime;
        }
    }
}
