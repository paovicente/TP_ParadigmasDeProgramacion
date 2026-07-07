using System.Drawing;

namespace EngineGDI
{
    public class Button
    {
        public string Label { get; private set; }
        public MenuAction Action { get; private set; }
        public bool IsSelected { get; private set; }
        public string TexturePath { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Scale { get; private set; }

        public Button(string label, MenuAction action, string texturePath, Vector2 position, Vector2 scale)
        {
            Label = label;
            Action = action;
            IsSelected = false;
            TexturePath = texturePath;
            Position = position;
            Scale = scale;
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
        }

        public void Render()
        {
            float renderScaleX = IsSelected
                ? Scale.X * 1.03f
                : Scale.X * 0.95f;

            float renderScaleY = IsSelected
                ? Scale.Y * 1.03f
                : Scale.Y * 0.95f;


            Engine.Draw(
                TexturePath,
                Position.X,
                Position.Y,
                renderScaleX,
                renderScaleY,
                0f,
                0.5f,
                0.5f
            );


            Color textColor = IsSelected
                ? Color.Gold
                : Color.White;


            float labelWidth = MeasureTextWidthPx(
                Label,
                18f,
                "Arial"
            );

            float textX = Position.X - labelWidth / 2f;
            float textY = Position.Y - 15f;

            Engine.DrawText(
                Label,
                textX,
                textY,
                18f,
                textColor,
                "Arial"
            );
        }

        private static float MeasureTextWidthPx(string text, float fontSize, string fontName)
        {
            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                using (var font = new Font(fontName, fontSize))
                {
                    return g.MeasureString(text, font).Width;
                }
            }
        }
    }
}
