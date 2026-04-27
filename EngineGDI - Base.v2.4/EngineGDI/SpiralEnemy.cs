using System;

namespace EngineGDI
{
    public class SpiralEnemy : Enemy
    {
        private float angle;
        private float radius;
        private Vector2 center;

        public SpiralEnemy(string sprite, Vector2 startPos)
            : base(sprite, startPos)
        {
            size = new Vector2(0.04f, 0.04f);

            center = startPos;
            radius = 1f;
            angle = 0f;
        }

        public override void Update(float deltaTime)
        {
            angle += 2f * deltaTime;
            radius += 20f * deltaTime;

            center.Y += 50f * deltaTime;

            pos.X = center.X + (float)Math.Cos(angle) * radius;
            pos.Y = center.Y + (float)Math.Sin(angle) * radius;

            CheckIfOutOfScreen();
        }

        protected override void CheckIfOutOfScreen()
        {
            float margin = 50f;

            if (
                pos.X < -margin ||
                pos.X > Program.SCREEN_WIDTH + margin ||
                pos.Y < -margin ||
                pos.Y > Program.SCREEN_HEIGHT + margin
            )
            {
                Deactivate();
            }
        }
    }
}