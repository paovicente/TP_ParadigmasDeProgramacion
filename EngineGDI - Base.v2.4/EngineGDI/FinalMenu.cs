using System.Drawing;
using System.Windows.Forms;

namespace EngineGDI
{
    /// <summary>
    /// Victory or defeat screen with a button to return to menu
    /// </summary>
    public class FinalMenu : MenuScene
    {
        private readonly bool victory;
        private int selectedIndex;

        private static float MeasureTextWidthPx(
            string text,
            float fontSize,
            string fontName)
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

        public FinalMenu(bool victory): base("Final Screen","fondofinalmenu.png")
        {
            this.victory = victory;

            buttons.Add(new Button(
                "RETURN TO MENU",
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

        public override void Update(float deltaTime)
        {
            if (Engine.OnKeyDown(Keys.Enter))
            {
                SelectedAction = buttons[selectedIndex].Action;
            }
        }

        public override void Render()
        {
            base.Render();

            string title = victory? "VICTORY": "DEFEAT";

            Color titleColor = victory
                ? Color.LawnGreen
                : Color.IndianRed;

            float titleW = MeasureTextWidthPx(title,36f,"Arial");

            float titleX = Program.SCREEN_WIDTH / 2f - titleW / 2f;

            Engine.DrawText(
                title,
                titleX,
                120f,
                36f,
                titleColor,
                "Arial"
            );
        }
    }
}