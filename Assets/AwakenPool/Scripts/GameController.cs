using System;
using UnityEngine;

namespace AwakenPool.Gameplay
{
    public class GameController : IMovesHandler, IGameEnder
    {
        public event Action<int, int> OnMoveMade;
        public event Action OnGameWon;
        public event Action OnGameLost;
        public int CurrentMoves { get; private set; }
        public int MaxMoves { get; private set; }

        readonly CueController cueController;
        readonly BallsSettler ballSettler;
        readonly ScoreController scoreController;

        const float stateChangeDuration = 0.1f;
        float timeOfStateChange;

        GameState currentState;

        public GameController(GameSetup gameSetup, CueController cueController, 
            BallsSettler ballSettler, ScoreController scoreController)
        {
            this.cueController = cueController;
            this.ballSettler = ballSettler;
            this.scoreController = scoreController;
            MaxMoves = gameSetup.MaxMoves;

            ChangeGameState(GameState.CueMove);

            cueController.OnForceApplied += HandleForceApplied;
            cueController.SetCueActive(true);
        }

        public void Update()
        {
            if (Time.time < timeOfStateChange + stateChangeDuration)
                return;

            if (currentState == GameState.Ended)
                return;

            ProcessGameState();
        }

        void ProcessGameState()
        {
            switch (currentState)
            {
                case GameState.CueMove:
                    // We could update the cue from here?
                    break;
                case GameState.BallSettling:
                    WaitForBallsSettled();
                    break;
                case GameState.Won:
                    currentState = GameState.Ended;
                    OnGameWon?.Invoke();
                    break;
                case GameState.Lost:
                    currentState = GameState.Ended;
                    OnGameLost?.Invoke();
                    break;
            }
        }

        void ChangeGameState(GameState newState)
        {
            timeOfStateChange = Time.time;
            currentState = newState;
        }

        void HandleForceApplied()
        {
            CurrentMoves++;
            ChangeGameState(GameState.BallSettling);
            OnMoveMade?.Invoke(CurrentMoves, MaxMoves);
        }

        void WaitForBallsSettled()
        {
            if (!ballSettler.AreBallsSettled())
            {
                return;
            }

            if (scoreController.IsGameWon)
            {
                ChangeGameState(GameState.Won);
                return;
            }

            if(CurrentMoves >= MaxMoves)
            {
                ChangeGameState(GameState.Lost);
                return;
            }

            ChangeGameState(GameState.CueMove);
            cueController.SetCueActive(true);
        }
    }
}