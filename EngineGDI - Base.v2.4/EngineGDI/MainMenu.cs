using System.Windows.Forms;
using System.Drawing;

namespace EngineGDI
{
    public class MainMenu : MenuScene
    {
        private int selectedIndex;

        public MainMenu(): base("Main Menu","fondomenu.png")
        {
            buttons.Add(new Button(
                "PLAY",
                MenuAction.Play,
                "button1.png",
                new Vector2(512, 240),
                new Vector2(1.5f, 3f)
            ));

            buttons.Add(new Button(
                "EXIT",
                MenuAction.Exit,
                "button1.png",
                new Vector2(512, 350),
                new Vector2(1.5f, 3f)
            ));
        }

        public override void Initialize()
        {
            selectedIndex = 0;
            SelectedAction = MenuAction.None;

            SetSelection();
        }

        public override void Update(float deltaTime)
        {
            bool move = Engine.OnKeyDown(Keys.Down) ||
                        Engine.OnKeyDown(Keys.S) ||
                        Engine.OnKeyDown(Keys.Up) ||
                        Engine.OnKeyDown(Keys.W);

            if (move)
            {
                selectedIndex = (selectedIndex + 1) % buttons.Count;
                SetSelection();
            }


            if (Engine.OnKeyDown(Keys.Enter))
            {
                SelectedAction = buttons[selectedIndex].Action;
            }
        }


        private void SetSelection()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetSelected(i == selectedIndex);
            }
        }
    }
}
