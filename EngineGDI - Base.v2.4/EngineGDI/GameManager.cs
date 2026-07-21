using System;
using System.Drawing;
using System.Windows.Forms;

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

            public static PowerUpSpawner PowerUpSpawner { get; private set; }
            public static Bar TimeBar { get; private set; }

            public static Bar PointsBar { get; private set; }

            public static PlayerStats PlayerStats { get; private set; }

            public static EnemyUI BouncingUI { get; private set; }

            public static EnemyUI SpiralUI { get; private set; }

            public static EnemyUI ChaserUI { get; private set; }

            public static EnemyUI BossUI { get; private set; }


            public bool SessionEnded { get; private set; }

            public bool SessionVictory { get; private set; }


            private float sessionTimeLeft;

            private int sessionScore;

            private int currentLevel;
            public int CurrentLevel => currentLevel;

            public bool ShowHUD { get; set; } = true;

            // Events
            public event Action<int> OnLevelCompleted;

            public event Action OnGameOver;

            private GameManager()
            {

            }


            public void Initialize()
            {
                SessionEnded = false;
                SessionVictory = false;

                sessionScore = 0;

                Player = null;
                EnemySpawner = null;
                PowerUpSpawner = null;
                TimeBar = null;
                PointsBar = null;
                PlayerStats = null;
                BouncingUI = null;
                SpiralUI = null;
                ChaserUI = null;
                BossUI = null;
            }

            public void StartLevel(int level, LevelData data)
            {
                RenderSystem.Instance.Clear();

                currentLevel = level;

                SessionEnded = false;

                SessionVictory = false;

                sessionDuration = data.Duration;

                sessionTimeLeft = data.Duration;

                pointsToWin = data.PointsToWin;

                sessionScore = 0;

                Player = new Player();

                EnemySpawner = new EnemySpawner(
                    1.5f,
                    Player,
                    data.Enemies
                );

                PowerUpSpawner = new PowerUpSpawner();

                TimeBar = new Bar(
                    "BarEmpty1.png",
                    "BarFull1.png",
                    new Vector2 (50f, 50f),
                    new Vector2 (1.5f, 0.2f),
                    sessionDuration);

                PointsBar = new Bar(
                    "BarEmpty2.png",
                    "BarFull2.png",
                    new Vector2 (750f, 50f),
                    new Vector2 (1.5f, 0.2f),
                    pointsToWin);

            if (PlayerStats == null)
            {
                PlayerStats = new PlayerStats();
            }

                BouncingUI = new EnemyUI(PlayerStats, EnemyType.Bouncing, new Vector2(830f, 150f), new Vector2(2f, 2f));
                SpiralUI = new EnemyUI(PlayerStats, EnemyType.Spiral, new Vector2(830f, 220f), new Vector2(1.8f, 1.8f));
                ChaserUI = new EnemyUI(PlayerStats, EnemyType.Chaser, new Vector2(830f, 290f), new Vector2(1.6f, 1.6f));
                BossUI = new EnemyUI(PlayerStats, EnemyType.Boss, new Vector2(830f, 360f), new Vector2(1f, 1f));
        }


            public void Update(float deltaTime, int screenWidth)
            {
                if (SessionEnded)
                    return;

                sessionTimeLeft -= deltaTime;
                TimeBar.BarCurrent = sessionTimeLeft;

                if (sessionTimeLeft < 0)
                    sessionTimeLeft = 0;

                Player.Update();

                EnemySpawner.Update(
                    deltaTime,
                    screenWidth
                );

                PowerUpSpawner.Update(deltaTime);

                CollisionSystem.HandlePowerUpCollisions(
                    PowerUpSpawner.PowerUps,
                    Player
                );


                int pointsThisFrame =
                    CollisionSystem.HandleCollisions(
                        EnemySpawner.Enemies,
                        Player.Shooter.Projectiles
                    );

                sessionScore += pointsThisFrame;
                PointsBar.BarCurrent = sessionScore;

                if (sessionScore >= pointsToWin)
                {
                    RenderSystem.Instance.Clear();

                    if (currentLevel < LevelDatabase.Levels.Count)
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


                if (sessionTimeLeft <= 0)
                {
                    SessionEnded = true;

                    OnGameOver?.Invoke();
                }
            }

            public void RemoveTime(float seconds)
            {
                sessionTimeLeft -= seconds;


                if (sessionTimeLeft < 0)
                    sessionTimeLeft = 0;
            }

            public void Render()
            {
                RenderSystem.Instance.RenderAll();

                DrawUI();
            }

            private void DrawUI()
            {
                if (SessionEnded || !ShowHUD)
                    return;

                int secondsLeft = (int)Math.Ceiling(sessionTimeLeft);

                if (secondsLeft < 0)
                    secondsLeft = 0;

                Engine.DrawText(
                    "Time Left: " + secondsLeft + " s",
                    16f,
                    12f,
                    22f,
                    Color.White,
                    "Consolas"
                );

                Engine.DrawText(
                    "Points: " + sessionScore +
                    " / " + pointsToWin,
                    Program.SCREEN_WIDTH - 320f,
                    12f,
                    22f,
                    Color.White,
                    "Consolas"
                );
            }
        }
    }

