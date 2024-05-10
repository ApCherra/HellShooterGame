using System.Linq;
using System.Xml.Serialization;
using MyGame.Controller.Managers;
using MyGame.View.Visuals;
using System.IO;
using System;
using MyGame.Utilities;

namespace MyGame.View.Screens;

public class MenuScreen : GameScreen
{
    protected MenuManager _menuManager;

    public MenuScreen() : base()
    {   
        _menuManager = new MenuManager();
    }

    public override void LoadContent()
    {
        base.LoadContent();
        String path = Helpers.GetPath("MainMenu.xml");
        _menuManager.LoadContent(path);
    }

    public override void UnloadContent()
    {
        base.UnloadContent();
        _menuManager.UnloadContent();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _menuManager.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _menuManager.Draw(spriteBatch);
    }
}