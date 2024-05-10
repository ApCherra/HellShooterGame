using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.View.Visuals;
using System.IO;
using MyGame.Utilities;

namespace MyGame.View.Screens;

public abstract class GameScreen
{
    public Image Image;

    [XmlIgnore]
    public Type Type;
    public string XmlPath;

    protected ContentManager content;

    public GameScreen()
    {
        Type = this.GetType();
        var path =  Helpers.GetPath("GameSettings");
        XmlPath = Path.Combine(path, Type.ToString().Replace("MyGame.View.Screens.", "") + ".xml");
    }

    public virtual void LoadContent()
    {
        content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, Constants.ContentPath);
    }
    
    public virtual void UnloadContent()
    {
        content.Unload();
    }

    public virtual void Update(GameTime gameTime)
    {
        InputManager.Instance.Update();

        // Exit game on input
        if (InputManager.Instance.KeyPressed(Keys.Escape))
            ScreenManager.Instance.ChangeScreen("MenuScreen");
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {

    }
}