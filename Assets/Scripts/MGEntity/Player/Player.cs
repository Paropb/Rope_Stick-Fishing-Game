using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.CoreSystem;
using Sirenix.OdinInspector;
using System.Reflection;
using DG.Tweening;

namespace MyGame.MGEntity
{
    public class Player : Entity
    {
        [Header("Data"), Space(10), InlineEditor]
        public PlayerDataSO Data;
        public Animator Animator { get; protected set; }
        #region States
        public PlayerIdleState IdleState { get; private set; }
        public PlayerGroundMoveState GroundMoveState { get; private set; }
        #endregion
        #region StateSOs
        [SerializeField, BoxGroup("Normal State SO")] private PlayerIdleStateSO Idle;
        [SerializeField, BoxGroup("Normal State SO")] private PlayerGroundMoveStateSO GroundMove;
        
        public PlayerIdleStateSO IdleInstance { get; private set; }
        public PlayerGroundMoveStateSO GroundMoveInstance { get; private set; }
        #endregion
        #region Core
        public CoreComp<Movement> Movement { get; private set; }
        public CoreComp<CollisionSenses> CollisionSenses { get; private set; }
        #endregion
        public PlayerStateMachine StateMachine { get; private set; }
        public InputHandler InputHandler { get; private set; }
        public virtual Vector2 HInput { get; }
        protected override void Awake()
        {
            base.Awake();

            MGSystem.GameManager.Instance.SetPlayer(this);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Components
            Animator = SR.transform.GetComponent<Animator>();

            //CoreComp Awake
            Movement = new CoreComp<Movement>(Core);
            CollisionSenses = new CoreComp<CollisionSenses>(Core);
            

            StateMachine = new PlayerStateMachine();

            InputHandler = GameObject.Find("InputHandler").GetComponent<InputHandler>();

            //States
            IdleState = new PlayerIdleState(this);
            GroundMoveState = new PlayerGroundMoveState(this);
            
            IdleInstance = Instantiate(Idle);
            GroundMoveInstance = Instantiate(GroundMove);
        }
        protected override void Start()
        {
            base.Start();

            IdleInstance.Initialize(this);
            GroundMoveInstance.Initialize(this);

            StateMachine.Initialize(IdleState);
        }
        protected override void Update()
        {
            base.Update();

            StateMachine.MGUpdate();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            StateMachine.MGFixedUpdate();

            Movement.Comp.UpdatePosition(RB, CollisionSenses.Comp);
        }
        #region Other Functions
        public void Scale(float x, float y, float time)
        {
            DOTween.Kill("ScaleSequence");
            SR.transform.DOScale(new Vector3(x, y, 1f), time).SetId("PlayerScale");
        }
        public void ScaleSequence(Vector3 first, Vector3 second, float time1, float time2)
        {
            //DOTween.Kill("ScaleSequence");
            Sequence scaleSequence = DOTween.Sequence();

            scaleSequence.SetId("ScaleSequence");
            scaleSequence.Append(SR.transform.DOScale(first, time1));
            scaleSequence.Append(SR.transform.DOScale(second, time2));
        }
        public override void Die()
        {
            base.Die();
        }
        public void MoveRotation(int xInput)
        {
            if(xInput * SR.transform.rotation.z >= 0)
            {
                DOTween.To((value) => SR.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, value), transform.rotation.z, Data.MoveRotationDegree * -Movement.Comp.FacingDirectionInt * xInput.Abs(), Data.RotationDuration).SetId<Tweener>("RotationTween");
            }
            else
            {
                //DOTween.Kill("RotationTween");
            }
        }
        #endregion
    }
    
}
