using System;

namespace AwakenPool
{
    public class ScoreController
    {
        public int CurrentScore { get; private set; }
        public bool IsGameWon => CurrentScore > scoreToWin;
        public event Action<int> OnScoreUpdated;

        readonly int scoreToWin;

        public ScoreController(Ball[] balls, int scoreToWin)
        {
            foreach (var ball in balls)
            {
                ball.OnBallDestroyed += HandleBallDestroyed;
            }

            this.scoreToWin = scoreToWin;
        }

        void HandleBallDestroyed(Ball ball)
        {
            CurrentScore += ball.PointsForHit;
            OnScoreUpdated?.Invoke(CurrentScore);
        }
    }
}