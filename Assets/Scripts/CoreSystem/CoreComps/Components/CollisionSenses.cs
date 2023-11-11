using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MyGame.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
        [Header("Movement Detections")]
        [SerializeField] protected ColliderSense GroundSense;

        [Header("Ray Senses")]
        [SerializeField] protected RaySense WallSense;
        [SerializeField] protected RaySense LedgeSense;

        [Header("Attack Detections")]
        [SerializeField] public ColliderSense AttackSense;

        [Header("Range Detections")]
        [SerializeField] protected ColliderSense CloseRange;
        [SerializeField] protected ColliderSense LongRange;

        ////Temp
        //protected Vector3 _blockSenseDir;
        //protected float _velocityY;
        //protected Vector3 _wallLookDir;

        #region Collider Senses
        public bool Grounded
        {
            get
            {
                return ColliderDetect(GroundSense);
            }
        }
        public bool CloseRangeDetected()
        {
            return ColliderDetect(CloseRange);
        }
        public bool LongRangeDetected()
        {
            return ColliderDetect(LongRange);
        }
        #endregion
        #region Ray Senses
        public bool WallInFront(Movement movement)
        {
            return RayDetect(WallSense, movement);
        }
        public bool LedgeInFront(Movement movement)
        {
            return !RayDetect(LedgeSense, movement);
        }
        #endregion
        #region Attack
        private Collider2D[] DetectedEnemies(Movement movement)
        {
            if (!AttackSense.Use)
            {
                Debug.LogError("CollisionSenses AttackDetection: No AttackSense In Use");
                return new Collider2D[0];
            }

            {
                switch (AttackSense.ColliderType)
                {
                    case ColliderTypes.Collider:
                        Vector2 attackOffset = AttackSense.Collider.offset;
                        return Physics2D.OverlapBoxAll(new Vector2(attackOffset.x * movement.FacingDirectionInt, attackOffset.y)
                    + (Vector2)AttackSense.Collider.transform.position, AttackSense.Collider.size, 0, AttackSense.DetectedLayers);
                    case ColliderTypes.Value:
                        return Physics2D.OverlapBoxAll(new Vector2(AttackSense.Offset.x * movement.FacingDirectionInt, AttackSense.Offset.y)
                    + (Vector2)AttackSense.Relative.position, AttackSense.Size, 0, AttackSense.DetectedLayers);
                }
                return new Collider2D[0];
            }
        }
        private Collider2D[] DetectedEnemies(int facingDirInt)
        {
            if (!AttackSense.Use)
            {
                Debug.LogError("CollisionSenses AttackDetection: No AttackSense In Use");
                return new Collider2D[0];
            }

            {
                switch (AttackSense.ColliderType)
                {
                    case ColliderTypes.Collider:
                        Vector2 attackOffset = AttackSense.Collider.offset;
                        return Physics2D.OverlapBoxAll(new Vector2(attackOffset.x * facingDirInt, attackOffset.y)
                    + (Vector2)AttackSense.Collider.transform.position, AttackSense.Collider.size, 0, AttackSense.DetectedLayers);
                    case ColliderTypes.Value:
                        return Physics2D.OverlapBoxAll(new Vector2(AttackSense.Offset.x * facingDirInt, AttackSense.Offset.y)
                    + (Vector2)AttackSense.Relative.position, AttackSense.Size, 0, AttackSense.DetectedLayers);
                }
                return new Collider2D[0];
            }
        }
        public void AttackDetection(Movement movement, CoreHit coreHit, out Collider2D[] detectedEnemies)
        {
            detectedEnemies = new Collider2D[0];

            if (!AttackSense.Use)
            {
                Debug.LogError("CollisionSenses AttackDetection: No AttackSense In Use");
            }

            switch(AttackSense.ColliderType)
            {
                case ColliderTypes.Collider:
                    if(!AttackSense.Collider.enabled)
                    {
                        return;
                    }
                    break;
            }

            detectedEnemies = DetectedEnemies(movement);
            AttackDetectionLogic(coreHit, detectedEnemies);
        }
        public void AttackDetection(Movement movement, CoreHit coreHit, out Collider2D[] detectedEnemies, AttackData attackData)
        {
            detectedEnemies = new Collider2D[0];

            if (!AttackSense.Use)
            {
                Debug.LogError("CollisionSenses AttackDetection: No AttackSense In Use");
            }

            switch (AttackSense.ColliderType)
            {
                case ColliderTypes.Collider:
                    if (!AttackSense.Collider.enabled)
                    {
                        return;
                    }
                    break;
            }

            detectedEnemies = DetectedEnemies(movement);
            AttackDetectionLogic(coreHit, detectedEnemies, attackData, movement);
        }
        public void AttackDetection(int facingDirInt, CoreHit coreHit, out Collider2D[] detectedEnemies)
        {
            detectedEnemies = new Collider2D[0];

            if (!AttackSense.Use)
            {
                Debug.LogError("CollisionSenses AttackDetection: No AttackSense In Use");
            }

            switch (AttackSense.ColliderType)
            {
                case ColliderTypes.Collider:
                    if (!AttackSense.Collider.enabled)
                    {
                        return;
                    }
                    break;
            }

            detectedEnemies = DetectedEnemies(facingDirInt);
            AttackDetectionLogic(coreHit, detectedEnemies);
        }
        private void AttackDetectionLogic(CoreHit coreHit, Collider2D[] detectedEnemies)
        {
            foreach (var enemy in detectedEnemies)
            {
                CoreGetHit getHit = enemy.GetComponent<CoreGetHit>();
                if (getHit)
                {
                    switch (AttackSense.ColliderType)
                    {
                        case ColliderTypes.Collider:
                            if (AttackSense.Collider.enabled)
                            {
                                coreHit.Hit(getHit);
                            }
                            break;
                        case ColliderTypes.Value:
                            coreHit.Hit(getHit);
                            break;
                    }
                }

            }
        }
        private void AttackDetectionLogic(CoreHit coreHit, Collider2D[] detectedEnemies, AttackData attackData, Movement movement)
        {
            foreach (var enemy in detectedEnemies)
            {
                CoreGetHit getHit = enemy.GetComponent<CoreGetHit>();
                if (getHit)
                {
                    switch (AttackSense.ColliderType)
                    {
                        case ColliderTypes.Collider:
                            if (AttackSense.Collider.enabled)
                            {
                                coreHit.Hit(getHit, attackData, movement);
                            }
                            break;
                        case ColliderTypes.Value:
                            coreHit.Hit(getHit, attackData, movement);
                            break;
                    }
                }

            }
        }

        #endregion
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;

            if (GroundSense.Use)
            {
                //Gizmos.DrawCube(GroundSense.Collider.offset + (Vector2)GroundSense.Collider.transform.position, GroundSense.Collider.size);
            }
        }
        private bool ColliderDetect(ColliderSense sense)
        {
            if (!sense.Use)
            {
                return false;
            }

            switch (sense.ColliderType)
            {
                case ColliderTypes.Collider:
                    if (Physics2D.OverlapBoxAll(sense.Collider.offset + (Vector2)sense.Collider.transform.position, sense.Collider.size, 0, sense.DetectedLayers).Length > 0)
                    {
                        return true;
                    }
                    break;
                case ColliderTypes.Value:
                    if (Physics2D.OverlapBoxAll(sense.Offset + (Vector2)sense.Relative.position, sense.Size, 0, sense.DetectedLayers).Length > 0)
                    {
                        return true;
                    }
                    break;
            }



            return false;
        }
        private bool RayDetect(RaySense sense, Movement movement)
        {
            if (!sense.Use)
                return false;

            return (Physics2D.Raycast(sense.Origin.transform.position, 
                sense.DetectDirection.x * movement.FacingDirectionInt * Vector2.right + sense.DetectDirection.y * Vector2.up,
                sense.DetectDistance, sense.DetectedLayers));
        }
    }
    public enum ColliderTypes { Collider, Value }
    [System.Serializable]
    public class ColliderSense
    {
        public bool Use;

        [ShowIfGroup("Use")]
        [BoxGroup("Use")] public ColliderTypes ColliderType;
        [BoxGroup("Use/Datas"), ShowIf("ColliderType", Value = ColliderTypes.Collider)] public BoxCollider2D Collider;
        [BoxGroup("Use/Datas"), ShowIf("ColliderType", Value = ColliderTypes.Value)] public Vector2 Offset;
        [BoxGroup("Use/Datas"), ShowIf("ColliderType", Value = ColliderTypes.Value)] public Vector2 Size;
        [BoxGroup("Use/Datas"), ShowIf("ColliderType", Value = ColliderTypes.Value)] public Transform Relative;
        [BoxGroup("Use/Datas")] public LayerMask DetectedLayers;
    }
    [System.Serializable]
    public class RaySense
    {
        public bool Use;

        [ShowIfGroup("Use")]
        
        [BoxGroup("Use/Datas")] public Transform Origin;
        [BoxGroup("Use/Datas")] public float DetectDistance;
        [BoxGroup("Use/Datas")] public Vector2 DetectDirection;
        [BoxGroup("Use/Datas")] public LayerMask DetectedLayers;
        [HideInInspector] public RaycastHit HitInfo;
    }

}
