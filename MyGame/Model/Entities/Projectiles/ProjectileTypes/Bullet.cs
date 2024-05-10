using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Interfaces;
using MyGame.Model.Models;
using MyGame.Model.MovementPatterns;
using MyGame.View.Visuals;

namespace MyGame.Model.Entities.Projectiles.ProjectileTypes
{
    public class Bullet : Projectile
    {
        public Bullet()
        {
            IsActive = true;
            Image = new Image();
            Image.TexturePath = "Star";
            Image.Scale = new Vector2(0.01f, 0.01f);
            Velocity = Vector2.Zero;
            MovementPattern = new ConstantMovementPattern(new Vector2(0, -1));
            HitBox = new HitBox(24, 24, Position); // Default
            DMG = 5;
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

        public override void UpdateHitBox()
        {
            HitBox.Update(Position + new Vector2(Image.GetWidth() / 2 - HitBox.Box.X / 2, Image.GetHeight() / 2 - HitBox.Box.Y / 2));
            
        }

        public override void OnCollision(ICollidable other)
        {
            base.OnCollision(other);
        }
    }
}