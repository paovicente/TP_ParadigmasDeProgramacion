using System;

namespace EngineGDI
{
    public class Bar : IRenderable
    {
        private readonly Transform transform;

        private readonly Renderer backgroundRenderer;
        private readonly Renderer fillRenderer;

        private readonly float originalWidth;

        private float barCurrent;
        private float barMax;

        public Transform Transform => transform;

        public float BarCurrent
        {
            get => barCurrent;
            set => barCurrent = Math.Max(value, 0f);
        }

        public float BarMax
        {
            get => barMax;
            set => barMax = Math.Max(value, 1f);
        }

        public Bar(
            string backgroundSprite,
            string fillSprite,
            Vector2 position,
            Vector2 scale,
            float maxValue)

        {
            transform = new Transform();

            transform.Position = position;
            transform.Scale = scale;

            originalWidth = scale.X;

            backgroundRenderer = new Renderer(backgroundSprite, transform);
            fillRenderer = new Renderer(fillSprite, transform);

            barMax = maxValue;
            barCurrent = maxValue;

            RenderSystem.Instance.Register(this);
        }

        public void Render()
        {
            float percentage = barCurrent / barMax;

            backgroundRenderer.Render();

            Vector2 previousScale = transform.Scale;

            transform.Scale = new Vector2(
                originalWidth * percentage,
                previousScale.Y);

            fillRenderer.Render();

            transform.Scale = previousScale;
        }
    }
}