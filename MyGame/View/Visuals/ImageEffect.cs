using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGame.View.Visuals
{
    public class ImageEffect
    {
        public bool IsActive;

        protected Image image;

        public ImageEffect() {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image Image) {
            image = Image;
        }

        public virtual void UnloadContent() {}

        public virtual void Update(GameTime gameTime) {}
    }
}