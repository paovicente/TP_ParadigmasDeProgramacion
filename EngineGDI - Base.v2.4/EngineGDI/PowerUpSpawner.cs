using EngineGDI.EngineGDI;
using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public class PowerUpSpawner
    {
        private List<ICollectable> powerUps = new List<ICollectable>();

        private int maxPowerUpsPerLevel = 5;
        private int spawnedPowerUps = 0;

        private float spawnInterval = 5f;
        private float timer;

        private Random random = new Random();

        public List<ICollectable> PowerUps => powerUps;

        public void Update(float deltaTime)
        {
            if (spawnedPowerUps < maxPowerUpsPerLevel)
            {
                timer += deltaTime;

                if (timer >= spawnInterval)
                {
                    Spawn();
                    timer = 0f;
                }
            }

            foreach (var powerUp in powerUps)
            {
                if (powerUp.IsActive)
                {
                    powerUp.Update(deltaTime);
                }
            }
        }


        private void Spawn()
        {
            Vector2 position = GetRandomPosition();

            ICollectable powerUp = PowerUpFactory.CreateRandomPowerUp(position);

            powerUps.Add(powerUp);

            spawnedPowerUps++;
        }

        private Vector2 GetRandomPosition()
        {
            float x = random.Next(50, Program.SCREEN_WIDTH - 50);

            return new Vector2(x, 0);
        }

        public void Reset()
        {
            foreach (var powerUp in powerUps)
            {
                if (powerUp.IsActive)
                {
                    powerUp.Deactivate();
                }
            }

            powerUps.Clear();
            spawnedPowerUps = 0;
            timer = 0f;
        }
    }
}