using System;

namespace Pool.Gameplay
{
    public interface IMovesHandler
    {
        int CurrentMoves { get; }
        int MaxMoves { get; }
        public event Action<int, int> OnMoveMade;
    }
}