using System;
using System.Collections.Generic;
using Player;

namespace PowerUps {
    public interface IPowerUp {
        Action ActivatePowerUp(PlayerShip player);
        void ListenToOnConsume(Action<IPowerUp> onConsume);
        void OnConsume(IPowerUp powerUp);
        void OnCancel();
    }
}

