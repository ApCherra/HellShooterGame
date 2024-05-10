using System.Data;
using System.Xml.Serialization;
using MyGame.Model.Attacks;
using MyGame.Model.Entities.Characters;
using MyGame.Model.Entities.Characters.Enemies;
using MyGame.Model.Models;

namespace MyGame.Controller.Managers;

public class AttackManager
{
    [XmlElement("Attack")]
    [XmlElement("Scatter", typeof(Scatter))]
    [XmlElement("SingleShot", typeof(SingleShot))]
    [XmlElement("BombAttack", typeof(BombAttack))]
    [XmlElement("BossAttack1", typeof(BossAttack1))]
    [XmlElement("BossAttack2", typeof(BossAttack2))]
    public List<Attack> Attacks;
    public bool AutoShoot;
    private Character parent;
    public float Frequency;
    private float timeSinceLastAttack; // Keep track of time since last attack
    public bool Expires;
    private int attackIndex;

    public AttackManager()
    {
        Attacks = new List<Attack>();
        Frequency = 2000;
        AutoShoot = false;
        timeSinceLastAttack = 0; // Initialize time since last attack
        attackIndex = 0;
    }

    public void LoadContent(Character parent)
    {
        this.parent = parent;
        if (Attacks.Count > 0)
            Attacks[attackIndex].LoadContent(this.parent); // Load first attack
    }

    public void UnloadContent()
    {
        foreach (var attack in Attacks)
            if (attack.Loaded)
                attack.UnloadContent();
        Attacks.Clear();
    }

    public void Update(GameTime gameTime)
    {
        foreach (var attack in Attacks)
            if (attack.Loaded && attack.IsActive)
                attack.Update(gameTime);

        NextAttack(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var attack in Attacks)
            if (attack.Loaded && attack.IsActive)
                attack.Draw(spriteBatch);
    }

    private void NextAttack(GameTime gameTime)
    {
        if (AutoShoot)
        {
            // Update time since last attack
            timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // Go to next attack if AutoShoot is true and enough time has passed
            if (timeSinceLastAttack >= Frequency)
            {
                // Reactivate attack pattern by reloading inactive attacks
                for (int i = attackIndex; i < Attacks.Count; i++)
                {
                    if (!Attacks[i].IsActive)
                    {
                        if (Attacks[i].Loaded)
                            Attacks[i].UnloadContent();
                        if (Attacks.Count < attackIndex + 1)
                        {
                            attackIndex++;
                            Attacks[attackIndex].LoadContent(parent);
                            break;
                        }
                        else
                        {
                            Attacks[i].LoadContent(parent);
                            break;
                        }
                    }
                }
                timeSinceLastAttack = 0; // Reset time since last attack
            }
        }
        else
        {
            // Create a copy of the Attacks list
            List<Attack> attacksToRemove = new List<Attack>(Attacks);

            // Iterate over the copy to avoid modifying the original list
            foreach (var attack in attacksToRemove)
            {
                if (!attack.IsActive)
                {
                    attack.UnloadContent();
                    Attacks.Remove(attack);
                }
            }
        }
    }
}