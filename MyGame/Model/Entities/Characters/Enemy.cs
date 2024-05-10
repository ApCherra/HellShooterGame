using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Models;
using MyGame.Model.MovementPatterns;
using MyGame.Utilities;
using MyGame.View.Visuals;
using MyGame.Interfaces;

namespace MyGame.Model.Entities.Characters;

public abstract class Enemy : Character
{
    public string EnemyType;

    public Enemy()
    {
        Image = new Image();
        Image.TexturePath = "peasant";
        SpawnPos = Helpers.GenerateRandomPosBelow(400);
        Position = SpawnPos;
        Speed = 60;
        Velocity = Vector2.Zero;
        MovementPattern = new RandomMovementPattern(4000);
        HitBox = new HitBox(60, 60, Position);
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void UnloadContent()
    {
        base.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public override void OnCollision(ICollidable other)
    {
        if (other is Projectile projectile && projectile.Parent != this && !(projectile.Parent is Enemy))
        {
            HP -= projectile.DMG;
            other.IsActive = false;
        }
    }
}