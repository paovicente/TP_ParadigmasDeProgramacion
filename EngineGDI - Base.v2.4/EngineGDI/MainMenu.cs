using System.Windows.Forms;
using System.Drawing;

namespace EngineGDI
{
    public class MainMenu : Menu
    {
        private int selectedIndex;

        public MainMenu()
        {
            buttons.Add(new Button(
                "JUGAR",
                MenuAction.Play,
                "ButtonTest.png",
                new Vector2(512, 240),
                new Vector2(0.28f, 0.28f)
            ));
            buttons.Add(new Button(
                "SALIR",
                MenuAction.Exit,
                "ButtonTest.png",
                new Vector2(512, 405),
                new Vector2(0.28f, 0.28f)
            ));
            selectedIndex = 0;
        }

        public override void Initialize()
        {
            selectedIndex = 0;
            SelectedAction = MenuAction.None;
            SetSelection();
        }

        public override void Update()
        {
            bool moveUp = Engine.OnKeyDown(Keys.Up) || Engine.OnKeyDown(Keys.W);
            bool moveDown = Engine.OnKeyDown(Keys.Down) || Engine.OnKeyDown(Keys.S);

            if (moveUp || moveDown)
            {
                selectedIndex = (selectedIndex + 1) % buttons.Count;
                SetSelection();
            }

            if (Engine.OnKeyDown(Keys.Enter))
            {
                SelectedAction = buttons[selectedIndex].Action;
            }
        }

        public override void Render()
        {
            Engine.Draw("fondo1.png", 0, 0);

            foreach (Button button in buttons)
            {
                float renderScaleX = button.IsSelected ? button.Scale.X * 1.03f : button.Scale.X * 0.95f;
                float renderScaleY = button.IsSelected ? button.Scale.Y * 1.03f : button.Scale.Y * 0.95f;

                Engine.Draw(
                    button.TexturePath,
                    button.Position.X,
                    button.Position.Y,
                    renderScaleX,
                    renderScaleY,
                    0f,
                    0.5f,
                    0.5f
                );

                Color textColor = button.IsSelected ? Color.Gold : Color.White;
                float textX = button.Position.X - 37f;
                float textY = button.Position.Y - 11f;
                Engine.DrawText(button.Label, textX, textY, 18f, textColor, "Arial");
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
