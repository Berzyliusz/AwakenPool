using System;

namespace Pool.Gameplay
{
    public interface IScoreHandler
    {
        int CurrentScore { get; }
        int ScoreToWin { get; }
        bool IsGameWon { get; }
        event Action<int, int> OnScoreUpdated;
    }
}