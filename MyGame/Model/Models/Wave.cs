using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyGame.Controller;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Characters.Enemies;

namespace MyGame.Model.Models
{
    public class Wave
    {
        [XmlElement("Boss", typeof(Boss))]
        [XmlElement("Noble", typeof(Noble))]
        [XmlElement("Peasant", typeof(Peasant))]
        public List<Enemy> Enemies;
        public float WaveTime;
        public bool IsActive;
        public bool AllCharactersDead;
        public bool WaveTimeEnded;
        [XmlIgnore]
        public bool Loaded;

        public Wave()
        {
            // Default values
            WaveTime = 20.0f;
            Enemies = new List<Enemy>();
            IsActive = false;
            AllCharactersDead = false;
            WaveTimeEnded = false;
            Loaded = false;
        }

        public void LoadContent()
        {
            Loaded = true;
            IsActive = true;
            foreach (Enemy enemy in Enemies)
            {
                enemy.LoadContent();
                CollidablesManager.Instance.AddCollidable(enemy);
            }
        }

        public void UnloadContent()
        {
            foreach (Enemy enemy in Enemies)
                enemy.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            RemoveInactive();
            foreach (Enemy enemy in Enemies)
                enemy.Update(gameTime);
            UpdateTimer(gameTime);
            CheckActive();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
                enemy.Draw(spriteBatch);
        }

        private void RemoveInactive()
        {
            foreach (var enemy in Enemies.ToList())
            {
                if (!enemy.IsActive)
                {
                    enemy.UnloadContent();
                    Enemies.Remove(enemy);
                }
            }
        }

        private void UpdateTimer(GameTime gameTime)
        {
            WaveTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (WaveTime <= 0.0f)
            {
                WaveTime = 0.0f;
                WaveTimeEnded = true;
            }
        }

        private void CheckActive()
        {
            if (Enemies.Count == 0)
                AllCharactersDead = true;

            if (AllCharactersDead) // Redundancy for use of all characters dead and wave time end
                IsActive = false;
        }
    }
}