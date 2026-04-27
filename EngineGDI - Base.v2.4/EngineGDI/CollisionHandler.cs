using EngineGDI;
using System.Collections.Generic;

namespace EngineGDI
{
    /// <summary>
    /// This class is static because it only has logic. It does not have its own state or properties.
    /// </summary>
    public static class CollisionSystem
    {
        public static void HandleCollisions(List<Enemy> enemies, List<Projectile> bullets)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                var enemy = enemies[i];

                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    var bullet = bullets[j];

                    if (Collision.IsBoxColliding(
                        bullet.Position, bullet.Size,
                        enemy.Pos, enemy.Size))
                    {
                        bullets.RemoveAt(j);
                        enemies.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}