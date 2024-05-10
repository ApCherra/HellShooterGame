using MyGame.Controller;
using MyGame.Controller.Managers;
using MyGame.Interfaces;
using MyGame.Model.Entities.Characters;
using MyGame.Utilities;

namespace MyGame.View.Screens;

public class GameplayScreen : GameScreen
{
    GameManager gameManager;
    public GameplayScreen() : base()
    {
        gameManager = new GameManager();
    }   

    public override void LoadContent()
    {
        // Load gameplay screen-related content here
        base.LoadContent();
        Image.LoadContent();
        gameManager.LoadContent();
    }

    public override void UnloadContent()
    {
        // Unload gameplay screen content
        base.UnloadContent();
        Image.UnloadContent();
        gameManager.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
        // Update gameplay screen-related objects here
        base.Update(gameTime);
        Image.Update(gameTime);
        gameManager.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Draw gameplay screen-related objects here
        base.Draw(spriteBatch); 
        Image.Draw(spriteBatch);
        gameManager.Draw(spriteBatch);
    }
}