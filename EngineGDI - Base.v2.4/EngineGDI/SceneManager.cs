using System.Drawing;
using System.Windows.Forms;

namespace EngineGDI
{
    public class SceneManager
    {
        private enum MenuState
        {
            InMenu,
            InGame,
            EndScreen,
            LevelCompleted
        }

        private static SceneManager instance;
        private MenuState currentState;
        private Scene currentScene;
        private int nextLevel;
        private int completedLevel;

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SceneManager();

                return instance;
            }
        }

        private SceneManager()
        {
            currentState = MenuState.InMenu;
            currentScene = new MainMenu();

            //events subscription
            GameManager.Instance.OnLevelCompleted += HandleLevelCompleted;

            GameManager.Instance.OnGameOver += HandleGameOver;
        }

        public void Initialize()
        {
            currentState = MenuState.InMenu;
            currentScene = new MainMenu();
            currentScene.Initialize();
        }

        public void Update(float deltaTime, int screenWidth)
        {
            switch (currentState)
            {
                case MenuState.InMenu:

                    currentScene.Update(deltaTime);

                    MenuScene mainMenu = currentScene as MenuScene;

                    if (mainMenu != null)
                    {
                        switch (mainMenu.SelectedAction)
                        {
                            case MenuAction.Play:

                                GameManager.Instance.Initialize();

                                LevelData data = LevelDatabase.Levels[1];

                                currentScene = new GameplayScene(1, data);
                                currentScene.Initialize();

                                currentState = MenuState.InGame;
                                break;

                            case MenuAction.Exit:

                                Engine.Window.Close();
                                break;
                        }
                    }

                    break;

                case MenuState.InGame:

                    currentScene.Update(deltaTime);

                    if (GameManager.Instance.SessionEnded)
                    {
                        currentScene = new FinalMenu(GameManager.Instance.SessionVictory);
                        currentScene.Initialize();

                        currentState = MenuState.EndScreen;
                    }

                    break;

                case MenuState.EndScreen:

                    currentScene.Update(deltaTime);

                    MenuScene finalMenu = currentScene as MenuScene;

                    if (finalMenu != null &&
                        finalMenu.SelectedAction == MenuAction.BackToMenu)
                    {
                        currentScene = new MainMenu();
                        currentScene.Initialize();

                        currentState = MenuState.InMenu;
                    }

                    break;

                case MenuState.LevelCompleted:

                    if (Engine.OnKeyDown(Keys.Enter))
                    {

                        GameManager.Instance.ShowHUD = true;

                        LevelData data = LevelDatabase.Levels[nextLevel];

                        currentScene = new GameplayScene(nextLevel, data);
                        currentScene.Initialize();

                        currentState = MenuState.InGame;
                    }

                    break;
            }
        }

        private void HandleLevelCompleted(int nextLevel)
        {
            GameManager.Instance.ShowHUD = false;

            completedLevel = GameManager.Instance.CurrentLevel;
            this.nextLevel = nextLevel;

            currentState = MenuState.LevelCompleted;
        }

        private void HandleGameOver()
        {
            currentScene = new FinalMenu(false);

            currentScene.Initialize();

            currentState = MenuState.EndScreen;
        }

        public void Render()
        {
            currentScene.Render();

            if (currentState == MenuState.LevelCompleted)
            {
                DrawLevelCompletedOverlay();
            }
        }

        private void DrawLevelCompletedOverlay()
        {
            int imageWidth = 720;
            int imageHeight = 480;

            float x = (Program.SCREEN_WIDTH - imageWidth) / 2f;
            float y = (Program.SCREEN_HEIGHT - imageHeight) / 2f;

            Engine.Draw("fondolevelcompleted.png", x, y, 0.8f, 0.8f);

            Engine.DrawText($"LEVEL {completedLevel} COMPLETED",280,220,32,Color.White,"Arial");

            Engine.DrawText("PRESS ENTER TO CONTINUE",300,300,22,Color.Plum,"Arial");
        }

    }
}
