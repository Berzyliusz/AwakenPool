using System.Collections.Generic;

namespace AwakenPool.Gameplay
{
    public class BallsSettler
    {
        readonly List<Ball> balls;
        const float movementThreshold = 0.02f;

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
    }
}