using System;

namespace AwakenPool.Gameplay
{
    public interface IGameEnder
    {
        public event Action OnGameWon;
        public event Action OnGameLost;
    }
}