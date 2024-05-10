using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Model.Attacks;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Models;
using MyGame.Model.MovementPatterns;
using MyGame.Utilities;
using MyGame.View.Visuals;

namespace MyGame.Model.Entities.Characters.Enemies;

public class Boss : Enemy
{
    public float SwitchTime;
    private double counter;

    public Boss()
    {
        MovementPattern = new CircularMovementPattern(10);
        AttackManager = new AttackManager()
        {
            AutoShoot = true,
            Frequency = 12000,
        };
        Attack attack1 = new BossAttack1();
        AttackManager.Attacks.Add(attack1);
        Attack attack2 = new BossAttack2();
        AttackManager.Attacks.Add(attack2);
        Speed = 8;
        Image.TexturePath = "boss";
        Image.Scale = new Vector2(0.45f, 0.45f);
        HitBox.SetSize(140, 140);
        HP = 50;
        SwitchTime = 12;
        counter = SwitchTime;
    }
}