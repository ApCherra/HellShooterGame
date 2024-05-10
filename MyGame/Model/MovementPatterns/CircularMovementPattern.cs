using System;
using Microsoft.Xna.Framework;

namespace MyGame.Model.MovementPatterns
{
    public class CircularMovementPattern : MovementPattern
    {
        private float _radius;
        private float _angle;

        public CircularMovementPattern(float radius)
        {
            _radius = radius;
        }

        public override Vector2 GetMoveVector(float magnitude)
        {
            // Increment angle each time GetMoveVector is called
            _angle += 0.01f; // Adjust angular speed as needed

            // Calculate X and Y components of the movement vector using trigonometry
            float x = _radius * (float)Math.Cos(_angle);
            float y = _radius * (float)Math.Sin(_angle);

            // Scale the movement vector by the specified magnitude
            Vector2 moveVector = new Vector2(x, y) * magnitude;

            return moveVector;
        }
    }
}
