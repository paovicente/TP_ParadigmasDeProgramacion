namespace EngineGDI
{
    public class Projectile
    {
        private Vector2 position;
        private Vector2 direction;
        private Vector2 size = new Vector2(32, 32);
        private float speed;
        private bool isActive;
        private string sprite;

        //encapsulation
        public Vector2 Position => position;
        public Vector2 Size => size;
        public bool IsActive => isActive;
        public string Sprite => sprite;

        public Projectile(string sprite, float speed)
        {
            this.sprite = sprite;
            this.speed = speed;
            isActive = false;
        }

        //activate the projectile
        public void Activate(Vector2 startPosition, Vector2 dir)
        {
            position = startPosition;
            direction = dir.Normalize();
            isActive = true;
        }

        //movement
        public void Update(float deltaTime)
        {
            if (!isActive) return;

            position += direction * speed * deltaTime;

            if (position.Y < 0 || position.Y > Program.SCREEN_HEIGHT || position.X < 0 || position.X > Program.SCREEN_WIDTH)
            {
                Deactivate();
            }
        }

        public void Deactivate()
        {
            isActive = false;
        }
    }
}