using EngineGDI;
using System.Collections.Generic;

/// <summary>
/// This class exist if the Player class exist. So there is a Composition relation between them.
/// </summary>
namespace EngineGDI
{
    public class PlayerShoot
    {
        private List<Projectile> projectiles;

        public List<Projectile> Projectiles => projectiles;

        public PlayerShoot()
        {
            projectiles = new List<Projectile>();

            for (int i = 0; i < 20; i++) //bullet limit (temp solution - apply object pool later)
            {
                projectiles.Add(new Projectile("Bullet.png", 150f));
            }
        }

        public void Shoot(Vector2 pos)
        {
            foreach (var proj in projectiles)
            {
                if (!proj.IsActive)
                {
                    proj.Activate(pos,
                                  new Vector2(0f, -1f)); //up
                    break;
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var proj in projectiles)
            {
                proj.Update(deltaTime);
            }
        }
    }
}