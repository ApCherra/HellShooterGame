using MyGame.Interfaces;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Models;
using MyGame.Utilities;

namespace MyGame.Controller.Managers;

public class GameManager {
    Player player;

    public Heart heart;

    public Speed100 speed100;
    public Speed125 speed125;
    public Speed150 speed150;
    public Speed175 speed175;


    public WaveManager WaveManager;

    public GameManager() {
        WaveManager = new WaveManager();

    }
    
    public void LoadContent()
    {
        XmlManager<Player> playerLoader = new XmlManager<Player>();
        player = playerLoader.Load(Helpers.GetPath("Player.xml"));
        player.LoadContent();

        CollidablesManager.Instance.AddCollidable(player); // Add the player to the list of collidables

        XmlManager<WaveManager> waveManagerLoader = new XmlManager<WaveManager>();
        WaveManager = waveManagerLoader.Load(Helpers.GetPath("WaveManager.xml"));
        WaveManager.LoadContent();

        XmlManager<Heart> heartLoader = new XmlManager<Heart>();
        heart = heartLoader.Load(Helpers.GetPath("Heart.xml"));
        heart.LoadContent();

        XmlManager<Speed100> speed100Loader = new XmlManager<Speed100>();
        speed100 = speed100Loader.Load(Helpers.GetPath("Speed100.xml"));
        speed100.LoadContent();

        XmlManager<Speed125> speed125Loader = new XmlManager<Speed125>();
        speed125 = speed125Loader.Load(Helpers.GetPath("Speed125.xml"));
        speed125.LoadContent();

        XmlManager<Speed150> speed150Loader = new XmlManager<Speed150>();
        speed150 = speed150Loader.Load(Helpers.GetPath("Speed150.xml"));
        speed150.LoadContent();

        XmlManager<Speed175> speed175Loader = new XmlManager<Speed175>();
        speed175 = speed175Loader.Load(Helpers.GetPath("Speed175.xml"));
        speed175.LoadContent();
    }

    public void UnloadContent()
    {
        player.UnloadContent();
        WaveManager.UnloadContent();
        heart.UnloadContent();
    }


    public void Update(GameTime gameTime)
    {
        player.Update(gameTime);
        WaveManager.Update(gameTime);
        heart.Update(gameTime);


        // Update collisions from all collidables in the game
        CollisionManager.Instance.Update(CollidablesManager.Instance.GetAllCollidables());
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        player.Draw(spriteBatch);
        WaveManager.Draw(spriteBatch);
        for(var i = 0; i < player.Lives; i++)
        {
        heart.Position = new Vector2(heart.Position.X + 15 * i, heart.Position.Y);

        heart.Draw(spriteBatch);

        heart.Position = heart.heartPos;

        }

        if(player.Speed == 100)
        {
            speed100.Draw(spriteBatch);
        }
        if(player.Speed == 125)
        {
            speed125.Draw(spriteBatch);
        }
        if(player.Speed == 150)
        {
            speed150.Draw(spriteBatch);
        }
        if(player.Speed == 175)
        {
            speed175.Draw(spriteBatch);
        }

        
    }
}