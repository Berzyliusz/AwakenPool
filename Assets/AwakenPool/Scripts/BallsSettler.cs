using System.Collections.Generic;

namespace AwakenPool.Gameplay
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
                    areSettled = false; 
                    break;
                }
            }

            return areSettled;
        }
    }
}