namespace MyGame.Controller;

using MyGame.Model.Models;
using MyGame.Interfaces;
using System.Runtime.CompilerServices;
using MyGame.Controller.Managers;

public class CollisionManager
{
    private static CollisionManager _instance;
    public static CollisionManager Instance {
        get {
            if(_instance == null)
                _instance = new CollisionManager();
            return _instance;
        }
    }
    
    public List<Collision> Collisions;

    public CollisionManager(){
        Collisions = new List<Collision>();
    }

    public void Update(List<ICollidable> collidables) {
        CheckCollisions(collidables);
        CollidablesManager.Instance.Update();
    }

    private void CheckCollisions(List<ICollidable> collidables)
    {
        for (int i = 0; i < collidables.Count; i++)
        {
            for (int j = i + 1; j < collidables.Count; j++)
            {
                if (IsColliding(collidables[i], collidables[j]))
                {
                    Collisions.Add(new Collision(collidables[i], collidables[j]));
                    collidables[i].OnCollision(collidables[j]);
                    collidables[j].OnCollision(collidables[i]);
                }
            }
        }
    }

    private bool IsColliding(ICollidable a, ICollidable b)
    {
        return a.HitBox.Intersects(b.HitBox);
    }
}