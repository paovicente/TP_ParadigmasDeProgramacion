using System;

namespace EngineGDI
{
    public class SpiralEnemy : Enemy
    {
        private float angle;
        private float radius;
        private Vector2 center;

        public override int PointsOnKill => 5;
        public override Vector2 RenderScale => new Vector2(3f, 3f);
        public override EnemyType Type => EnemyType.Spiral;
        public SpiralEnemy(string sprite, Vector2 startPos)
            : base(sprite, startPos)
        {
            health = 2;
            Size = new Vector2(3f, 3f);

            center = startPos;
            radius = 1f;
            angle = 0f;
        }

        public override void Update(float deltaTime)
        {
            angle += 2f * deltaTime;
            radius += 20f * deltaTime;

            center.Y += 50f * deltaTime;
            transform.Position = new Vector2
                (
                center.X + (float)Math.Cos(angle) * radius,
                center.Y + (float)Math.Sin(angle) * radius
                );

            CheckIfOutOfScreen();
        }

        protected override void CheckIfOutOfScreen()
        {
            float margin = 50f;

            if (
                transform.Position.X < -margin ||
                transform.Position.X > Program.SCREEN_WIDTH + margin ||
                transform.Position.Y < -margin ||
                transform.Position.Y > Program.SCREEN_HEIGHT + margin
            )
            {
                Deactivate();
            }
        }
    }
}