using System;
using System.Collections.Generic;
using System.Drawing;

namespace EngineGDI
{
    public class GameManager
    {
        public float sessionDuration;
        public int pointsToWin;

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

        //events
        public event Action<int> OnLevelCompleted; //here is Action int because we want to send next level number
        public event Action OnGameOver;

        //levels
        private int currentLevel;
        public int CurrentLevel => currentLevel;

        private Dictionary<int, LevelData> levels = new Dictionary<int, LevelData>
        {
            {
                1, new LevelData(30f,
                    20,
                    new List<EnemyType>
                    {
                        EnemyType.Bouncing,
                        EnemyType.Spiral
                    }
                )
            },

            {
                2,
                new LevelData(
                    50f,
                    40,
                    new List<EnemyType>
                    {
                        EnemyType.Bouncing,
                        EnemyType.Spiral,
                        EnemyType.Chaser
                    }
                )
            },

            {
                3,
                new LevelData(
                    80f,
                    70,
                    new List<EnemyType>
                    {
                        EnemyType.Boss
                    }
                )
            }
        };

        private GameManager()
        {
        }

        public void Initialize()
        {
            StartLevel(1);
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

            if (sessionScore >= pointsToWin)
            {
                if (currentLevel < 3)
                {
                    OnLevelCompleted?.Invoke(currentLevel + 1);
                }
                else
                {
                    SessionVictory = true;
                    SessionEnded = true;
                }
                return;
            }

            if (sessionTimeLeft <= 0f)
            {
                SessionEnded = true;

                OnGameOver?.Invoke();
            }
        }

        public void StartLevel(int level)
        {
            currentLevel = level;

            LevelData levelData = levels[level];

            SessionEnded = false;
            SessionVictory = false;

            sessionDuration = levelData.Duration;

            sessionTimeLeft = levelData.Duration;

            pointsToWin = levelData.PointsToWin;

            sessionScore = 0;

            Player = new Player("PlayerFrame1.png", new Vector2(40, 490));

            EnemySpawner = new EnemySpawner(2f, Player, levelData.Enemies);
        }

        public void RemoveTime(float seconds)
        {
            sessionTimeLeft -= seconds;

            if (sessionTimeLeft < 0f)
                sessionTimeLeft = 0f;
        }

        public void Render()
        {
            Engine.Draw("fondo1.png", 0, 0);

            const float playerRenderScale = 3.0f;
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
                if (!e.IsActive)
                    continue;

                Engine.Draw(
                    e.Sprite,
                    e.Pos.X,
                    e.Pos.Y,
                    e.RenderScale.X,
                    e.RenderScale.Y,
                    0,
                    0.5f,
                    0.5f
                );
            }

            if (!SessionEnded)
            {
                int secondsLeft = (int)Math.Ceiling(sessionTimeLeft);
                if (secondsLeft < 0)
                    secondsLeft = 0;

                Engine.DrawText(
                    "Time Left: " + secondsLeft + " s",
                    16f,
                    12f,
                    22f,
                    Color.White,
                    "Consolas");

                Engine.DrawText(
                    "Points: " + sessionScore + " / " + pointsToWin,
                    Program.SCREEN_WIDTH - 320f,
                    12f,
                    22f,
                    Color.White,
                    "Consolas");
            }
        }
    }
}
