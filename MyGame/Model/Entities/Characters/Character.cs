using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Models;
using MyGame.Model.MovementPatterns;
using MyGame.Utilities;
using MyGame.View.Visuals;
using MyGame.Interfaces;
using MyGame.Model.Attacks;

namespace MyGame.Model.Entities.Characters;

public abstract class Character : ICollidable
{
    public bool IsActive { get; set; } = true;
    [XmlIgnore]
    public HitBox HitBox { get; set; }
    public Vector2 Position { get; set; }
    public Image Image;
    public float Speed { get; set; }
    public AttackManager AttackManager;
    public Vector2 Velocity;
    [XmlIgnore]
    public Vector2 SpawnPos;
    [XmlIgnore]
    public MovementPattern MovementPattern;
    protected bool reverseDir = false;
    public int HP;


    public Character()
    {
        AttackManager = new AttackManager();
        SpawnPos = Helpers.GenerateRandomPosBelow(400);
        Position = SpawnPos;
        Speed = 60;
        Velocity = Vector2.Zero;
        MovementPattern = new RandomMovementPattern(4000);
        HitBox = new HitBox(60, 60, Position);
        HP = 10;
    }

    public virtual void LoadContent()
    {
        Image.LoadContent();
        AttackManager.LoadContent(this);
        UpdateHitBox();
    }

    public virtual void UnloadContent()
    {
        Image.UnloadContent();
        AttackManager.UnloadContent();
    }

    public virtual void Update(GameTime gameTime)
    {
        Move(gameTime);
        Shoot(gameTime);
        Image.Update(gameTime);
        AttackManager.Update(gameTime);
        UpdateHitBox();
        CheckDead();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Image.Position = Position;
        Image.Draw(spriteBatch);
        AttackManager.Draw(spriteBatch);
    }

    public virtual void OnCollision(ICollidable other)
    {
        if (other is Projectile projectile && projectile.Parent != this)
        {
            HP -= projectile.DMG;
            other.IsActive = false;
        }
    }

    public virtual void UpdateHitBox()
    {
        HitBox.Update(Position + new Vector2(Image.GetWidth() / 2 - HitBox.Box.X / 2, Image.GetHeight() / 2 - HitBox.Box.X / 2));
    }

    public virtual void Shoot(GameTime gameTime)
    {
    }

    public virtual void Move(GameTime gameTime)
    {
        Velocity = MovementPattern.GetMoveVector(Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (reverseDir)
        {
            Velocity = -Velocity;
        }

        Vector2 offset = Vector2.Zero;
        if (Image != null)
            offset = new Vector2(Image.GetWidth() / 2, Image.GetWidth() / 2);

        // Calculate the next position after applying velocity
        Vector2 nextPosition = Position + Velocity + offset;

        // Check if the next position is out of the specified border or below Y=500
        if (Helpers.IsOutOfBorder(nextPosition) || nextPosition.Y > 500) // keep pos under 500
        {
            // Reverse the movement direction
            reverseDir = !reverseDir;
        }

        // Update the position
        Position += Velocity;
    }

    private void CheckDead()
    {
        if (HP <= 0)
        {
            HP = 0;
            IsActive = false;
        }
    }
}