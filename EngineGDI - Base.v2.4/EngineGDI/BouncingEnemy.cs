using EngineGDI;
using System;

namespace EngineGDI
{
    public class BouncingEnemy : Enemy
    {
        private Vector2 direction;
        private Movement movement;
        private static Random rng = new Random();

        public override int PointsOnKill => 3;
        public override Vector2 RenderScale => new Vector2(3f, 3f);

        public BouncingEnemy(string sprite, Vector2 startPos)
            : base(sprite, startPos)
        {
            Size = new Vector2(0.01f, 0.01f);

            movement = new Movement(140f);
            PickRandomDirection();
        }

        public override void Update(float deltaTime)
        {
            movement.Move(transform, direction, deltaTime);
            KeepInsideScreen();
            CheckIfOutOfScreen();
        }

        private void PickRandomDirection()
        {
            //returns a number between -1 and 1
            float x = (float)(rng.NextDouble() * 2 - 1);
            float y = (float)(rng.NextDouble() * 2 - 1);
            direction = new Vector2(x, y).Normalize();
        }

        private void KeepInsideScreen()
        {
            if (transform.Position.X < 0 || transform.Position.X > Program.SCREEN_WIDTH) 
                direction.X *= -1;

            if (transform.Position.Y < 0 || transform.Position.Y > Program.SCREEN_HEIGHT) 
                direction.Y *= -1;
        }
    }
}