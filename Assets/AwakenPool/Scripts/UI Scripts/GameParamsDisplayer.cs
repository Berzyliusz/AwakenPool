using UnityEngine;
using TMPro;

namespace AwakenPool.UI
{
    public class GameParamsDisplayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI movesText;

        public void SetScoreUpdateEvent(ScoreController scoreController)
        {
            scoreController.OnScoreUpdated += UpdateScoreText;
            UpdateScoreText(0, scoreController.CurrentScore);
        }

        void Awake()
        {
            scoreText.text = string.Empty;
            movesText.text = string.Empty;
        }

        void UpdateScoreText(int currentScore, int neededScore)
        {
            scoreText.text = currentScore + " / " + neededScore;
        }

        void UpdateMovesText(int currentMoves, int neededMoves)
        {
            movesText.text = currentMoves + " / " + neededMoves;
        }
    }
}