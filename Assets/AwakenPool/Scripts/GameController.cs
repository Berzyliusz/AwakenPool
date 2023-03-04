using System;
using UnityEngine;

namespace AwakenPool
{
    public enum GameState
    {
        CueMove,
        BallSettling,
        Won,
        Lost
    };

    public class GameController : IMovesHandler
    {
        public event Action<int, int> OnMoveMade;
        public int CurrentMoves { get; private set; }
        public int MaxMoves { get; private set; }

        readonly GameSetup gameSetup;
        readonly CueController cueController;
        readonly BallsSettler ballSettler;
        readonly ScoreController scoreController;

        GameState currentState;

        public GameController(GameSetup gameSetup, CueController cueController, 
            BallsSettler ballSettler, ScoreController scoreController)
        {
            this.gameSetup = gameSetup;
            this.cueController = cueController;
            this.ballSettler = ballSettler;
            this.scoreController = scoreController;
            MaxMoves = gameSetup.MaxMoves;

            currentState = GameState.CueMove;

            cueController.OnForceApplied += HandleForceApplied;
            cueController.SetCueActive(true);
        }

        public void Update()
        {
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

        void HandleForceApplied()
        {
            CurrentMoves++;
            currentState = GameState.BallSettling;
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
                currentState = GameState.Won;
                return;
            }

            if(CurrentMoves > MaxMoves)
            {
                currentState= GameState.Lost;
                return;
            }

            currentState = GameState.CueMove;
            cueController.SetCueActive(true);
        }
    }
}