using EngineGDI;
using System;

namespace EngineGDI
{
    /*public class Enemy
    {
        private Vector2 pos;
        private Vector2 size = new Vector2(16, 16);
        private Movement movement;
        private string sprite;
        private Vector2 direction;
        private bool isActive = true;
        
        private static Random rng = new Random();

        public Vector2 Pos => pos;
        public Vector2 Size => size;
        public string Sprite => sprite;
        public bool IsActive => isActive;

        public Enemy(string sprite, Vector2 startPos)
        {
            this.sprite = sprite;
            this.pos = startPos;

            movement = new Movement(120f);

            PickRandomDirection();

            isActive = true;
        }

        public void Update(float deltaTime)
        {
            movement.Move(ref pos, direction, deltaTime);

            KeepInsideScreen();

            /*if (pos.Y > Program.SCREEN_HEIGHT)
            {
                isActive = false;
            }
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
            //simple bounce - basic enemy
            if (pos.X < 0 || pos.X > Program.SCREEN_WIDTH) direction.X *= -1;
            if (pos.Y < 0 || pos.Y > Program.SCREEN_HEIGHT) direction.Y *= -1;
        }

        public void Deactivate()
        {
            isActive = false;
        }
    }*/
    public abstract class Enemy
    {
        protected Vector2 pos;
        protected Vector2 size = new Vector2(0.05f, 0.05f);
        protected string sprite;
        protected bool isActive = true;

        public Vector2 Pos => pos;
        public Vector2 Size => size;
        public string Sprite => sprite;
        public bool IsActive => isActive;

        public Enemy(string sprite, Vector2 startPos)
        {
            this.sprite = sprite;
            this.pos = startPos;
        }

        public abstract void Update(float deltaTime);

        protected virtual void CheckIfOutOfScreen()
        {
            if (pos.Y > Program.SCREEN_HEIGHT)
                Deactivate();
        }

        public virtual void Deactivate()
        {
            isActive = false;
        }

    }
}
