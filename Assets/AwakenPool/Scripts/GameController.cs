using System;
using UnityEngine;

namespace AwakenPool.Gameplay
{
    public enum GameState
    {
        CueMove,
        BallSettling,
        Won,
        Lost
    };

    public class GameController : IMovesHandler, IGameEnder
    {
        public event Action<int, int> OnMoveMade;
        public event Action OnGameWon;
        public event Action OnGameLost;
        public int CurrentMoves { get; private set; }
        public int MaxMoves { get; private set; }

        readonly GameSetup gameSetup;
        readonly CueController cueController;
        readonly BallsSettler ballSettler;
        readonly ScoreController scoreController;

        const float stateChangeDuration = 0.1f;
        float timeOfStateChange;

        GameState currentState;

        public GameController(GameSetup gameSetup, CueController cueController, 
            BallsSettler ballSettler, ScoreController scoreController)
        {
            this.gameSetup = gameSetup;
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

            Debug.Log(currentState);

            switch (currentState)
            {
                case GameState.CueMove:
                    break;
                    case GameState.BallSettling:
                    WaitForBallsSettled();
                    break;
                    case GameState.Won:
                    // Display game won, stop updating
                    break;
                    case GameState.Lost:
                    // Display game lost, stop updating
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
            // Todo: Wait a couple of frames, for rigidbody to gain velocity
        }

        void WaitForBallsSettled()
        {
            if (!ballSettler.AreBallsSettled())
            {
                Debug.Log("Balls not settled");
                return;
            }

            Debug.Log("Balls settled");

            if (scoreController.IsGameWon)
            {
                ChangeGameState(GameState.Won);
                return;
            }

            if(CurrentMoves > MaxMoves)
            {
                ChangeGameState(GameState.Lost);
                return;
            }

            ChangeGameState(GameState.CueMove);
            cueController.SetCueActive(true);
        }
    }
}