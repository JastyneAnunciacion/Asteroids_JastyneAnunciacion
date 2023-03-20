using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids;
using Player;

namespace PowerUps {
    public class BarrierObject : MonoBehaviour {
        private PlayerShip _player;
        public void Setup(float duration, PlayerShip player) {
            _player = player;
            if (duration != 0) {
                Destroy(gameObject, duration);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.transform.TryGetComponent(out Asteroid asteroid)) {
                asteroid.Split(true);
                _player.GetHealthScript().SetInvinciblity(false, true);
                Destroy(gameObject);
            }
        }

        private void OnDestroy() {
            _player.GetHealthScript().SetInvinciblity(false);
        }
    }
}

