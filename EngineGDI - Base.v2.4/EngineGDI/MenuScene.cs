using System.Collections.Generic;

namespace EngineGDI
{
    public abstract class MenuScene : Scene
    {
        protected readonly List<Button> buttons;

        public IReadOnlyList<Button> Buttons => buttons;

        public MenuAction SelectedAction { get; protected set; }

        protected MenuScene(
            string name,
            string background
        ) : base(name, background)
        {
            buttons = new List<Button>();
            SelectedAction = MenuAction.None;
        }

        public override void Initialize()
        {
            SelectedAction = MenuAction.None;
        }

        public override void Render()
        {
            base.Render();

            foreach (Button button in buttons)
            {
                button.Render();
            }
        }
    }
}