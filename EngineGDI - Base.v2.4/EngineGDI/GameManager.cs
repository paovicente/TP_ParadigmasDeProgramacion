using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;

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
        private Renderer bgRenderer;

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
                RenderSystem.Instance.Clear();

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
            
            bgRenderer = new Renderer("fondo1.png",new Transform());

            currentLevel = level;

            LevelData levelData = levels[level];

            SessionEnded = false;
            SessionVictory = false;

            sessionDuration = levelData.Duration;

            sessionTimeLeft = levelData.Duration;

            pointsToWin = levelData.PointsToWin;

            sessionScore = 0;

            Player = new Player();

            EnemySpawner = new EnemySpawner(1.5f, Player, levelData.Enemies);
        }

        public void RemoveTime(float seconds)
        {
            sessionTimeLeft -= seconds;

            if (sessionTimeLeft < 0f)
                sessionTimeLeft = 0f;
        }

        public void Render()
        {
            bgRenderer.Render();

            RenderSystem.Instance.RenderAll();

            DrawUI();
        }

        private void DrawUI()
        {
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
