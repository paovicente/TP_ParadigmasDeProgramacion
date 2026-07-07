namespace EngineGDI
{
    namespace EngineGDI
    {
        public abstract class PowerUp : ICollectable, IRenderable
        {
            protected Transform transform;
            protected Renderer renderer;
            protected bool isActive;
            protected float fallSpeed = 50f;

            public Transform Transform => transform;

            public bool IsActive => isActive;

            public virtual Vector2 CollisionSize => new Vector2(32f, 32f);

            public PowerUp(string sprite, Vector2 position)
            {
                transform = new Transform();
                transform.Position = position;
                transform.Scale = new Vector2(3.5f, 3.5f);

                renderer = new Renderer(sprite, transform);

                isActive = true;

                RenderSystem.Instance.Register(this);
            }

            public virtual void Update(float deltaTime)
            {
                if (!isActive)
                    return;

                Transform.Position += new Vector2(0f, fallSpeed * deltaTime);

                if (Transform.Position.Y > Program.SCREEN_HEIGHT + 50)
                {
                    Deactivate();
                }
            }

            public abstract void Collect(Player player);

            public void Deactivate()
            {
                isActive = false;
                RenderSystem.Instance.Unregister(this);
            }

            public void Render()
            {
                if (!isActive)
                    return;

                renderer.Render();
            }
        }
    }
}
