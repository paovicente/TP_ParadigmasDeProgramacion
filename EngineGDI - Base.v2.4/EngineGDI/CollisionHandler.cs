using EngineGDI;
using EngineGDI.EngineGDI;
using System.Collections.Generic;

namespace EngineGDI
{
    /// <summary>
    /// This class is static because it only has logic. It does not have its own state or properties.
    /// </summary>
    public static class CollisionSystem
    {
        //parameter list called enemies just for guidance, it can be a list with any class type that implements IDamageableByPlayer
        public static int HandleCollisions<T>(List<T> enemies, List<Projectile> bullets) where T : IDamageableByPlayer
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
                        enemy.TakeDamage(bullet.Damage);
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

        public static void HandlePowerUpCollisions<T>(List<T> powerUps,Player player) where T : ICollectable
        {
            foreach (var powerUp in powerUps)
            {
                if (!powerUp.IsActive)
                    continue;

                if (Collision.IsBoxColliding(
                    player.Transform.Position,
                    new Vector2(32f, 32f),
                    powerUp.Transform.Position,
                    powerUp.CollisionSize))
                {
                    powerUp.Collect(player);
                    powerUp.Deactivate();
                }
            }
        }
    }
}