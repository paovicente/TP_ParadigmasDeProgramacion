namespace EngineGDI
{
    public class SceneManager
    {
        private enum MenuState
        {
            InMenu,
            InGame,
            EndScreen
        }

        private static SceneManager instance;
        private MenuState currentState;
        private Scene currentScene;

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
            }
        }

        private void HandleLevelCompleted(int nextLevel)
        {
            if (LevelDatabase.Levels.ContainsKey(nextLevel))
            {
                LevelData data = LevelDatabase.Levels[nextLevel];

                currentScene = new GameplayScene(nextLevel,data);

                currentScene.Initialize();

                currentState = MenuState.InGame;
            }
            else
            {
                currentScene = new FinalMenu(true);

                currentScene.Initialize();

                currentState = MenuState.EndScreen;
            }
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
        }
    }
}
