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
        
        private List<EnemyType> level1Enemies = new List<EnemyType>
        {
            EnemyType.Bouncing,
            EnemyType.Spiral
        };

        //constructor
        private GameManager()
        {
        }

        public void Initialize()
        {
            Player = new Player("PlayerTest.png", new Vector2(40, 490));

            EnemySpawner = new EnemySpawner(2f, Player, level1Enemies);
        }

        public void Update(float deltaTime, int screenWidth)
        {
            Player.Update();

            EnemySpawner.Update(deltaTime, screenWidth);

            CollisionSystem.HandleCollisions(
                EnemySpawner.Enemies,
                Player.Shooter.Projectiles
            );
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
                    e.Size.X, e.Size.Y,
                    0,
                    0.5f, 0.5f
                );
            }
        }
    }
}