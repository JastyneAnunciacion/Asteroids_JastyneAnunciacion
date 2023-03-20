using System;
using UnityEngine;
using Player;

namespace PowerUps {
    public abstract class PowerUpDataScript : ScriptableObject, IPowerUp {
        [Header("Power Up Variables")]
        [SerializeField] protected float powerUpDuration;

        protected Action<IPowerUp> _onConsume;
        protected Action<IPowerUp> _onCancel;
        protected bool _isConsumed;

        public virtual Action ActivatePowerUp(PlayerShip player) {
            _onConsume?.Invoke(this);
            return OnCancel;
        }

        public virtual void Initialize() {
            _onConsume += OnConsume;
        }

        public void ListenToOnConsume(Action<IPowerUp> onConsume) {
            _onConsume += ((p) => { onConsume?.Invoke(p); });
        }

        public virtual void OnConsume(IPowerUp powerUp) {
            _isConsumed = true;
        }
        public virtual void OnCancel() {
            _onCancel?.Invoke(this);
        }
    }
}


