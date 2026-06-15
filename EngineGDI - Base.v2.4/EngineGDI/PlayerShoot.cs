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
            Transform transform = new Transform();
            transform.Scale = new Vector2(0.5f,0.5f);//0.5,0.5

            Renderer renderer = new Renderer("Bullet.png", transform);

            renderer.OffsetX = 0.5f;
            renderer.OffsetY = 0.5f;

            return new Projectile(transform, renderer, 250f);
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
                if (proj == null)
                    continue;

                proj.Update(deltaTime);
            }
        }

        public void Render()
        {

            foreach (var proj in projectilePool.Objects)
            {
                if (proj == null)
                    continue;

                if (proj.IsActive)
                    proj.Render();
            }
        }
    }
}