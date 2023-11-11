using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class StateMachine<TState> where TState : State
    {
        protected bool _changedStateAtFrame;
        public bool ChangedStateAtFrame { get => _changedStateAtFrame;}
        protected TState _currentStateAtFrame;
        public TState CurrentState { get; protected set; }
        public TState PreviousState { get; protected set; }
        public virtual void Initialize(TState startingState)
        {
            CurrentState = startingState;
            _currentStateAtFrame = CurrentState;
            PreviousState = CurrentState;
            _changedStateAtFrame = true;
            CurrentState.Enter();
        }
        public virtual void ChangeState(TState newState)
        { 
            _changedStateAtFrame = true;

            PreviousState = CurrentState;
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();

        }
        public virtual void MGUpdate()
        {
            _currentStateAtFrame = CurrentState;
            CurrentState.MGUpdate();

            _changedStateAtFrame = false;
        }
        public virtual void MGFixedUpdate()
        {
            CurrentState.MGFixedUpdate();
        }
    }
}
