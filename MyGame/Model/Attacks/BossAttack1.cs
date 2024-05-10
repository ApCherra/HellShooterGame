using System;
using System.ComponentModel;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Entities.Projectiles.ProjectileTypes;
using MyGame.Model.MovementPatterns;

namespace MyGame.Model.Attacks
{
    public class BossAttack1 : Attack
    {
        private float frequency;
        private double time;
        private Vector2 SourcePoint1;
        private Vector2 SourcePoint2;
        private float expanseRate;
        private float radius;
        private float angle;
        private Vector2 convergentPoint;
        private float angularVelo;
        private int numShots;

        public BossAttack1()
        {

        }

        public override void LoadContent(Character parent)
        {
            radius = 25f;
            SourcePoint1 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 - radius, parent.HitBox.Box.Y / 2);
            SourcePoint2 = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2 + radius, parent.HitBox.Box.Y / 2);
            convergentPoint = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2 + 300f);
            frequency = 200f;
            time = frequency;
            expanseRate = 5f;
            numShots = 50;
            angle = 0f;
            angularVelo = 60f;
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
            ContinueAttack();
        }

        private void ContinueAttack()
        {
            if (numShots >= 0)
            {
                IsActive = true;
                if (time <= 0)
                {
                    radius += expanseRate;
                    time = frequency;
                    projectiles.Add(NewProjectile(SourcePoint1));
                    projectiles.Add(NewProjectile(SourcePoint2));
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

            // Offset points before rotation (considered with expanded radius)
            Vector2 offsetPoint1 = new Vector2(-radius, 0);
            Vector2 offsetPoint2 = new Vector2(radius, 0);

            // Apply rotation to the offset points
            SourcePoint1 = RotatePoint(offsetPoint1, radAngle) + center;
            SourcePoint2 = RotatePoint(offsetPoint2, radAngle) + center;

            // Convergent point remains unchanged in rotation
            convergentPoint = parent.HitBox.Position + new Vector2(parent.HitBox.Box.X / 2, parent.HitBox.Box.Y / 2 + 300f);
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

        private Projectile NewProjectile(Vector2 pos)
        {
            Projectile proj = new Bullet()
            {
                MovementPattern = new ConstantMovementPattern(GetDirectionVect(pos, convergentPoint)),
                Parent = parent,
                Position = pos,
                Speed = 75
            };

            proj.Image.TexturePath = "RedBullet";
            proj.Image.Scale = new Vector2(0.02f, 0.02f);
            proj.HitBox.Box = new Vector2(15, 15);
            if (!proj.Loaded)
                proj.LoadContent();
            CollidablesManager.Instance.AddCollidable(proj);
            return proj;
        }
    }
}