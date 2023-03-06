using System;

namespace AwakenPool.Gameplay
{
    public interface IMovesHandler
    {
        int CurrentMoves { get; }
        int MaxMoves { get; }
        public event Action<int, int> OnMoveMade;
    }
}