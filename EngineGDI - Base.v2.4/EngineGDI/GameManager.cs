using System;
using System.Collections.Generic;
using System.Drawing;

namespace EngineGDI
{
    public class GameManager
    {
        public const float SessionDuration = 30f;
        public const int PointsToWin = 30;

        private static GameManager instance;

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

        public bool SessionEnded { get; private set; }
        public bool SessionVictory { get; private set; }

        private float sessionTimeLeft;
        private int sessionScore;

        private List<EnemyType> level1Enemies = new List<EnemyType>
        {
            EnemyType.Bouncing,
            EnemyType.Spiral
        };

        private GameManager()
        {
        }

        public void Initialize()
        {
            SessionEnded = false;
            SessionVictory = false;
            sessionTimeLeft = SessionDuration;
            sessionScore = 0;

            Player = new Player("PlayerTest.png", new Vector2(40, 490));

            EnemySpawner = new EnemySpawner(2f, Player, level1Enemies);
        }

        public void Update(float deltaTime, int screenWidth)
        {
            if (SessionEnded)
                return;

            sessionTimeLeft -= deltaTime;
            if (sessionTimeLeft < 0f)
                sessionTimeLeft = 0f;

            Player.Update();

            EnemySpawner.Update(deltaTime, screenWidth);

            int pointsThisFrame = CollisionSystem.HandleCollisions(
                EnemySpawner.Enemies,
                Player.Shooter.Projectiles
            );
            sessionScore += pointsThisFrame;

            if (sessionScore >= PointsToWin)
            {
                SessionVictory = true;
                SessionEnded = true;
                return;
            }

            if (sessionTimeLeft <= 0f)
            {
                SessionVictory = false;
                SessionEnded = true;
            }
        }

        public void Render()
        {
            Engine.Draw("fondo1.png", 0, 0);

            // Pixelart: escalas visuales centralizadas (no afectan colisiones/mecánicas).
            const float playerRenderScale = 3.0f;
            const float enemyRenderScale = 3.0f;
            const float projectileRenderScale = 0.50f;

            Engine.Draw(Player.Sprite, Player.Pos.X, Player.Pos.Y, playerRenderScale, playerRenderScale, 0, .5f, .5f);

            foreach (var proj in Player.Shooter.Projectiles)
            {
                if (proj.IsActive)
                {
                    Engine.Draw(
                        proj.Sprite,
                        proj.Position.X,
                        proj.Position.Y,
                        projectileRenderScale, projectileRenderScale,
                        0,
                        0.5f, 0.5f
                    );
                }
            }

            foreach (var e in EnemySpawner.Enemies)
            {
                Vector2 renderScale = e.RenderScale;
                if (e is SpiralEnemy || e is BouncingEnemy)
                    renderScale = new Vector2(enemyRenderScale, enemyRenderScale);

                Engine.Draw(
                    e.Sprite,
                    e.Pos.X,
                    e.Pos.Y,
                    renderScale.X, renderScale.Y,
                    0,
                    0.5f, 0.5f
                );
            }

            if (!SessionEnded)
            {
                int secondsLeft = (int)Math.Ceiling(sessionTimeLeft);
                if (secondsLeft < 0)
                    secondsLeft = 0;

                Engine.DrawText(
                    "Tiempo: " + secondsLeft + " s",
                    16f,
                    12f,
                    22f,
                    Color.White,
                    "Consolas");

                Engine.DrawText(
                    "Puntos: " + sessionScore + " / " + PointsToWin,
                    Program.SCREEN_WIDTH - 320f,
                    12f,
                    22f,
                    Color.White,
                    "Consolas");
            }
        }
    }
}
