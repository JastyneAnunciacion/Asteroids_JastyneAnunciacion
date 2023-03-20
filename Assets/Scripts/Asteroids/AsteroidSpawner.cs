using Pooling;
using Asteroids;

namespace Spawners {
    public class AsteroidSpawner : Spawner {
        protected override void SpawnObject() {
            var pooledAsteroid = PoolManager.instance.GetPoolObject(GetPoolType()).GetComponent<Asteroid>();
            pooledAsteroid.Setup(1);
            pooledAsteroid.transform.position = RandomSpawn();
            pooledAsteroid.gameObject.SetActive(true);
        }
    }
}

