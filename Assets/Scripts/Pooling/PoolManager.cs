using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blasters;

namespace Pooling {
    public class PoolManager : Singleton<PoolManager> {

        [SerializeField] private List<PoolClass> poolClasses;

        public GameObject GetPoolObject(PoolTypes poolType) {
            var poolClass = GetPoolByType(poolType);
            var pool = poolClass.Pool;
            for (int i = 0; i < pool.Count; i++) {
                if (!pool[i].activeInHierarchy) {
                    return pool[i];
                }
            }
            var obj = Instantiate(poolClass.Prefab, poolClass.TransformParent);
            pool.Add(obj);
            return obj;
        }

        private void Start() {
            for (int i = 0; i < poolClasses.Count; i++) {
                FillPool(poolClasses[i]);
            }
        }

        private void FillPool(PoolClass poolClass) {
            for (int i = 0; i < poolClass.Amount; i++) {
                var obj = Instantiate(poolClass.Prefab, poolClass.TransformParent);
                obj.gameObject.SetActive(false);
                poolClass.Pool.Add(obj);
            }
        }

        private PoolClass GetPoolByType(PoolTypes poolType) {
            for (int i = 0; i < poolClasses.Count; i++) {
                if (poolClasses[i].Type == poolType) {
                    return poolClasses[i];
                }
            }
            return null;
        }
    }
}

