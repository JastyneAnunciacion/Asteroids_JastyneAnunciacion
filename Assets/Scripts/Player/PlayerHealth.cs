using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Player;

namespace Player {
    public class PlayerHealth : MonoBehaviour {
        [SerializeField] private PlayerShip player;

        [Header("Health Variables")]
        [SerializeField] private int health = 3;
        [SerializeField] private GameObject lifeSpritePrefab;
        [SerializeField] private GameObject lifeSpriteParent;
        [SerializeField] private float takeDamageCoolDown;

        [Header("Death Variables")]
        [SerializeField] private float deathBloatSize;
        [SerializeField] private float bloatTime;
        [SerializeField] private Ease bloatEasing;

        [Header("Events")]
        [SerializeField] private UnityEvent onTakeDamage;
        [SerializeField] private UnityEvent onDeath;

        private bool _isInvincible;
        private bool _hasRecentlyBeenDamaged = false;
        private float _timeForCD;

        public void AddHealth(int amount) {
            health += amount;
            for (int i = lifeSpriteParent.transform.childCount; i < health; i++) {
                Instantiate(lifeSpritePrefab, lifeSpriteParent.transform);
            }
        }

        public void SetInvinciblity(bool isTrue, bool hasDelay = false) {
            _isInvincible = isTrue;
            if (hasDelay) {
                _hasRecentlyBeenDamaged = true;
            }
        }

        public bool TakeDamage() {
            if (player.GetIsDead()) {
                return false;
            }

            if (_isInvincible) {
                return false;
            }

            if (_hasRecentlyBeenDamaged) {
                return false;
            }
            if (lifeSpriteParent.transform.childCount - 1 <= 0) {
                Death();
                onDeath?.Invoke();
            }
            else {
                Destroy(lifeSpriteParent.transform.GetChild(0).gameObject);
                onTakeDamage?.Invoke();
                _hasRecentlyBeenDamaged = true;
            }
            return true;
        }

        private void Start() {
            SetupStartingHealth();
        }

        private void Update() {
            if (player.GetIsDead()) {
                return;
            }

            if (_hasRecentlyBeenDamaged) {
                var t = Mathf.Min(_timeForCD += Time.deltaTime, takeDamageCoolDown);
                if (t == takeDamageCoolDown) {
                    _hasRecentlyBeenDamaged = false;
                    _timeForCD = 0;
                }
            }
        }

        private void Death() {
            this.transform.DOScale(deathBloatSize, bloatTime).SetEase(bloatEasing);
            player.Death();
        }

        private void SetupStartingHealth() {
            for (int i = 0; i < health; i++) {
                Instantiate(lifeSpritePrefab, lifeSpriteParent.transform);
            }
        }
    }
}
