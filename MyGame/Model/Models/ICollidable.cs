using MyGame.Model.Models;

namespace MyGame.Interfaces;

public interface ICollidable
{
    public HitBox HitBox { get; }
    bool IsActive { get; set; }
    void OnCollision(ICollidable other);
}