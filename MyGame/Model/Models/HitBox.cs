using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyGame.Model.Models
{
    public class HitBox
    {
        public Vector2 Box;
        public Vector2 Position;
        public HitBox(float Width, float Height, Vector2 position)
        {
            Box = new Vector2(Width, Height);
            Position = position;
        }

        public void Update(Vector2 pos)
        {
            SetPosition(pos);
            // Other update logic
        }

        public void SetSize(float width, float height)
        {
            Box.X = width;
            Box.Y = height;
        }

        public bool Intersects(HitBox hitBox)
        {
            // Calculate the bounds of the current hitbox
            float left1 = Position.X;
            float right1 = Position.X + Box.X;
            float top1 = Position.Y;
            float bottom1 = Position.Y + Box.Y;

            // Calculate the bounds of the other hitbox
            float left2 = hitBox.Position.X;
            float right2 = hitBox.Position.X + hitBox.Box.X;
            float top2 = hitBox.Position.Y;
            float bottom2 = hitBox.Position.Y + hitBox.Box.Y;

            // Check for collision
            if (right1 >= left2 && left1 <= right2 && bottom1 >= top2 && top1 <= bottom2)
            {
                // Collision occurred
                return true;
            }
            else
            {
                // No collision
                return false;
            }
        }

        private void SetPosition(Vector2 pos) {
            Position = pos;
        }
    }
}