using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace PowerUps {
    public class PickablePowerUp : MonoBehaviour {
        [SerializeField] private PowerUpDataScript powerUpData;
        [SerializeField] private bool disableOnPickUp = true;

        public void ActivatePowerUp(PlayerShip player) {
            powerUpData.ActivatePowerUp(player);
            if (disableOnPickUp) {
                gameObject.SetActive(false);
            }
        }
    }
}
