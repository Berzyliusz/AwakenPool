using AwakenPool.Inputs;
using AwakenPool.UI;
using System.Linq;
using UnityEngine;

namespace AwakenPool
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] CueController cueController;
        [SerializeField] GameParamsDisplayer gameParamsDisplayer;

        // This could be expanded into some 'GameSetupProvided'
        // that allows selection of levels
        [SerializeField] GameSetup gameSetup;

        GameController gameController;
        IInputs inputs;

        void Awake()
        {
            inputs = new InputWrapper();
        }

        void Start()
        {
            StartGame(gameSetup);
        }

        void Update()
        {
            gameController.Update();
        }

        public void StartGame(GameSetup gameSetup)
        {
            SetupBalls(gameSetup);

            cueController.Initialize(inputs, gameSetup);
            var scoreController = new ScoreController(gameSetup.Balls, gameSetup.ScoreToWin);
            var allBalls = gameSetup.Balls.ToList();
            allBalls.Add(gameSetup.PlayableBall);
            var ballSettler = new BallsSettler(allBalls, Mathf.Epsilon);
            gameController = new GameController(gameSetup, cueController, ballSettler, scoreController);

            gameParamsDisplayer.SetScoreHandling(scoreController);
            gameParamsDisplayer.SetMovesHandling(gameController);

            // Setup UI:
            // Game won
            // Game lost
        }

        void SetupBalls(GameSetup gameSetup)
        {
            gameSetup.PlayableBall.Initialize(true);
            foreach (var ball in gameSetup.Balls)
            {
                ball.Initialize(false);
            }
        }
    }
}