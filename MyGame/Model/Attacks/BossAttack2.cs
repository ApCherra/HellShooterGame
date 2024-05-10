using System;
using System.ComponentModel;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Entities.Projectiles.ProjectileTypes;
using MyGame.Model.MovementPatterns;

namespace MyGame.Model.Attacks
{
    public class BossAttack2 : Attack
    {
        private float frequency;
        private double time;
        private Vector2 SourcePoint;
        private float expanseRate;
        private float radius;
        private float angle;
        private Vector2 convergentPoint1;
        private Vector2 convergentPoint2;
        private Vector2 convergentPoint3;
        private float angularVelo;
        private int numShots;

        public BossAttack2()
        {

        }

        public override void LoadContent(Character parent)
        {
            radius = 0f;
            SourcePoint = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 - radius, parent.HitBox.Box.Y / 2);
            convergentPoint1 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2 + 300f);
            convergentPoint2 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 - 200f, parent.HitBox.Box.Y / 2 - 300f);
            convergentPoint3 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 + 200f, parent.HitBox.Box.Y / 2 - 300f);
            frequency = 200f;
            time = frequency;
            expanseRate = 6f;
            numShots = 50;
            angle = 0f;
            angularVelo = 65f;
            base.LoadContent(parent);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            time -= gameTime.ElapsedGameTime.TotalMilliseconds;
            UpdateSourcePositions(gameTime);
            ContinueAttack();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void CreateAttack(Character parent)
        {
            projectiles.Add(NewProjectile(SourcePoint, new Vector2(0, 1)));

            numShots--;
        }

        private void ContinueAttack()
        {
            if (numShots >= 0)
            {
                if (time <= 0)
                {
                    radius += expanseRate;
                    time = frequency;
                    IsActive = true;
                    projectiles.Add(NewProjectile(SourcePoint, convergentPoint1));
                    projectiles.Add(NewProjectile(SourcePoint, convergentPoint2));
                    projectiles.Add(NewProjectile(SourcePoint, convergentPoint3));
                    numShots--;
                }
            }
        }

        private void UpdateSourcePositions(GameTime gameTime)
        {
            // Calculate the center of the parent's hitbox
            Vector2 center = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2);

            // Increment the angle by the angular velocity to rotate over time
            angle += angularVelo * (float)gameTime.ElapsedGameTime.TotalSeconds; // This needs gameTime passed in or calculated elsewhere
            float radAngle = angle * (float)Math.PI / 180.0f;  // Converting degrees to radians

            // Offset point before rotation (considered with expanded radius)
            Vector2 offsetPoint = new Vector2(radius, 0);

            // Apply rotation to the offset points
            SourcePoint = RotatePoint(offsetPoint, radAngle) + center;

            // Convergent points remain unchanged in rotation
            convergentPoint1 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2 + 300f);
            convergentPoint2 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 - 200f, parent.HitBox.Box.Y / 2 - 200f);
            convergentPoint3 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 + 200f, parent.HitBox.Box.Y / 2 - 200f);
        }

        // Helper method to rotate a point around the origin (0,0) by a given angle
        private Vector2 RotatePoint(Vector2 point, float a)
        {
            return new Vector2(
                point.X * (float)Math.Cos(a) - point.Y * (float)Math.Sin(a),
                point.X * (float)Math.Sin(a) + point.Y * (float)Math.Cos(a)
            );
        }

        private Vector2 GetDirectionVect(Vector2 p1, Vector2 p2)
        {
            Vector2 direction = p2 - p1;
            return Vector2.Normalize(direction);
        }

        private Projectile NewProjectile(Vector2 pos, Vector2 convergePoint)
        {
            Projectile proj = new Bullet()
            {
                MovementPattern = new ConstantMovementPattern(GetDirectionVect(pos, convergePoint)),
                Parent = parent,
                Position = pos,
                Speed = 75
            };

            proj.Image.TexturePath = "GreenBullet";
            proj.Image.Scale = new Vector2(0.06f, 0.06f);
            proj.HitBox.Box = new Vector2(15, 15);
            if (!proj.Loaded)
                proj.LoadContent();
            CollidablesManager.Instance.AddCollidable(proj);
            return proj;
        }
    }
}