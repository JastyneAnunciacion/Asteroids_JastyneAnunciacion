using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player {
    public class PlayerInput : MonoBehaviour {
        [SerializeField] private PlayerShip player;

        [Header("Contol Input")]
        [SerializeField] private KeyCode moveForward = KeyCode.UpArrow;
        [SerializeField] private KeyCode rotateLeft = KeyCode.LeftArrow;
        [SerializeField] private KeyCode rotateRight = KeyCode.RightArrow;
        [SerializeField] private KeyCode actionButton = KeyCode.Space;

        [Header("Rotation Variables")]
        [Range(1f, 5f)]
        [SerializeField] private float maxRotation = 2f;
        [Range(1f, 300f)]
        [SerializeField] private float rotationMultiplier = 1f;
        [Range(0.5f, 3f)]
        [SerializeField] private float timeForMaxRotation = 1f;

        [Header("Acceleration Variables")]
        [Range(1f, 5f)]
        [SerializeField] private float maxAcceleration = 2f;
        [Range(1f, 10f)]
        [SerializeField] private float accelerationMultiplier = 1f;
        [Range(0.5f, 5f)]
        [SerializeField] private float timeForMaxAcceleration = 1f;
        [Range(0f, 5f)]
        [SerializeField] private float stoppingForce = 3f;

        [Header("Action Variables")]
        [SerializeField] private UnityEvent onActionButton;

        private float _leftInput = 0;
        private float _rightInput = 0;
        private float _upInput = 0;

        private float _originalRotationMultiplier;
        private float _originalAccelerationMultiplier;
        private float _originalStoppingForce;

        public void ModifyMultipliers(float newRotation = 0f, float newAcceleration = 0f, float newStoppingForce = 0f) {
            if (newRotation > 0f) {
                rotationMultiplier = newRotation;
            }
            if (newAcceleration > 0f) {
                accelerationMultiplier = newAcceleration;
            }
            if (newStoppingForce > 0f) {
                stoppingForce = newStoppingForce;
            }
        }

        public void RevertMultipliers() {
            rotationMultiplier = _originalRotationMultiplier;
            accelerationMultiplier = _originalAccelerationMultiplier;
            stoppingForce = _originalStoppingForce;
        }

        private void Start() {
            _originalRotationMultiplier = rotationMultiplier;
            _originalAccelerationMultiplier = accelerationMultiplier;
            _originalStoppingForce = stoppingForce;
        }

        private void Update() {
            if (player.GetIsDead() || !GameManager.instance.PlayerCanMove) {
                return;
            }

            Rotate();
            MoveForward();
            Action();
        }

        private void Action() {
            if (Input.GetKeyDown(actionButton)) {
                if (!GameManager.instance.GameStarted) {
                    GameManager.instance.StartGame();
                }
                onActionButton?.Invoke();
            }
        }

        private void MoveForward() {
            var totalUpAcceleration = 0f;
            if (Input.GetKey(moveForward)) {
                if (!GameManager.instance.GameStarted) {
                    GameManager.instance.StartGame();
                }

                _upInput = Mathf.Min(_upInput += Time.deltaTime, timeForMaxAcceleration);
                totalUpAcceleration = Mathf.Lerp(0, maxAcceleration, _upInput / timeForMaxAcceleration);
            }
            else {
                _upInput = Mathf.Max(_upInput -= Time.deltaTime * stoppingForce, 0);
                totalUpAcceleration = Mathf.Lerp(0, maxAcceleration, _upInput / timeForMaxAcceleration);
            }
            transform.Translate(Vector3.up * totalUpAcceleration * accelerationMultiplier * Time.deltaTime);
        }

        private void Rotate() {
            var totalLeftRotation = 0f;
            var totalRightRotation = 0f;
            if (Input.GetKey(rotateLeft)) {
                _leftInput = Mathf.Max(_leftInput -= Time.deltaTime, -timeForMaxRotation);
                totalLeftRotation = Mathf.Lerp(0, -maxRotation, _leftInput / -timeForMaxRotation);
            }
            else {
                _leftInput = Mathf.Min(_leftInput += Time.deltaTime, 0);
            }

            if (Input.GetKey(rotateRight)) {
                _rightInput = Mathf.Min(_rightInput += Time.deltaTime, timeForMaxRotation);
                totalRightRotation = Mathf.Lerp(0, maxRotation, _rightInput / timeForMaxRotation);
            }
            else {
                _rightInput = Mathf.Max(_rightInput -= Time.deltaTime, 0);
            }
            var totalRotation = totalLeftRotation + totalRightRotation;
            var rotation = 0f;
            rotation -= (totalRotation * rotationMultiplier) * Time.deltaTime;
            var newaxis = new Vector3(0, 0, rotation);
            transform.Rotate(newaxis);
        }
    }
}

