using System;

namespace Pool.Gameplay
{
    public interface IGameEnder
    {
        public event Action OnGameWon;
        public event Action OnGameLost;
    }
}