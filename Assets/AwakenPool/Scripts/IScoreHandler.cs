using System;

namespace AwakenPool
{
    public interface IScoreHandler
    {
        int CurrentScore { get; }
        int ScoreToWin { get; }
        bool IsGameWon { get; }
        event Action<int, int> OnScoreUpdated;
    }
}