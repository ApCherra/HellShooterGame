using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Controller.Managers;

namespace MyGame.View.Screens
{
public class LoadScreen : GameScreen
    {
        public override void LoadContent()
        {
            // Initialize gameplay-related objects here
            base.LoadContent();
            Image.LoadContent();
        }

        public override void UnloadContent()
        {
            // Load gameplay-related content here
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Update gameplay-related objects here
            base.Update(gameTime);
            Image.Update(gameTime);
            // Temp changer
            if (InputManager.Instance.KeyPressed(Keys.Enter))
                ScreenManager.Instance.ChangeScreen("MenuScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw gameplay-related objects here
            base.Draw(spriteBatch);
            Image.Draw(spriteBatch);
        }
    }
}