using System;
using System.IO;
using MyGame.Controller.Managers;
using MyGame.Interfaces;
using MyGame.Utilities;

namespace MyGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Color defaultBackgroundColor = new Color(80, 150, 235); // Default background color here

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        String path = Helpers.GetPath("DesktopGL");
        Content.RootDirectory = path;
        IsMouseVisible = true;
    }

    /// <summary>
    /// Intialize game
    /// <summary>
    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
        _graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
        _graphics.ApplyChanges();
        base.Initialize();
    }

        /// <summary>
    /// Loads all game content
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        ScreenManager.Instance.GraphicsDevice = GraphicsDevice;
        ScreenManager.Instance.SpriteBatch = _spriteBatch;
        ScreenManager.Instance.LoadContent(Content);
    }

    /// <summary>
    /// Unloads all game content
    /// </summary>
    protected override void UnloadContent()
    {
        ScreenManager.Instance.UnloadContent();
    }

    /// <summary>
    /// Updates game according to all inputs and game states
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Update(GameTime gameTime)
    {
        // Allow game it end
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || ScreenManager.Instance.End)
            this.Exit();
        ScreenManager.Instance.Update(gameTime);
        base.Update(gameTime);
    }

    /// <summary>
    /// Draws current game state to screen
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(defaultBackgroundColor);
        _spriteBatch.Begin();
        ScreenManager.Instance.Draw(_spriteBatch);

        // _____________________ Draw hitboxes _________________________________
        // Texture2D pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        // pixelTexture.SetData(new[] { Color.White }); 
        // foreach(ICollidable c in CollidablesManager.Instance.GetAllCollidables()) 
        //     Helpers.DrawRectangle(c, _spriteBatch, pixelTexture, Color.White);
        // ______________________________________________________________________

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
