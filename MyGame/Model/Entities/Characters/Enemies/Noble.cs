using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Controller.Managers;
using MyGame.Model.Attacks;
using MyGame.Model.MovementPatterns;

namespace MyGame.Model.Entities.Characters.Enemies
{
    public class Noble : Enemy
    {
        public Noble() {
            AttackManager = new AttackManager() {
                AutoShoot = true,
                Frequency = 1500
            };
            Attack attack = new Scatter();
            AttackManager.Attacks.Add(attack);
            Speed = 20;
            Image.TexturePath = "noble";
            Image.Scale = new Vector2(0.3f, 0.3f);
            MovementPattern = new CircularMovementPattern(5);
            HP = 20;
        }
    }
}