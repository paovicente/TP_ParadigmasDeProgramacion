using System.Drawing;
using System.Windows.Forms;

namespace EngineGDI
{
    /// <summary>Pantalla de victoria o derrota con un único botón para volver al menú principal.</summary>
    public class FinalMenu : Menu
    {
        private readonly bool victory;
        private int selectedIndex;

        private static float MeasureTextWidthPx(string text, float fontSize, string fontName)
        {
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                FontFamily family = FontFamily.GenericSansSerif;
                try
                {
                    family = new FontFamily(fontName);
                }
                catch
                {
                    family = new FontFamily("Arial");
                }

                using (var font = new Font(family, fontSize))
                    return g.MeasureString(text, font).Width;
            }
        }

        public FinalMenu(bool victory)
        {
            this.victory = victory;
            buttons.Add(new Button(
                "VOLVER AL MENU",
                MenuAction.BackToMenu,
                "button1.png",
                new Vector2(512, 320),
                new Vector2(2.6f, 3.4f)
            ));
            selectedIndex = 0;
        }

        public override void Initialize()
        {
            selectedIndex = 0;
            SelectedAction = MenuAction.None;
            buttons[0].SetSelected(true);
        }

        public override void Update()
        {
            if (Engine.OnKeyDown(Keys.Enter))
                SelectedAction = buttons[selectedIndex].Action;
        }

        public override void Render()
        {
            Engine.Draw("fondomenu.png", 0, 0, 1f, 0.5f);

            string title = victory ? "VICTORIA" : "DERROTA";
            Color titleColor = victory ? Color.LawnGreen : Color.IndianRed;
            float titleW = MeasureTextWidthPx(title, 36f, "Arial");
            float titleX = Program.SCREEN_WIDTH / 2f - titleW / 2f;
            Engine.DrawText(title, titleX, 120f, 36f, titleColor, "Arial");

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
                    0.5f);

                Color textColor = button.IsSelected ? Color.Gold : Color.White;
                float labelW = MeasureTextWidthPx(button.Label, 18f, "Arial");
                float textX = button.Position.X - labelW / 2f;
                float textY = button.Position.Y - 15f;
                Engine.DrawText(button.Label, textX, textY, 18f, textColor, "Arial");
            }
        }
    }
}
