using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Entities.Projectiles.ProjectileTypes;
using MyGame.Model.MovementPatterns;

namespace MyGame.Model.Attacks
{
    public class Scatter : Attack
    {
        public int NumProjectiles;
        private int currentDirectionIndex;
        private float angleIncrement;

        public Scatter()
        {
            NumProjectiles = 8; // Default
            currentDirectionIndex = 0;
            angleIncrement = MathHelper.TwoPi / NumProjectiles;
        }


        public override void LoadContent(Character parent)
        {
            base.LoadContent(parent);
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

        public override void CreateAttack(Character parent)
        {
            // Iterate to create each projectile
            for (int i = 0; i < NumProjectiles; i++)
            {
                // Calculate the direction vector based on the angle
                Vector2 direction = NextDirectionVector();

                // Create a new projectile with a constant movement pattern in the calculated direction
                Projectile proj = new Bullet()
                {
                    MovementPattern = new ConstantMovementPattern(direction),
                    Parent = parent,
                    Position = parent.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2)
                };

                // Add the projectile to the list
                projectiles.Add(proj);
            }
        }

        public Vector2 NextDirectionVector()
        {
            // Calculate the angle for the current direction
            float angle = currentDirectionIndex * angleIncrement;

            // Calculate the direction vector based on the angle
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

            // Increment the direction index
            currentDirectionIndex++;
            if (currentDirectionIndex >= NumProjectiles)
            {
                currentDirectionIndex = 0; // Reset back to start
            }

            return direction;
        }
    }
}