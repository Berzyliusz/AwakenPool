using AwakenPool.Gameplay;
using System;
using UnityEngine.SceneManagement;

namespace AwakenPool
{
    public class GameRestarter
    {
        bool gameEnded;

        public void Update()
        {
            // restart on button 

            // restart on any when game ended
        }

        public GameRestarter(IGameEnder gameEnder)
        {
            gameEnder.OnGameLost += HandleGameEnded;
            gameEnder.OnGameWon += HandleGameEnded;
        }

        void HandleGameEnded()
        {
            gameEnded = true;
        }

        void RestartScene()
        {
            var currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}