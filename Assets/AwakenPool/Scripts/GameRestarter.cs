﻿using AwakenPool.Gameplay;
using AwakenPool.Inputs;
using UnityEngine.SceneManagement;

namespace AwakenPool
{
    public class GameRestarter
    {
        bool gameEnded;
        readonly IInputs inputs;

        public GameRestarter(IGameEnder gameEnder, IInputs inputs)
        {
            gameEnder.OnGameLost += HandleGameEnded;
            gameEnder.OnGameWon += HandleGameEnded;
            this.inputs = inputs;
        }

        public void Update()
        {
            if(inputs.RestartGame)
                RestartScene();

            if(gameEnded && inputs.AnyInput)
                RestartScene();
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