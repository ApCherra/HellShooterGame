using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Interfaces;

namespace MyGame.Model.Models
{
    public class Collision
    {
        public ICollidable Entity1 { get; private set; }
        public ICollidable Entity2 { get; private set; }

        public Collision(ICollidable entity1, ICollidable entity2)
        {
            Entity1 = entity1;
            Entity2 = entity2;
        }
    }
}