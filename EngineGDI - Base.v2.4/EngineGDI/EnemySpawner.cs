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

        //encapsulation
        public List<Enemy> Enemies => enemies;

        
        public EnemySpawner(float cooldown)
        {
            enemies = new List<Enemy>();
            spawnCooldown = cooldown;
            spawnTimer = cooldown;
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

            //checks the enemies that have overtaken the screen and erases them
            enemies.RemoveAll(e => e.Pos.Y > Program.SCREEN_HEIGHT);
        }

        private void Spawn(int screenWidth)
        {
            float x = (float)rng.NextDouble() * screenWidth;
            enemies.Add(new Enemy("BasicEnemy.png", new Vector2(x, 0)));
        }
    }
}