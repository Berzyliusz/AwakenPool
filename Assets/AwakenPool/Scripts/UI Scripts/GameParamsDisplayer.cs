using UnityEngine;
using TMPro;

namespace AwakenPool.UI
{
    public class GameParamsDisplayer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI movesText;

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