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

    public class ScoreController : IScoreHandler
    {
        public int CurrentScore { get; private set; }
        public int ScoreToWin { get; private set; }

        public bool IsGameWon => CurrentScore > ScoreToWin;
        public event Action<int,int> OnScoreUpdated;

        public ScoreController(Ball[] balls, int scoreToWin)
        {
            foreach (var ball in balls)
            {
                ball.OnBallDestroyed += HandleBallDestroyed;
            }

            this.ScoreToWin = scoreToWin;
        }

        void HandleBallDestroyed(Ball ball)
        {
            CurrentScore += ball.PointsForHit;
            OnScoreUpdated?.Invoke(CurrentScore, ScoreToWin);
        }
    }
}