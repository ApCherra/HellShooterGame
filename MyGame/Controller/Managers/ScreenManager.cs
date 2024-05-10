using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.View.Visuals;
using MyGame.View.Screens;
using System.IO;
using MyGame.Utilities;

namespace MyGame.Controller.Managers;

/// <summary>
/// Class object for managing the screen
/// </summary>
public class ScreenManager
{
    [XmlIgnore]
    public Vector2 Dimensions { private set; get; } // Screen dimensions
    [XmlIgnore]
    public ContentManager Content { private set; get; }
    [XmlIgnore]
    public GraphicsDevice GraphicsDevice;
    [XmlIgnore]
    public SpriteBatch SpriteBatch;
    public XmlManager<GameScreen> XmlGameScreenManager;
    public Image TransitionImage;
    [XmlIgnore]
    public bool IsTransitioning { get; private set; }
    [XmlIgnore]
    public bool End;

    private GameScreen currentScreen, newScreen;


    // Singleton object to avoid bad communication errors
    private static ScreenManager _instance;
    public static ScreenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                _instance = xml.Load(Helpers.GetPath("ScreenManager.xml"));
            }
            return _instance;
        }
    }

    public ScreenManager()
    {
        Dimensions = new Vector2(Constants.ScreenWidth, Constants.ScreenHeight);
        currentScreen = new LoadScreen();
        if (File.Exists(currentScreen.XmlPath))
        {
            XmlGameScreenManager = new XmlManager<GameScreen>();
            XmlGameScreenManager.Type = currentScreen.Type;
            String path = Helpers.GetPath("LoadScreen.xml");
            currentScreen = XmlGameScreenManager.Load(path);
        }
    }

    public void ChangeScreen(string screenName)
    {
        if (!IsTransitioning)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MyGame.View.Screens." + screenName));
            TransitionImage.IsActive = true;
            TransitionImage.FadeEffect.Increase = true;
            TransitionImage.Alpha = 0.0f;
            IsTransitioning = true;
            End = false;
        }
    }

    public void LoadContent(ContentManager Content)
    {
        this.Content = new ContentManager(Content.ServiceProvider, Constants.ContentPath);
        currentScreen.LoadContent();
        TransitionImage.LoadContent();
    }

    public void UnloadContent()
    {
        currentScreen.UnloadContent();
        TransitionImage.UnloadContent();
    }

    public void Update(GameTime gameTime)
    {
        currentScreen.Update(gameTime);
        Transition(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentScreen.Draw(spriteBatch);
        if (IsTransitioning)
            TransitionImage.Draw(spriteBatch);
    }

    private void Transition(GameTime gameTime)
    {
        if (IsTransitioning)
        {
            TransitionImage.Update(gameTime);
            if (TransitionImage.Alpha == 1.0f)
            {
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                XmlGameScreenManager.Type = currentScreen.Type;
                if (File.Exists(currentScreen.XmlPath))
                    currentScreen = XmlGameScreenManager.Load(currentScreen.XmlPath);
                currentScreen.LoadContent();
            }
            else if (TransitionImage.Alpha == 0.0f)
            {
                TransitionImage.IsActive = false;
                IsTransitioning = false;
            }
        }
    }
}
