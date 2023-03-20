using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scores;
using Pooling;

namespace Asteroids {
    public class Asteroid : MonoBehaviour {
        [SerializeField] private int timesToSplit;
        [SerializeField] private int amountOfAsteriodsPerSplit;
        [SerializeField] private float asteroidSpeed = 10f;
        [SerializeField] private float speedModifierPerSplit = 0.1f;
        [SerializeField] private float scaleModifierPerSplit = 0.75f;
        [SerializeField] private int scoreToAdd = 50;

        [Header("Not Moving Property")]
        [SerializeField] private float spinSpeed;

        private int _currentSplit = 1;
        private Vector3 _originalScale;
        private bool _dontMove = false;

        public void DontMove() {
            _dontMove = true;
        }

        public void Setup(int newIteration) {
            _currentSplit = newIteration;
            if (_currentSplit <= 1) {
                return;
            }
            for (int i = 0; i < _currentSplit - 1; i++) {
                transform.localScale *= scaleModifierPerSplit;
            }
        }

        public void Split(bool isHitByShip) {
            if ((_currentSplit + 1) <= timesToSplit) {
                for (int i = 0; i < amountOfAsteriodsPerSplit; i++) {
                    if (PoolManager.instance.GetPoolObject(PoolTypes.Asteroids).TryGetComponent(out Asteroid asteroid)) {
                        asteroid.Setup(_currentSplit + 1);
                        asteroid.gameObject.SetActive(true);
                        asteroid.transform.position = this.transform.position;
                    }
                }
            }
            else {
                if (!isHitByShip) {
                    ScoreManager.instance.AddScore(scoreToAdd);
                }
            }
            gameObject.SetActive(false);

        }

        private void Awake() {
            _originalScale = transform.localScale;
        }

        private void Start() {
            RandomizeRotation();
        }

        private void Update() {
            if (!_dontMove) {
                transform.Translate(Vector3.up * TotalAsteroidSpeed() * Time.deltaTime);
            }
            else {
                transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
            }
        }

        private float TotalAsteroidSpeed() {
            return asteroidSpeed * (_currentSplit * speedModifierPerSplit);
        }

        private void RandomizeRotation() {
            var randRotation = Random.Range(-360f, 360f);
            var newaxis = new Vector3(0, 0, randRotation);
            transform.Rotate(newaxis);
        }

        private void OnDisable() {
            transform.localScale = _originalScale;
            _dontMove = false;
        }
    }
}
