using UnityEngine;
using TMPro;
using AwakenPool.Gameplay;

namespace AwakenPool.UI
{
    public class GameParamsDisplayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI movesText;

        public void SetScoreHandling(IScoreHandler scoreHandler)
        {
            scoreHandler.OnScoreUpdated += UpdateScoreText;
            UpdateScoreText(0, scoreHandler.ScoreToWin);
        }

        public void SetMovesHandling(IMovesHandler moveHandler)
        {
            moveHandler.OnMoveMade += UpdateMovesText;
            UpdateMovesText(0, moveHandler.MaxMoves);
        }

        void UpdateScoreText(int currentScore, int neededScore)
        {
            scoreText.text = currentScore + " / " + neededScore;
        }

        void UpdateMovesText(int currentMoves, int neededMoves)
        {
            movesText.text = currentMoves + " / " + neededMoves;
        }

        void Awake()
        {
            scoreText.text = string.Empty;
            movesText.text = string.Empty;
        }
    }
}