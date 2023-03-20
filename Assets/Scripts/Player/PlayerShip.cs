using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asteroids;
using Blasters;
using PowerUps;

namespace Player {
    public class PlayerShip : MonoBehaviour {
        [SerializeField] private PlayerHealth health;
        [SerializeField] private Blaster blaster;
        [SerializeField] private PlayerInput inputs;

        public PlayerHealth GetHealthScript() => health;
        public Blaster GetBlasterScript() => blaster;
        public PlayerInput GetPlayerInput() => inputs;
        public bool GetIsDead() => _isDead;

        private bool _isDead = false;

        public void Death() {
            _isDead = true;
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (_isDead) {
                return;
            }

            if (collision.transform.TryGetComponent(out Asteroid asteroid)) {
                if (health.TakeDamage()) {
                    asteroid.Split(true);
                }
            }

            if (collision.transform.TryGetComponent(out PickablePowerUp powerup)) {
                powerup.ActivatePowerUp(this);
            }
        }
    }
}

