using System;
using Player;
using UnityEngine;

namespace PowerUps {
    [CreateAssetMenu(fileName = "Barrier", menuName = "Game/PowerUp/Barrier")]
    public class BarrierPowerUp : PowerUpDataScript {
        [Header("Barrier Properties")]
        [SerializeField] private BarrierObject barrierPrefab;

        public override Action ActivatePowerUp(PlayerShip player) {
            base.ActivatePowerUp(player);
            var barrier = Instantiate(barrierPrefab, player.transform);
            barrier.Setup(powerUpDuration, player);
            player.GetHealthScript().SetInvinciblity(true);
            return OnCancel;
        }
    }
}

