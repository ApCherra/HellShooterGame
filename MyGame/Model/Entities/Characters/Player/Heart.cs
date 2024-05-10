using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Projectiles;
using MyGame.Model.Models;
using MyGame.Model.MovementPatterns;
using MyGame.Utilities;
using MyGame.View.Visuals;
using MyGame.Interfaces;

namespace MyGame.Model.Entities.Characters;

public class Heart : Character
{
        //private readonly Vector2 _spawnPos = new Vector2(Constants.ScreenWidth / 2, Constants.ScreenHeight / 2 + 150);

    public readonly Vector2 heartPos = new Vector2(0, 625);


    public Heart()
    {
        Image = new Image();
        Image.TexturePath = "heart";
        //SpawnPos = Helpers.GenerateRandomPosBelow(100);
        Position = heartPos;

    }
    public override void Move(GameTime gameTime)
    {

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

    
}