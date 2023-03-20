using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pooling;
using System;
using Player;

namespace PowerUps {
    [CreateAssetMenu(fileName = "Custom Power Up", menuName = "Game/PowerUp/Custom Power Up")]
    public class CustomPowerUp : PowerUpDataScript {
        [Header("Player Movement Variables")]
        [SerializeField] private int addHealth;
        [SerializeField] private float rotationSpeedMultiplier;
        [SerializeField] private float accelerationSpeedMultiplier;
        [SerializeField] private float stoppingForceMultiplier;

        [Header("Player Blaster Variables")]
        [SerializeField] private int newBurstFireAmount;
        [SerializeField] private float newFireRate;
        [SerializeField] private PoolTypes newBullet;

        public override Action ActivatePowerUp(PlayerShip player) {
            base.ActivatePowerUp(player);
            if (powerUpDuration > 0) {
                player.StartCoroutine(ChangeVariablesTemporarily(player));
            }
            else {
                player.GetHealthScript().AddHealth(addHealth);
                player.GetPlayerInput().ModifyMultipliers(rotationSpeedMultiplier, accelerationSpeedMultiplier, stoppingForceMultiplier);
                player.GetBlasterScript().ChangeBullet(newBullet, 5);
            }
            return OnCancel;
        }

        private IEnumerator ChangeVariablesTemporarily(PlayerShip player) {
            player.GetHealthScript().AddHealth(addHealth);
            player.GetPlayerInput().ModifyMultipliers(rotationSpeedMultiplier, accelerationSpeedMultiplier, stoppingForceMultiplier);
            player.GetBlasterScript().ChangeBullet(newBullet, newBurstFireAmount, newFireRate);
            yield return new WaitForSeconds(powerUpDuration);
            player.GetPlayerInput().RevertMultipliers();
            player.GetBlasterScript().RevertBullet();
        }
    }
}

