namespace EngineGDI
{
    public class Projectile: IPoolable
    {
        private readonly Transform transform;
        private readonly Renderer renderer;

        private Vector2 direction;
        private float speed;
        private bool isActive;
        private string sprite;

        //encapsulation
        public Transform Transform => transform;
        public Renderer Renderer => renderer;
        public bool IsActive => isActive;

        public Projectile(Renderer renderer, float speed)
        {
            renderer.TexturePath = sprite;
            this.speed = speed;
            isActive = false;
        }

        public void Activate(Vector2 startPosition, Vector2 dir)
        {
            transform.Position = startPosition;
            direction = dir.Normalize();
            isActive = true;
        }

        public void Update(float deltaTime)
        {
            if (!isActive) return;

            transform.Position += direction * speed * deltaTime;

            if (transform.Position.Y < 0 || transform.Position.Y > Program.SCREEN_HEIGHT || transform.Position.X < 0 || transform.Position.X > Program.SCREEN_WIDTH)
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