using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGame.Model.MovementPatterns
{
    public abstract class MovementPattern
    {
        public MovementPattern() {
            
        }

        public abstract Vector2 GetMoveVector(float magnitude);
    }
}