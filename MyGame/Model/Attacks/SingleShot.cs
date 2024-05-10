

using System.Data.Common;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Entities.Projectiles.ProjectileTypes;
using MyGame.Model.MovementPatterns;

namespace MyGame.Model.Attacks
{
    public class SingleShot : Attack
    {
        public Vector2 Direction;
        public SingleShot()
        {
            Direction = new Vector2(0, -1); // Default
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
            Projectile proj = new Bullet()
            {
                MovementPattern = new ConstantMovementPattern(Direction),
                Parent = parent,
                Position = parent.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2)
            };
            if (parent is Player) {
                proj.Image.TexturePath = "PlayerBullet";
                proj.Image.Scale = new Vector2(0.06f, 0.06f);
                proj.HitBox.Box = new Vector2(10, 35);
            }
            
            projectiles.Add(proj);
        }
    }
}