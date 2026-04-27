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

            Enemy enemy = CreateEnemy(type, spawnPos);

            enemies.Add(enemy);
        }

        private Enemy CreateEnemy(EnemyType type, Vector2 pos)
        {
            switch (type)
            {
                case EnemyType.Bouncing:
                    return new BouncingEnemy("BasicEnemy.png", pos);

                case EnemyType.Spiral:
                    return new SpiralEnemy("SpiralEnemy.png", pos);

                case EnemyType.Chaser:
                    return new ChaserEnemy("BasicEnemy.png", pos, player);

                case EnemyType.Boss:
                    return new BossEnemy("BasicEnemy.png", pos, player);

                default:
                    throw new Exception("Enemy type not supported");
            }
        }
    }
}