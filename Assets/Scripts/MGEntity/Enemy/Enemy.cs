using MyGame.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyGame.MGEntity
{
    public class Enemy : Entity
    {
        public EnemyStateMachine StateMachine { get; private set; }
        #region States
        public EnemyIdleState IdleState { get; private set; }
        public EnemyMoveState MoveState { get; private set; }
        public EnemyLongRangeState LongRangeState { get; private set; }
        public EnemyCloseRangeState CloseRangeState { get; private set; }
        public EnemyGetHitState GetHitState { get; private set; }
        #endregion
        #region StateSOs
        [Header("State SOs")]
        [SerializeField, InlineEditor] private EnemyIdleSOBase enemyIdleBase;
        [SerializeField, InlineEditor] private EnemyMoveSOBase enemyMoveBase;
        [SerializeField, InlineEditor] private EnemyLongRangeSOBase enemyLongRangeBase;
        [SerializeField, InlineEditor] private EnemyCloseRangeSOBase enemyCloseRangeBase;
        [SerializeField, InlineEditor] private EnemyGetHitSOBase enemyGetHitBase;

        public EnemyIdleSOBase enemyIdleInstance { get; private set; }
        public EnemyMoveSOBase enemyMoveInstance { get; private set; }
        public EnemyLongRangeSOBase enemyLongRangeInstance { get; private set; }
        public EnemyCloseRangeSOBase enemyCloseRangeInstance { get; private set; }
        public EnemyGetHitSOBase enemyGetHitInstance { get; private set; }
        #endregion
        #region Core
        public CoreComp<Movement> Movement { get; private set; }
        public CoreComp<CollisionSenses> CollisionSenses { get; private set; }
        public CoreComp<CoreHit> Hit { get; private set; }
        public CoreComp<KnockBackable> KnockBackable { get; private set; }
        #endregion
        protected override void Awake()
        {
            base.Awake();

            //CoreComp Awake
            Movement = new CoreComp<Movement>(Core);
            CollisionSenses = new CoreComp<CollisionSenses>(Core);
            Hit = new CoreComp<CoreHit>(Core);
            KnockBackable = new CoreComp<KnockBackable>(Core);

            enemyIdleInstance = Instantiate(enemyIdleBase);
            enemyMoveInstance = Instantiate(enemyMoveBase);
            enemyLongRangeInstance = Instantiate(enemyLongRangeBase);
            enemyCloseRangeInstance = Instantiate(enemyCloseRangeBase);
            enemyGetHitInstance = Instantiate(enemyGetHitBase);

            StateMachine = new EnemyStateMachine();

            IdleState = new EnemyIdleState(this);
            MoveState = new EnemyMoveState(this);
            LongRangeState = new EnemyLongRangeState(this);
            CloseRangeState = new EnemyCloseRangeState(this);
            GetHitState = new EnemyGetHitState(this);

        }
        protected override void Start()
        {
            base.Start();

            enemyIdleInstance.Initialize(this);
            enemyMoveInstance.Initialize(this);
            enemyLongRangeInstance.Initialize(this);
            enemyCloseRangeInstance.Initialize(this);
            enemyGetHitInstance.Initialize(this);

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
        public override void Damage()
        {
            base.Damage();

            StateMachine.ChangeState(GetHitState);
        }
        public override void KnockBack(Vector2 knockBackDir, float knockBackSpeed, float knockBackTime, Movement movement)
        {
            base.KnockBack(knockBackDir, knockBackSpeed, knockBackTime, movement);

            enemyGetHitInstance.KnockBack(knockBackDir, knockBackSpeed, knockBackTime, movement);
        }

    }
}
