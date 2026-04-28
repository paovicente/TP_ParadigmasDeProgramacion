namespace EngineGDI
{
    public class MenuManager
    {
        private enum MenuState
        {
            InMenu,
            InGame,
            EndScreen
        }

        private static MenuManager instance;
        private MenuState currentState;
        private Menu currentMenu;

        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuManager();

                return instance;
            }
        }

        private MenuManager()
        {
            currentState = MenuState.InMenu;
            currentMenu = new MainMenu();
        }

        public void Initialize()
        {
            currentState = MenuState.InMenu;
            currentMenu = new MainMenu();
            currentMenu.Initialize();
        }

        public void Update(float deltaTime, int screenWidth)
        {
            if (currentState == MenuState.InMenu)
            {
                currentMenu.Update();
                MenuAction action = currentMenu.SelectedAction;

                if (action == MenuAction.Play)
                {
                    GameManager.Instance.Initialize();
                    currentState = MenuState.InGame;
                }
                else if (action == MenuAction.Exit)
                {
                    Engine.Window.Close();
                }

                return;
            }

            if (currentState == MenuState.EndScreen)
            {
                currentMenu.Update();
                MenuAction action = currentMenu.SelectedAction;

                if (action == MenuAction.BackToMenu)
                {
                    currentMenu = new MainMenu();
                    currentMenu.Initialize();
                    currentState = MenuState.InMenu;
                }

                return;
            }

            GameManager.Instance.Update(deltaTime, screenWidth);

            if (GameManager.Instance.SessionEnded)
            {
                currentMenu = new FinalMenu(GameManager.Instance.SessionVictory);
                currentMenu.Initialize();
                currentState = MenuState.EndScreen;
            }
        }

        public void Render()
        {
            if (currentState == MenuState.InMenu || currentState == MenuState.EndScreen)
            {
                currentMenu.Render();
                return;
            }

            GameManager.Instance.Render();
        }
    }
}
