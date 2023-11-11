using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.MGEntity;
using Sirenix.OdinInspector;

namespace MyGame.MGSystem
{
    public class FishManager : Singleton<FishManager>
    {
        [SerializeField] protected Rect LFishSpawnRange;
        [SerializeField] protected Rect RFishSpawnRange;

        [BoxGroup("Fish Data"), SerializeField] protected Enemy RightSlowMovingFish;
        [BoxGroup("Fish Data"), SerializeField] protected Enemy LeftSlowMovingFish;
        [BoxGroup("Fish Data"), SerializeField] protected Enemy RightFastMovingFish;
        [BoxGroup("Fish Data"), SerializeField] protected Enemy LeftFastMovingFish;

        [BoxGroup("Spawn Data"), SerializeField] protected float SlowMoveFishSpawnMinInterval;
        [BoxGroup("Spawn Data"), SerializeField] protected float SlowMoveFishSpawnMaxInterval;
        [BoxGroup("Spawn Data"), SerializeField] protected float FastMoveFishSpawnMinInterval;
        [BoxGroup("Spawn Data"), SerializeField] protected float FastMoveFishSpawnMaxInterval;

        protected Coroutine _lsmSpawnRoutine;
        protected Coroutine _rsmSpawnRoutine;
        protected Coroutine _lfmSpawnRoutine;
        protected Coroutine _rfmSpawnRoutine;

        private void Start()
        {
            Debug.Log($"LFishSpawnRange.center: {LFishSpawnRange.center} + LFishSpawnRange.min: {LFishSpawnRange.min}");
            _lsmSpawnRoutine = StartCoroutine(SpawnRoutine(SlowMoveFishSpawnMinInterval, SlowMoveFishSpawnMaxInterval, LeftSlowMovingFish, LFishSpawnRange));
            _lfmSpawnRoutine = StartCoroutine(SpawnRoutine(FastMoveFishSpawnMinInterval, FastMoveFishSpawnMaxInterval, LeftFastMovingFish, LFishSpawnRange));
            _rsmSpawnRoutine = StartCoroutine(SpawnRoutine(SlowMoveFishSpawnMinInterval, SlowMoveFishSpawnMaxInterval, RightSlowMovingFish, RFishSpawnRange));
            _rfmSpawnRoutine = StartCoroutine(SpawnRoutine(FastMoveFishSpawnMinInterval, FastMoveFishSpawnMaxInterval, RightFastMovingFish, RFishSpawnRange));
        }

        IEnumerator SpawnRoutine(float spawnDurationMin, float spawnDurationMax, Enemy fish, Rect spawnRange)
        {
            while(Application.isPlaying)
            {
                yield return new WaitForSeconds(Random.Range(spawnDurationMin, spawnDurationMax));

                Enemy currentEnemy = Instantiate(fish, new Vector2(
                    Random.Range(spawnRange.min.x, spawnRange.max.x),
                    Random.Range(spawnRange.min.y, spawnRange.max.y)),
                    Quaternion.identity);
                Destroy(currentEnemy.gameObject, 10f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube(LFishSpawnRange.center, LFishSpawnRange.size);

            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(RFishSpawnRange.center, RFishSpawnRange.size);
        }

    }
}
