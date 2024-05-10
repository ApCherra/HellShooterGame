using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Interfaces;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Models;
using MyGame.Model.MovementPatterns;
using MyGame.Utilities;
using MyGame.View.Visuals;

namespace MyGame.Model.Entities.Projectiles;


public abstract class Projectile : ICollidable
{
    public Character Parent;
    public bool IsActive { get; set; }
    public Vector2 Position { get; set; }
    public Image Image;
    public float Speed { get; set; }
    [XmlIgnore]
    public Vector2 Velocity;
    [XmlIgnore]
    public HitBox HitBox { get; set; }
    public MovementPattern MovementPattern;
    public int DMG;
    [XmlIgnore]
    public bool Loaded;

    public Projectile()
    {
        IsActive = true;
        Image = new Image();
        Image.TexturePath = "projectile";
        Speed = 300;
        Velocity = Vector2.Zero;
        MovementPattern = new ConstantMovementPattern(new Vector2(0, -1));
        HitBox = new HitBox(1, 1, Position); // Default
        DMG = 1;
        Loaded = false;
    }

    public virtual void LoadContent()
    {
        Image.LoadContent();
        Loaded = true;
        UpdateHitBox();
    }

    public virtual void UnloadContent() {
        Image.UnloadContent();
    }

    public virtual void Update(GameTime gameTime)
    {
        Move(gameTime);
        Image.Update(gameTime);
        UpdateHitBox();
        CheckOutOfBorder();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Image.Position = Position;
        Image.Draw(spriteBatch);
    }

    public virtual void OnCollision(ICollidable other)
    {
        
    }

    public virtual void UpdateHitBox() {
        HitBox.Update(Position + new Vector2(Image.GetWidth() / 2, Image.GetHeight() / 2));
    }

    private void CheckOutOfBorder() {
        if(Helpers.IsOutOfProjectileBorder(Position + new Vector2(HitBox.Box.X / 2, HitBox.Box.Y / 2))) {
            IsActive = false;
        }
    }

    private void Move(GameTime gameTime) {
        Velocity = MovementPattern.GetMoveVector(Speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
        Position += Velocity;
    }
}