using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;

namespace Blasters {
    public class Blaster : MonoBehaviour, IBlaster {
        [Header("Firing Variables")]
        [SerializeField] private int amountOfBurstFire = 3;
        [SerializeField] private float timeBetweenBurst = 0.3f;
        [SerializeField] private float additionalDelay = 0f;
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private PoolTypes originalBullet;
        
        private PoolTypes newBulletType;

        private int _originalAmountOfBurstFire;
        private float _originalFireRate;
        private bool _isStillBurstFiring = false;
        private bool _isShootingDelayed = false;
        private int _remainingBurst;
        private float _timeBurst;
        private float _timeDelay;

        public void ChangeBullet(PoolTypes newBullet = PoolTypes.Empty, int newBurstAmount = 0, float newFireRate = 0) {
            if (newBullet != PoolTypes.Empty) {
                newBulletType = newBullet;
            }
            if (newBurstAmount > 0) {
                amountOfBurstFire = newBurstAmount;
            }
            if (newFireRate > 0) {
                timeBetweenBurst = newFireRate;
            }
        }

        public void RevertBullet() {
            timeBetweenBurst = _originalFireRate;
            amountOfBurstFire = _originalAmountOfBurstFire;
            newBulletType = PoolTypes.Empty;
        }

        public void Fire() {
            if (!_isStillBurstFiring && !_isShootingDelayed) {
                _isStillBurstFiring = true;
                _remainingBurst = amountOfBurstFire;
            }
        }

        private void Start() {
            _originalAmountOfBurstFire = amountOfBurstFire;
            _originalFireRate = timeBetweenBurst;
        }

        private void Update() {
            if (_isStillBurstFiring) {
                var t = Mathf.Min(_timeBurst += Time.deltaTime, timeBetweenBurst);
                if (t == timeBetweenBurst) {
                    SpawnBullet();
                    _remainingBurst -= 1;
                    _timeBurst = 0;
                    if (_remainingBurst <= 0) {
                        _isShootingDelayed = true;
                        _isStillBurstFiring = false;
                    }
                }
            }

            if (_isShootingDelayed) {
                var tD = Mathf.Min(_timeDelay += Time.deltaTime, additionalDelay);
                if (tD == additionalDelay) {
                    _isShootingDelayed = false;
                }
            }
        }

        private void SpawnBullet() {
            //Bullet b = Instantiate(GetBullet(), bulletSpawn.position, Quaternion.identity);
            var b = PoolManager.instance.GetPoolObject(GetBullet()).GetComponent<Bullet>();
            b.gameObject.SetActive(true);
            b.transform.position = bulletSpawn.position;
            b.Setup(transform.rotation);
        }

        private PoolTypes GetBullet() {
            if (newBulletType != PoolTypes.Empty) {
                return newBulletType;
            }
            return originalBullet;
        }

    }
}

