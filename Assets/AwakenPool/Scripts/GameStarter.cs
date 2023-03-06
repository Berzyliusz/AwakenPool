using AwakenPool.Gameplay;
using AwakenPool.Inputs;
using AwakenPool.UI;
using System.Linq;
using UnityEngine;

namespace AwakenPool
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] GameParamsDisplayer gameParamsDisplayer;
        [SerializeField] GameEndDisplayer gameEndDisplayer;

        // This could be expanded into some 'GameSetupProvided'
        // that allows selection of levels
        [SerializeField] GameSetup gameSetup;

        CueController cueController;
        GameController gameController;
        GameRestarter gameRestarer;
        IInputs inputs;

        public void StartGame(GameSetup gameSetup)
        {
            SetupBalls(gameSetup);

            cueController = new CueController(inputs, gameSetup);
            var scoreController = new ScoreController(gameSetup.Balls, gameSetup.ScoreToWin);
            var allBalls = gameSetup.Balls.ToList();
            allBalls.Add(gameSetup.PlayableBall);
            var ballSettler = new BallsSettler(allBalls);
            gameController = new GameController(gameSetup, cueController, ballSettler, scoreController);
            gameRestarer = new GameRestarter(gameController, inputs);

            gameParamsDisplayer.SetScoreHandling(scoreController);
            gameParamsDisplayer.SetMovesHandling(gameController);
            gameEndDisplayer.SetGameEndHandling(gameController);
        }

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
            gameRestarer.Update();
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