using EngineGDI;
using System.Collections.Generic;

namespace EngineGDI
{
    /// <summary>
    /// This class is static because it only has logic. It does not have its own state or properties.
    /// </summary>
    public static class CollisionSystem
    {
        public static int HandleCollisions(List<IDamageableByPlayer> enemies, List<Projectile> bullets)
        {
            int pointsEarned = 0;

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                var enemy = enemies[i];

                if (!enemy.IsActive)
                    continue;

                for (int j = bullets.Count - 1; j >= 0; j--)
                {
                    var bullet = bullets[j];

                    if (!bullet.IsActive)
                        continue;

                    if (Collision.IsBoxColliding(bullet.Transform.Position,bullet.CollisionSize,enemy.Transform.Position,enemy.CollisionSize))
                    {
                        enemy.TakeDamage(1);
                        bullet.Deactivate();

                        if (!enemy.IsActive)
                        {
                            pointsEarned += enemy.PointsOnKill;

                            //enemies.RemoveAt(i);
                        }

                        break;
                    }
                }
            }

            return pointsEarned;
        }
    }
}