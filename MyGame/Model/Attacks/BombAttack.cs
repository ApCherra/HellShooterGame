using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Entities.Projectiles.ProjectileTypes;
using MyGame.Model.MovementPatterns;


//inherit
namespace MyGame.Model.Attacks
{
    public class BombAttack : Attack
    {
        public int NumProjectiles;
        public float Offset;
        private int currentDirectionIndex;
        private float angleIncrement;

        public BombAttack()
        {
            Offset = 200;
            NumProjectiles = 15;
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
            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 direction = NextDirectionVector();

                Projectile proj = new Bullet()
                {
                    MovementPattern = new ConstantMovementPattern(direction),
                    Speed = 60,
                    Parent = parent,
                    Position = parent.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2 - Offset)
                };
                proj.Image.TexturePath = "Bomb";
                proj.Image.Scale = new Vector2(0.1f, 0.1f);
                proj.HitBox.Box = new Vector2(40, 40);

                projectiles.Add(proj);
            }
        }

        public Vector2 NextDirectionVector()
        {
            float angle = currentDirectionIndex * angleIncrement;

            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

            currentDirectionIndex++;
            if (currentDirectionIndex >= NumProjectiles)
            {
                currentDirectionIndex = 0; 
            }

            return direction;
        }
    }
}