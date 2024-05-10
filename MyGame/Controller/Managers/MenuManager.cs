using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Model.Models;
using MyGame.Utilities;

namespace MyGame.Controller.Managers
{
    public class MenuManager
    {
        private Menu _menu;
        private bool _isTransitioning;

        private void Transition(GameTime gameTime)
        {
            if (_isTransitioning)
            {
                for (int i = 0; i < _menu.Items.Count; i++)
                {
                    _menu.Items[i].Image.Update(gameTime);
                    float first = _menu.Items[0].Image.Alpha;
                    float last = _menu.Items[_menu.Items.Count - 1].Image.Alpha;
                    if (first == 0.0f && last == 0.0f)
                        _menu.ID = _menu.Items[_menu.ItemNumber].LinkID;
                    else if (first == 1.0f && last == 1.0f)
                    {
                        _isTransitioning = false;
                        foreach (MenuItem item in _menu.Items)
                            item.Image.RestoreEffects();
                    }
                }
            }
        }

        private void menu_OnMenuChange(object sender, EventArgs e)
        {
            if (_menu.ID != null)
            {
                XmlManager<Menu> xmlMenuManager = new XmlManager<Menu>();
                _menu.UnloadContent();
                // Transition
                if (!File.Exists(_menu.ID))
                    _menu = xmlMenuManager.Load(Helpers.GetPath(_menu.ID));
                else
                    _menu = xmlMenuManager.Load(_menu.ID);
                _menu.LoadContent();
                _menu.OnMenuChange += menu_OnMenuChange;
                _menu.Transition(0.0f);

                foreach (MenuItem item in _menu.Items)
                {
                    item.Image.StoreEffects();
                    item.Image.ActivateEffect("FadeEffect");
                }
            }
        }

        public MenuManager()
        {
            _menu = new Menu();
            _menu.OnMenuChange += menu_OnMenuChange;
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != String.Empty)
                _menu.ID = menuPath;
        }

        public void UnloadContent()
        {
            _menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (!_isTransitioning)
                _menu.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.Enter) && !_isTransitioning)
            {
                if (_menu.Items[_menu.ItemNumber].Image.Text == "Exit")
                {
                    ScreenManager.Instance.End = true;
                }
                else
                {
                    if (_menu.Items[_menu.ItemNumber].LinkType == "Screen")
                        ScreenManager.Instance.ChangeScreen(_menu.Items[_menu.ItemNumber].LinkID);
                    else
                    {
                        _isTransitioning = true;
                        _menu.Transition(1.0f);
                        foreach (MenuItem item in _menu.Items)
                        {
                            item.Image.StoreEffects();
                            item.Image.ActivateEffect("FadeEffect");
                        }
                    }
                }
            }
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _menu.Draw(spriteBatch);
        }
    }
}