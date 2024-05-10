using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Controller.Managers;
using MyGame.Model.Attacks;

namespace MyGame.Model.Entities.Characters.Enemies
{
    public class Peasant : Enemy
    {
        public Peasant()
        {
            AttackManager = new AttackManager() {
                AutoShoot = true
            };
            Attack attack = new SingleShot()
            {
                Direction = new Vector2(0, 1)
            };
            AttackManager.Attacks.Add(attack);
            Speed = 40;
            Image.TexturePath = "peasant";
            Image.Scale = new Vector2(0.18f, 0.18f);
            HP = 10;
        }
    }
}