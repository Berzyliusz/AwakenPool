using System.Collections.Generic;
using UnityEngine;

namespace AwakenPool.Gameplay
{
    public class BallsSettler
    {
        readonly List<Ball> balls;
        const float movementThreshold = 0.003f;

        public BallsSettler(List<Ball> balls)
        {
            this.balls = balls;

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
                if(ball.Rigidbody.velocity.sqrMagnitude > movementThreshold)
                {
                    areSettled = false; 
                    break;
                }
            }

            return areSettled;
        }

        public void StopBallsMovement()
        {
            foreach(var ball in balls)
            {
                ball.Rigidbody.velocity = Vector3.zero;
                ball.Rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}