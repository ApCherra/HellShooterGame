using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Controller.Managers;
using MyGame.Utilities;

namespace MyGame.View.Screens
{
    public class WinScreen : GameScreen
    {
        protected MenuManager _menuManager;

        public WinScreen() : base()
        {
            _menuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            String path = Helpers.GetPath("WinMenu.xml");
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
}