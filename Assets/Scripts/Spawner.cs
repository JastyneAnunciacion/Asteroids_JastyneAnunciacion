using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

namespace Spawners {
    public class Spawner : MonoBehaviour {
        [Header("Spawner Variables")]
        [SerializeField] private float spawnAmount;
        [SerializeField] private bool spawnOnlyOnEdges;

        [Header("Spawn Rate Over Time")]
        [SerializeField] private Vector2 randomSpawnTimeRange;
        [SerializeField] private bool changeSpawnRateOverTime;
        [SerializeField] private bool changesIsPositiveIncrements;
        [SerializeField] private float incrementAmount;

        [Header("Object To Spawn")]
        [SerializeField] protected PoolTypes[] objectsToSpawn;

        private float _timeToSpawn;
        private Camera _camera;
        private bool _hasRandomTimeRange = false;
        private float _randomTimeToSpawn;

        protected virtual void SpawnObject() {
            var pooledObject = PoolManager.instance.GetPoolObject(GetPoolType());
            pooledObject.transform.position = RandomSpawn();
            pooledObject.SetActive(true);
        }

        protected PoolTypes GetPoolType() {
            if (objectsToSpawn.Length > 1) {
                return objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            }
            return objectsToSpawn[0];
        }

        protected Vector2 RandomSpawn() {
            if (spawnOnlyOnEdges) {
                var randPos = Random.Range(0f, 1f);
                var randAxisIsX = Random.Range(0, 2) == 0 ? true : false;
                return randAxisIsX ? _camera.ViewportToWorldPoint(new Vector2(randPos, 0)) : _camera.ViewportToWorldPoint(new Vector2(0, randPos));
            }
            else {
                return _camera.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
            }
        }

        private void Start() {
            _camera = Camera.main;
        }

        private void Update() {
            if (!GameManager.instance.GameStarted) {
                return;
            }

            if (!_hasRandomTimeRange) {
                _randomTimeToSpawn = Random.Range(randomSpawnTimeRange.x, randomSpawnTimeRange.y);
                _hasRandomTimeRange = true;
            }
            else {
                var spawnTime = Mathf.Min(_timeToSpawn += Time.deltaTime, _randomTimeToSpawn);
                if (spawnTime == _randomTimeToSpawn) {
                    for (int i = 0; i < spawnAmount; i++) {
                        SpawnObject();
                    }
                    AdjustSpawnTime();
                    _timeToSpawn = 0;
                    _hasRandomTimeRange = false;
                }
            }
        }

        private void AdjustSpawnTime() {
            if (!changeSpawnRateOverTime) {
                return;
            }

            if (changesIsPositiveIncrements) {
                randomSpawnTimeRange.x += incrementAmount;
                randomSpawnTimeRange.y += incrementAmount;
            }
            else {
                randomSpawnTimeRange.x -= incrementAmount;
                randomSpawnTimeRange.y -= incrementAmount;
            }
        }
    }

}

