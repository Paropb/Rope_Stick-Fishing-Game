using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyGame.MGSystem;
using System;

namespace MyGame.MGEntity
{
    public class InputHandler : Singleton<InputHandler>
    {
        [field: SerializeReference] public InputActionAsset PlayerInputActionAsset { get; private set; }
        public PlayerInput PlayerInput { get; private set; }
        public Vector2 HInputA { get; private set; }
        public Vector2 HInputB { get; private set; }
        public MGInput R = new MGInput();

        protected override void Awake()
        {
            base.Awake();
        }
        protected virtual void Update()
        {
            MGInputCommand.Command.MGUpdate();
        }
        protected virtual void LateUpdate()
        {
            MGInputCommand.Command.MGLateUpdate();
        }
        #region GamePlay
        public void OnHInputA(InputAction.CallbackContext ctx)
        {
            HInputA = ctx.ReadValue<Vector2>();
        }
        public void OnHInputB(InputAction.CallbackContext ctx)
        {
            HInputB = ctx.ReadValue<Vector2>();
        }
        public void OnRInput(InputAction.CallbackContext ctx)
        {
            R.OnMGInput(ctx);
        }
        #endregion
    }
    public class MGInputCommand
    {
        public static MGInputCommand Command = new MGInputCommand();
        public Action OnUpdate;
        public Action OnLateUpdate;
        public void MGUpdate() => OnUpdate?.Invoke();
        public void MGLateUpdate() => OnLateUpdate?.Invoke();

    }
    public class MGInput
    {
        protected float _inputLastTime;
        public MGInput(float inputLastTime = Constants.FAST)
        {
            this._inputLastTime = inputLastTime;
            MGInputCommand.Command.OnUpdate += MGUpdate;
            MGInputCommand.Command.OnLateUpdate += MGLateUpdate;
        }
        protected virtual void MGUpdate()
        {
            
        }
        protected virtual void MGLateUpdate()
        {
            if(Started)
            {
                Started = false;
            }
            if(Performed)
            {
                Performed = false;
            }
            if(Canceled)
            {
                Canceled = false;
            }

            if (_startedStartedTime + _inputLastTime < Time.time)
            {
                _startedLast = false;
            }

            if (_startedLast)
            {
                if(_usedStartedLast)
                {
                    _startedLast = false;
                    _usedStartedLast = false;
                }
            }
        }
        protected bool _started;
        protected float _startedStartedTime;
        public bool Started
        {
            get
            {
                return _started;
            }
            private set
            {
                if (value)
                {
                    _startedStartedTime = Time.time;
                }
                _started = value;
            }
        }
        protected bool _startedLast;
        protected bool _usedStartedLast;
        public bool StartedLast
        {
            get
            {
                if(_startedLast)
                {
                    _usedStartedLast = true;
                    return true;
                }
                return false;
            }
            private set => _startedLast = value;
        }
        protected bool _performed;
        protected float _performedStartedTime;
        public bool Performed
        {
            get
            {
                return _performed;
            }
            private set
            {
                if (value)
                {
                    _performedStartedTime = Time.time;
                }
                _performed = value;
            }
        }
        protected bool _canceled;
        protected float _canceledStartedTime;
        public bool Canceled
        {
            get
            {
                return _canceled;
            }
            private set
            {
                if (value)
                {
                    _canceledStartedTime = Time.time;
                }
                _canceled = value;
            }
        }
        public virtual void OnMGInput(InputAction.CallbackContext context)
        {

            if (context.started)
            {
                Started = true;
                _startedLast = true;
                _usedStartedLast = false;
            }
            if (context.performed)
            {
                Performed = true;
            }
            if (context.canceled)
            {
                Canceled = true;
            }

        }
        protected void UseInput(ref bool input)
        {
            input = false;
        }
    }
}
