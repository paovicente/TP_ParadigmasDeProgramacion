using EngineGDI;
using System.Collections.Generic;

namespace EngineGDI
{
    /// <summary>
    /// This class is static because it only has logic. It does not have its own state or properties.
    /// </summary>
    public static class CollisionSystem
    {
        public static int HandleCollisions(List<Enemy> enemies, List<Projectile> bullets)
        {
            int pointsEarned = 0;

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
                        pointsEarned += enemy.PointsOnKill;
                        bullets.RemoveAt(j);
                        enemies.RemoveAt(i);
                        break;
                    }
                }
            }

            return pointsEarned;
        }
    }
}