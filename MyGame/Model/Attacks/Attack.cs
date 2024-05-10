using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Controller.Managers;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Projectiles;

namespace MyGame.Model.Attacks
{
    public abstract class Attack
    {
        public bool IsActive;
        public bool Loaded;
        protected List<Projectile> projectiles;
        protected Character parent;

        public Attack() {
            projectiles = new List<Projectile>();
        }

        public virtual void LoadContent(Character parent)
        {
            IsActive = true;
            Loaded = true;
            this.parent = parent;
            CreateAttack(parent);
            foreach(var proj in projectiles) {
                if(!proj.Loaded)
                    proj.LoadContent();
                CollidablesManager.Instance.AddCollidable(proj);
            }
        }

        public virtual void UnloadContent()
        {
            Loaded = false;
            foreach(var proj in projectiles) {
                proj.UnloadContent();
                CollidablesManager.Instance.RemoveCollidable(proj);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            RemoveInactive();
            foreach(var proj in projectiles) 
                proj.Update(gameTime);
            CheckActive();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(var proj in projectiles)
                proj.Draw(spriteBatch);
        }

        public abstract void CreateAttack(Character parent);

        private void RemoveInactive()
        {
            foreach (var proj in projectiles.ToList())
            {
                if (!proj.IsActive)
                {
                    proj.UnloadContent();
                    projectiles.Remove(proj);
                }
            }
        }

        protected void CheckActive() {
            if(projectiles.Count == 0) 
                IsActive = false;
        }
    }
}