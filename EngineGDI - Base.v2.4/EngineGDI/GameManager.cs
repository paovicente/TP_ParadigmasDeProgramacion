using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public class GameManager
    {
        //singleton
        private static GameManager instance;

        //getter
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();

                return instance;
            }
        }

        public static Player Player { get; private set; }
        public static EnemySpawner EnemySpawner { get; private set; }

        //constructor
        private GameManager()
        {
        }

        public void Initialize()
        {
            Player = new Player("PlayerTest.png", new Vector2(40, 490));
            EnemySpawner = new EnemySpawner(2f);
        }

        public void Update(float deltaTime, int screenWidth)
        {
            Player.Update();

            EnemySpawner.Update(deltaTime, screenWidth);

            HandleCollisions();
        }

        /*private void HandleCollisions()
        {
            var enemies = EnemySpawner.Enemies;
            var bullets = Player.Shooter.Projectiles;

            foreach (var enemy in enemies)
            {
                if (!enemy.IsActive) continue;

                foreach (var bullet in bullets)
                {
                    if (!bullet.IsActive) continue;

                    if (Collision.IsBoxColliding(
                        bullet.Position, bullet.Size,
                        enemy.Pos, enemy.Size))
                    {
                        bullet.Deactivate();
                        enemy.Deactivate();
                    }
                }
            }
        }*/

        private void HandleCollisions()
        {
            var enemies = EnemySpawner.Enemies;
            var bullets = Player.Shooter.Projectiles;

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

        public void Render()
        {
            Engine.Draw("fondo1.png", 0, 0);

            //player
            Engine.Draw(Player.Sprite, Player.Pos.X, Player.Pos.Y, 0.03f, 0.03f, 0, .5f, .5f);

            //projectiles
            foreach (var proj in Player.Shooter.Projectiles)
            {
                if (proj.IsActive)
                {
                    Engine.Draw(
                        proj.Sprite,
                        proj.Position.X,
                        proj.Position.Y,
                        0.02f, 0.02f,
                        0,
                        0.5f, 0.5f
                    );
                }
            }

            //enemies
            foreach (var e in EnemySpawner.Enemies)
            {

                Engine.Draw(
                    e.Sprite,
                    e.Pos.X,
                    e.Pos.Y,
                    0.01f, 0.01f,
                    0,
                    0.5f, 0.5f
                );
            }
        }
    }
}