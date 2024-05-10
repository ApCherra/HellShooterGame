using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using MyGame.Controller.Managers;
using MyGame.Utilities;

namespace MyGame.View.Visuals
{
    public class Image
    {
        public bool IsActive;
        public float Alpha, Rotation;
        public string Text, FontPath, TexturePath;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;

        [XmlIgnore]
        public Texture2D Texture;

        public Color ContentColor;
        public string Effects;
        public FadeEffect FadeEffect;

        private Vector2 _origin;
        private ContentManager _content;
        private RenderTarget2D _renderTarget;
        private SpriteFont _font;
        private Dictionary<string, ImageEffect> _effectList;

        private void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            _effectList.Add(effect.GetType().ToString().Replace("MyGame.View.Visuals.", ""),
                            (effect as ImageEffect));
        }

        public void ActivateEffect(string effect)
        {
            if (_effectList.ContainsKey(effect))
            {
                _effectList[effect].IsActive = true;
                var obj = this;
                _effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (_effectList.ContainsKey(effect))
            {
                _effectList[effect].IsActive = false;
                _effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects = String.Empty;
            foreach (var effect in _effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }
            if (Effects != String.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        public void RestoreEffects()
        {
            foreach (var effect in _effectList)
                DeactivateEffect(effect.Key);

            string[] split = Effects.Split(':');
            foreach (string s in split)
                ActivateEffect(s);
        }

        public Image()
        {
            IsActive = true;
            TexturePath = Text = Effects = String.Empty;
            FontPath = "MenuFont"; // Menu font is default font
            ContentColor = Color.White;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            Rotation = 0.0f;
            SourceRect = Rectangle.Empty;
            _effectList = new Dictionary<string, ImageEffect>();
            _origin = Vector2.Zero;
        }

        public void LoadContent()
        {
            _content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, Constants.ContentPath);

            if (TexturePath != String.Empty)
                Texture = _content.Load<Texture2D>(TexturePath);

            _font = _content.Load<SpriteFont>(FontPath);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += _font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = dimensions.Y = Math.Max(Texture.Height, _font.MeasureString(Text).Y);
            else
                dimensions.Y = _font.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            _renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                            (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(_renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, ContentColor);
            ScreenManager.Instance.SpriteBatch.DrawString(_font, Text, Vector2.Zero, ContentColor);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = _renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            // Add effects
            SetEffect<FadeEffect>(ref FadeEffect);

            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(":");
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public void UnloadContent()
        {
            _content.Unload();
            foreach (var effect in _effectList)
                DeactivateEffect(effect.Key);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in _effectList)
                if (effect.Value.IsActive)
                    effect.Value.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Text != string.Empty)
            {
                _origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
                spriteBatch.Draw(Texture, Position + _origin, SourceRect, ContentColor * Alpha,
                                Rotation, _origin, Scale, SpriteEffects.None, 0.0f);
            }
            else
            {
                // Calculate the adjusted position based on the scale
                Vector2 adjustedPosition = Position - new Vector2(SourceRect.Width * Scale.X * _origin.X, SourceRect.Height * Scale.Y * _origin.Y);
                spriteBatch.Draw(Texture, adjustedPosition, SourceRect, ContentColor * Alpha,
                                Rotation, _origin, Scale, SpriteEffects.None, 0.0f);
            }
        }

        public float GetWidth()
        {
            return SourceRect.Width * Scale.X;
        }

        public float GetHeight()
        {
            return SourceRect.Height * Scale.Y;
        }
    }
}