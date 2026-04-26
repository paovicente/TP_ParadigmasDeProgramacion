using System.Collections.Generic;

namespace EngineGDI
{
    public enum MenuAction
    {
        None,
        Play,
        Exit
    }

    public abstract class Menu
    {
        protected readonly List<Button> buttons;

        public IReadOnlyList<Button> Buttons => buttons;
        public MenuAction SelectedAction { get; protected set; }

        protected Menu()
        {
            buttons = new List<Button>();
            SelectedAction = MenuAction.None;
        }

        public abstract void Initialize();
        public abstract void Update();
        public abstract void Render();
    }
}
