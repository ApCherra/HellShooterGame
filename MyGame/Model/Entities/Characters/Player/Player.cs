using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Models;
using MyGame.Utilities;
using MyGame.View.Visuals;
using MyGame.Interfaces;
using System;
using MyGame.Model.Entities.Projectiles.ProjectileTypes;
using MyGame.Model.Attacks;

namespace MyGame.Model.Entities.Characters;

public class Player : Character
{
    public int Lives;

    public int Bombs = 3;
    private readonly Vector2 _spawnPos = new Vector2(Constants.ScreenWidth / 2, Constants.ScreenHeight / 2 + 150);
    private float invincibleTime; // Used when respawning
    bool cheatsOn;


    public Player()
    {
        Position = _spawnPos;
        Speed = 100;
        Velocity = Vector2.Zero;
        HitBox = new HitBox(30, 30, Position); // Default
        Lives = 5;
        HP = 1;
        cheatsOn = false;
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
        invincibleTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        Cheats();
        CheckLossLife();

        if (InputManager.Instance.KeyPressed(Keys.O)) //o for speed up
        {
            IncreaseSpeed(25);
        }
        if (InputManager.Instance.KeyPressed(Keys.L)) //l for speed down
        {
            DecreaseSpeed(25);
        }

        if (InputManager.Instance.KeyPressed(Keys.B) && Bombs > 0) //b for bomb
        {
            UseBomb();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

    }

    public override void OnCollision(ICollidable other)
    {
        base.OnCollision(other);
    }

    public override void Move(GameTime gameTime)
    {
        // Player movement
        if (InputManager.Instance.KeyDown(Keys.Down, Keys.S)) // Down
        {
            Velocity.Y = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (InputManager.Instance.KeyDown(Keys.Up, Keys.W)) // Up
        {
            Velocity.Y = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            Velocity.Y = 0;
        }

        if (InputManager.Instance.KeyDown(Keys.Right, Keys.D)) // Right
        {
            Velocity.X = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (InputManager.Instance.KeyDown(Keys.Left, Keys.A)) // Left
        {
            Velocity.X = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            Velocity.X = 0;
        }
        Vector2 offset = Vector2.Zero;
        if (Image != null)
        {
            offset = new Vector2(Image.SourceRect.Width * Image.Scale.X / 2, Image.SourceRect.Height * Image.Scale.X / 2);
        }
        if (!Helpers.IsOutOfBorder(Position + Velocity + offset))
            Position += Velocity;
    }

    public override void Shoot(GameTime gameTime)
    {
        if (InputManager.Instance.KeyPressed(Keys.Space))
        {
            Attack attack = new SingleShot();
            attack.LoadContent(this);
            AttackManager.Attacks.Add(attack);
        }
    }


    public void Cheats()
    {
        if (InputManager.Instance.KeyPressed(Keys.C))
            cheatsOn = !cheatsOn;
            
        if (cheatsOn)
        {
            if (invincibleTime <= 0)
                invincibleTime = 1;
            IsActive = true;
            HP = 1;
        }
    }

    private void UseBomb()
    {
        Bombs--;
        Attack bombAttack = new BombAttack();
        bombAttack.LoadContent(this);
        CollidablesManager.Instance.ClearCollidablesOfType("Projectile");
        AttackManager.Attacks.Add(bombAttack);
        invincibleTime = 10000f;
    }

    private void CheckLossLife()
    {
        if (invincibleTime <= 0)
        {
            invincibleTime = 0;
            if (!IsActive)
            {
                if (Lives - 1 > 0)
                {
                    Lives--;
                    Respawn();
                }
                else
                {
                    ScreenManager.Instance.ChangeScreen("LoseScreen");
                }
            }
        }
        else
        {
            HitBox = new HitBox(30, 30, Position + new Vector2(Image.GetWidth() / 2 - HitBox.Box.X / 2, Image.GetHeight() / 2 - HitBox.Box.X / 2));
            IsActive = true;
        }
    }

    private void Respawn()
    {
        IsActive = true;
        Velocity = Vector2.Zero;
        Position = _spawnPos;
        HitBox = new HitBox(30, 30, Position);
        HP = 1;
        invincibleTime = 2000f;
        CollidablesManager.Instance.ClearCollidablesOfType("Projectile");
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
        if (Speed > 175) Speed = 175;
    }

    public void DecreaseSpeed(float amount)
    {
        Speed -= amount;
        if (Speed < 100) Speed = 100;
    }

}