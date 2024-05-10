using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MyGame.Model.MovementPatterns
{
    public class RandomMovementPattern : MovementPattern
    {
        private int _wonderFrequency; // Frequency in milliseconds
        private Vector2 _lastRandomVector;
        private TimeSpan _elapsedTime;
        private Random _random;

        public RandomMovementPattern(int wonderFrequency)
        {
            _random = new Random();
            _wonderFrequency = wonderFrequency;
            _lastRandomVector = GetRandomUnitVector();
        }

        public override Vector2 GetMoveVector(float magnitude)
        {
            // Update elapsed time
            _elapsedTime += TimeSpan.FromMilliseconds(_random.Next(16, 33)); // Assuming 60 FPS, adjust if necessary

            // Check if it's time to get a new random vector
            if (_elapsedTime.TotalMilliseconds >= _wonderFrequency)
            {
                // Get new random unit vector
                _lastRandomVector = GetRandomUnitVector();

                // Reset elapsed time
                _elapsedTime = TimeSpan.Zero;
            }

            // Scale the unit vector to desired magnitude
            Vector2 scaledVector = _lastRandomVector * magnitude;

            return scaledVector;
        }

        private Vector2 GetRandomUnitVector()
        {
            // Generate random angle between 0 and 2π
            float angle = (float)(_random.NextDouble() * Math.PI * 2);

            // Calculate x and y components using trigonometry
            float x = (float)Math.Cos(angle);
            float y = (float)Math.Sin(angle);

            // Return the unit vector
            return new Vector2(x, y);
        }
    }
}