using System;
using Player;
using UnityEngine;
using Pooling;
using System.Collections;


namespace PowerUps {
    [CreateAssetMenu(fileName = "Blaster", menuName = "Game/PowerUp/Blaster")]

    public class BlasterPowerUp : PowerUpDataScript {
        [Header("Blaster Properties")]
        [SerializeField] private PoolTypes newBullet;
        [SerializeField] private int newBurstAmount;
        [SerializeField] private float newFireRate;

        public override Action ActivatePowerUp(PlayerShip player) {
            base.ActivatePowerUp(player);
            player.StartCoroutine(ChangeBulletTemporarily(player));
            return OnCancel;
        }

        private IEnumerator ChangeBulletTemporarily(PlayerShip player) {
            player.GetBlasterScript().ChangeBullet(newBullet, newBurstAmount);
            yield return new WaitForSeconds(powerUpDuration);
            player.GetBlasterScript().RevertBullet();
        }
    }
}
