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

        public BouncingEnemy(string sprite, Vector2 startPos)
            : base(sprite, startPos)
        {
            this.size = new Vector2(0.01f, 0.01f);

            movement = new Movement(140f);
            PickRandomDirection();
        }

        public override void Update(float deltaTime)
        {
            movement.Move(ref pos, direction, deltaTime);
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
            if (pos.X < 0 || pos.X > Program.SCREEN_WIDTH) 
                direction.X *= -1;

            if (pos.Y < 0 || pos.Y > Program.SCREEN_HEIGHT) 
                direction.Y *= -1;
        }
    }
}