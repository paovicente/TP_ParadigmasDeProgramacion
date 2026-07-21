using System;
using System.Drawing;

namespace EngineGDI
{
    public class TextRenderer
    {
        private readonly Transform transform;

        public string Text { get; set; } = "-";

        public string FontName { get; set; } = "Arial";

        public float FontSize { get; set; } = 24;

        public Color Color { get; set; } = Color.White;

        public TextRenderer(Transform position)
        {
            this.transform = position;
        }

        public void Render()
        {
            Engine.DrawText(
                Text,
                transform.Position.X,
                transform.Position.Y,
                FontSize,
                Color,
                FontName);
        }
    }
}