using Pool.Gameplay;
using UnityEngine;

namespace Pool.UI
{
    public class GameEndDisplayer : MonoBehaviour
    {
        [SerializeField] GameObject gameWonPopup;
        [SerializeField] GameObject gameLostPopup;

        public void SetGameEndHandling(IGameEnder gameEnder)
        {
            gameEnder.OnGameWon += HandleGameWon;
            gameEnder.OnGameLost += HandleGameLost;
        }

        void HandleGameWon()
        {
            gameWonPopup.SetActive(true);
        }

        void HandleGameLost()
        {
            gameLostPopup.SetActive(true);
        }

        void Awake()
        {
            gameWonPopup.SetActive(false);
            gameLostPopup.SetActive(false);
        }
    }
}