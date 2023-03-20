using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pooling {
    [Serializable]
    public class PoolClass {
        public PoolTypes Type;
        public int Amount;
        public GameObject Prefab;
        public Transform TransformParent;
        public List<GameObject> Pool => pool;

        private List<GameObject> pool = new List<GameObject>();
    }
}

