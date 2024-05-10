using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Utilities;

namespace MyGame.Model.MovementPatterns
{
    public class ConstantMovementPattern : MovementPattern
    {
        private Vector2 _moveDirection;
        private static Random _random = new Random();
        public ConstantMovementPattern(Vector2 moveDirection) {
            _moveDirection = moveDirection;
        }

        public override Vector2 GetMoveVector(float magnitude)
        {
            return Helpers.GetUnitVector(_moveDirection) * magnitude;
        }

        public void SetDirection(Vector2 newDir) 
        {
            _moveDirection = newDir;
        }

        public void NewRandomDir() {
            // Generate a random angle in radians
            float angle = (float)(_random.NextDouble() * Math.PI * 2);

            // Convert the angle to a direction vector
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

            // Set new direction
            _moveDirection = direction;
        }
    }
}