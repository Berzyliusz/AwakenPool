using System.Collections.Generic;
using UnityEngine;

namespace AwakenPool
{
    public class BallsSettler
    {
        readonly List<Ball> balls;
        readonly float movementThresholdSquared;

        public BallsSettler(List<Ball> balls, float movementThreshold)
        {
            this.balls = balls;
            movementThresholdSquared =  movementThreshold * movementThreshold;

            foreach(var ball in balls)
            {
                ball.OnBallDestroyed += HandleBallDestroyed;
            }
        }

        void HandleBallDestroyed(Ball ball)
        {
            balls.Remove(ball);
        }

        public bool AreBallsSettled()
        {
            bool areSettled = true;

            foreach (Ball ball in balls)
            {
                if(ball.Rigidbody.velocity.sqrMagnitude > movementThresholdSquared)
                {
                    Debug.Log($"Ball not settled: " + ball.gameObject.name);
                    areSettled = false; 
                    break;
                }
            }

            return areSettled;
        }
    }
}