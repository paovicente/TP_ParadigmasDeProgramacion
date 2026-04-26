namespace EngineGDI
{
    public class MenuManager
    {
        private enum MenuState
        {
            InMenu,
            InGame
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

            GameManager.Instance.Update(deltaTime, screenWidth);
        }

        public void Render()
        {
            if (currentState == MenuState.InMenu)
            {
                currentMenu.Render();
                return;
            }

            GameManager.Instance.Render();
        }
    }
}
