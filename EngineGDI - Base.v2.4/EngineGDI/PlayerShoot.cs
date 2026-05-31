using EngineGDI;
using System.Collections.Generic;

/// <summary>
/// This class exist if the Player class exist. So there is a Composition relation between them.
/// </summary>
namespace EngineGDI
{
    public class PlayerShoot
    {
        private ObjectPool<Projectile> projectilePool;

        public List<Projectile> Projectiles => projectilePool.Objects;

        public PlayerShoot()
        {
            projectilePool = new ObjectPool<Projectile>(20, CreateProjectile);
        }

        private Projectile CreateProjectile()
        {
            return new Projectile("Bullet.png",250f);
        }

        public void Shoot(Vector2 pos)
        {
            Projectile proj = projectilePool.Get();

            if (proj != null)
            {
                proj.Activate(pos,new Vector2(0f, -1f));
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var proj in projectilePool.Objects)
            {
                proj.Update(deltaTime);
            }
        }
    }
}