using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyGame.Controller.Managers;
using MyGame.View.Visuals;

namespace MyGame.Model.Models
{
    public class Menu
    {
        public event EventHandler OnMenuChange;
        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;
        public int Margin;

        private int itemNumber;
        public int ItemNumber
        {
            get { return itemNumber; }
        }

        private string _id;

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnMenuChange(this, null);
            }
        }

        private void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;
            bool first = true;
            foreach (MenuItem item in Items)
            {
                if (first)
                {
                    dimensions += new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height);
                    first = false;
                }
                if (Axis == "X")
                    dimensions += new Vector2(item.Image.SourceRect.Width + Margin, item.Image.SourceRect.Height);
                else if (Axis == "Y")
                    dimensions += new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height + Margin);
            }

            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2,
                                    (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);
            first = true;
            foreach (MenuItem item in Items)
            {
                if (Axis == "X")
                    item.Image.Position = new Vector2(dimensions.X,
                                    (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRect.Height) / 2);
                else if (Axis == "Y")
                    item.Image.Position = new Vector2((ScreenManager.Instance.Dimensions.X -
                                                        item.Image.SourceRect.Width) / 2, dimensions.Y);
                if (first)
                {
                    first = false;
                    dimensions += new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height);
                }
                if (Axis == "X")
                    dimensions += new Vector2(item.Image.SourceRect.Width + Margin, item.Image.SourceRect.Height);
                else if (Axis == "Y")
                    dimensions += new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height + Margin);
            }
        }

        public Menu()
        {
            Items = new List<MenuItem>();
            _id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
        }

        public void Transition(float alpha) {
            foreach(MenuItem item in Items) 
            {
                item.Image.IsActive = true;
                item.Image.Alpha = alpha;
                if (alpha == 0.0) 
                    item.Image.FadeEffect.Increase = true;
                else 
                    item.Image.FadeEffect.Increase = false;
            }
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(":");
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach (string s in split)
                    item.Image.ActivateEffect(s);
            }
            AlignMenuItems();
        }

        public void UnloadContent()
        {
            foreach (MenuItem item in Items)
                item.Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    itemNumber--;
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                    itemNumber--;
            }
            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > Items.Count - 1)
                itemNumber = Items.Count - 1;


            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                    Items[i].Image.IsActive = true;
                else
                    Items[i].Image.IsActive = false;
                Items[i].Image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in Items)
                item.Image.Draw(spriteBatch);
        }
    }
}