using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.MGEntity
{
    public class State
    {
        protected float _startTime;
        public bool IsExistingState { get; set; }

        public State()
        {

        }
        public virtual void Enter()
        {
            Debug.Log($"# {this.GetType().Name} # " + $"Enter State {this.GetType().Name.Split("State")[0]}");

            DoChangeStateChecks();
            _startTime = Time.time;

            IsExistingState = false;

        }
        public virtual void Exit()
        {
            IsExistingState = true;
        }
        public virtual void MGUpdate()
        {
            DoChangeStateChecks();
        }
        public virtual void MGFixedUpdate()
        {

        }
        public virtual void DoChangeStateChecks()
        {

        }
    }
    //public class StateIndicator<TFatherState, TStateMachine> where TFatherState : DCSystem.State where TStateMachine : 
    //{
    //    protected TStateMachine _stateMachine;
    //    public bool IsState
    //    {
    //        get
    //        {
    //            if (_stateMachine.CurrentState.GetType().IsSubclassOf(typeof(TFatherState)))
    //            {
    //                return true;
    //            }
    //            else if (_stateMachine.CurrentState.GetType() == typeof(TFatherState))
    //            {
    //                return true;
    //            }
    //            return false;
    //        }
    //    }
    //    public StateIndicator(TStateMachine stateMachine)
    //    {
    //        _stateMachine = stateMachine;
    //    }
    //}
}
