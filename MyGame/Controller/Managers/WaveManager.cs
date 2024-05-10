using System.Data;
using System.Xml.Serialization;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Characters.Enemies;
using MyGame.Model.Models;

namespace MyGame.Controller.Managers;

public class WaveManager
{
    [XmlElement("Wave")]
    public List<Wave> Waves;


    public WaveManager()
    {
    }

    public void LoadContent()
    {
        if (Waves.Count > 0)
            Waves[0].LoadContent(); // Load first wave
    }

    public void UnloadContent()
    {
        for (int waveIndex = 0; waveIndex < Waves.Count; waveIndex++) // Unload any remaining unused waves
            if (Waves[waveIndex].Loaded)
                Waves[waveIndex].UnloadContent();
        Waves.Clear();
        CollidablesManager.Instance.ClearCollidables();
    }

    public void Update(GameTime gameTime)
    {
        if (Waves.Count > 0)
            Waves[0].Update(gameTime);
        NextWave();
        CheckWin();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Waves.Count > 0)
            Waves[0].Draw(spriteBatch);
    }

    private void NextWave()
    {
        if (Waves.Count > 0)
            if (!Waves[0].IsActive)
            {
                Waves[0].UnloadContent();
                Waves.Remove(Waves[0]);
                if (Waves.Count > 0)
                    Waves[0].LoadContent();
            }
    }

    private void CheckWin()
    {
        if (Waves.Count == 0)
            ScreenManager.Instance.ChangeScreen("WinScreen");
    }
}