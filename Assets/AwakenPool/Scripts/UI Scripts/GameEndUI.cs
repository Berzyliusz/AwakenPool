using AwakenPool.Gameplay;
using UnityEngine;

namespace AwakenPool.UI
{
    public class GameEndUI : MonoBehaviour
    {
        public void SetGameEndHandling(IGameEnder gameEnder)
        {
            gameEnder.OnGameWon += HandleGameWon;
            gameEnder.OnGameLost += HandleGameLost;
        }

        void HandleGameWon()
        {
            
        }

        void HandleGameLost()
        {
            
        }
    }
}