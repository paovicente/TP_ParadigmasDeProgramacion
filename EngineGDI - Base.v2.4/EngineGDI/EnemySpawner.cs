using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public class EnemySpawner
    {
        private List<Enemy> enemies;
        private float spawnTimer;
        private float spawnCooldown;
        private int maxEnemies = 20;
        private int maxBosses = 3;

        private Random rng = new Random();

        private List<EnemyType> availableEnemies;
        private Player player;

        public List<Enemy> Enemies => enemies;

        public EnemySpawner(float cooldown, Player player, List<EnemyType> availableEnemies)
        {
            enemies = new List<Enemy>();
            spawnCooldown = cooldown;
            spawnTimer = cooldown;
            this.player = player;
            this.availableEnemies = availableEnemies;
        }

        public void Update(float deltaTime, int screenWidth)
        {
            spawnTimer -= deltaTime;

            if (spawnTimer <= 0f && enemies.Count < maxEnemies)
            {
                Spawn(screenWidth);
                spawnTimer = spawnCooldown;
            }

            foreach (var e in enemies)
            {
                e.Update(deltaTime);
            }

            enemies.RemoveAll(e => !e.IsActive);
        }

        private void Spawn(int screenWidth)
        {
            float x = (float)rng.NextDouble() * screenWidth;
            Vector2 spawnPos = new Vector2(x, 0);

            EnemyType type = availableEnemies[rng.Next(availableEnemies.Count)];

            //to limit the number of bosses on screen
            if (type == EnemyType.Boss)
            {
                int bossCount = 0;

                foreach (var e in enemies)
                {
                    if (e is BossEnemy)
                    {
                        bossCount++;
                    }
                }

                if (bossCount >= maxBosses)
                {
                    return;
                }
            }

            Enemy enemy = EnemyFactory.CreateEnemy(type, spawnPos,player);

            enemy.OnPlayerHit += HandlePlayerHit;

            enemy.OnSpawnRequested += SpawnMinions;

            enemies.Add(enemy);
        }

        private void HandlePlayerHit(float secondsLost)
        {
            GameManager.Instance.RemoveTime(secondsLost);
        }

        private void SpawnMinions(Vector2 bossPos)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 spawnPos = new Vector2(bossPos.X + rng.Next(-100, 100), bossPos.Y + rng.Next(-50, 50));

                Enemy minion = EnemyFactory.CreateEnemy(EnemyType.Chaser, spawnPos, player);

                minion.OnPlayerHit += HandlePlayerHit;

                enemies.Add(minion);
            }
        }
    }
}
