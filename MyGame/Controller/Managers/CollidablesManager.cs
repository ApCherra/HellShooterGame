using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGame.Interfaces;

namespace MyGame.Controller.Managers
{
    public class CollidablesManager
    {
        private static CollidablesManager _instance;
        public static CollidablesManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CollidablesManager();
                return _instance;
            }
        }

        private List<ICollidable> _collidables;

        public CollidablesManager()
        {
            _collidables = new List<ICollidable>();
        }

        public void Update() {
            RemoveInactive();
        }

        public void ClearCollidables()
        {
            _collidables.Clear();
        }

        public void ClearCollidablesOfType(string typeName)
        {
            Type targetType = Type.GetType(typeName); // Get the Type object for the specified typeName
            if (targetType == null)
            {
                // Invalid typeName
                return;
            }
            _collidables.RemoveAll(collidable => targetType.IsAssignableFrom(collidable.GetType()));
        }

        public List<ICollidable> GetAllCollidables()
        {
            return _collidables;
        }

        public List<ICollidable> GetCollidablesOfType(string typeName)
        {
            List<ICollidable> cols = _collidables
                .Where(c => c.GetType().Name == typeName)
                .ToList();
            return cols;
        }

        public List<ICollidable> GetCollidablesExceptType(string typeName)
        {
            List<ICollidable> cols = _collidables
                .Where(c => c.GetType().Name != typeName)
                .ToList();
            return cols;
        }

        public void AddCollidable(ICollidable collidable)
        {
            _collidables.Add(collidable);
        }

        public void RemoveCollidable(ICollidable collidable)
        {
            _collidables.Remove(collidable);
        }

        private void RemoveInactive()
        {
            _collidables.RemoveAll(collidable => !collidable.IsActive);
        }
    }
}