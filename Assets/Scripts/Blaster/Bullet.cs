using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids;

namespace Blasters {
    public class Bullet : MonoBehaviour, IBullet {
        [SerializeField] private float bulletSpeed = 15f;
        [SerializeField] private float bulletAliveTime = 5f;

        public void Setup(Quaternion rotation) {
            transform.rotation = rotation;
            StartCoroutine(DisableGameObject(bulletAliveTime));
        }

        private void Update() {
            transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.transform.TryGetComponent(out Asteroid asteroid)) {
                asteroid.Split(false);
                gameObject.SetActive(false);
            }
        }

        private IEnumerator DisableGameObject(float timeToDisable) {
            yield return new WaitForSeconds(timeToDisable);
            gameObject.SetActive(false);
        }

        private void OnDisable() {
            StopAllCoroutines();
        }
    }
}

