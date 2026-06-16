using EngineGDI;
using System;

namespace EngineGDI
{
    public abstract class Enemy : IRenderable, IDamageableByPlayer
    {
        protected Transform transform;
        protected Renderer renderer;
        protected bool isActive = true;

        public Transform Transform => transform;
        public Renderer Renderer => renderer;

        public Vector2 Pos => transform.Position;

        public Vector2 Size
        {
            get => transform.Scale;
            set => transform.Scale = value;
        }

        public virtual Vector2 CollisionSize => new Vector2(32f, 32f);

        public string Sprite => renderer.TexturePath;
        public bool IsActive => isActive;

        public abstract int PointsOnKill { get; }

        public virtual Vector2 RenderScale => Size;

        public event Action<float> OnPlayerHit;
        public event Action<Vector2> OnSpawnRequested;

        public Enemy(string sprite, Vector2 startPos)
        {
            transform = new Transform();

            transform.Position = startPos;
            transform.Scale = new Vector2(2f, 2f);

            renderer = new Renderer(sprite, transform);

            renderer.OffsetX = 0.5f;
            renderer.OffsetY = 0.5f;

            RenderSystem.Instance.Register(this);
        }

        public abstract void Update(float deltaTime);

        public virtual void Render()
        {
            if (!isActive)
                return;

            renderer.Render();
        }

        protected void NotifyPlayerHit(float damageTime)
        {
            OnPlayerHit?.Invoke(damageTime);
        }

        protected void RequestSpawn(Vector2 spawnPos)
        {
            OnSpawnRequested?.Invoke(spawnPos);
        }

        protected virtual void CheckIfOutOfScreen()
        {
            if (transform.Position.Y > Program.SCREEN_HEIGHT)
                Deactivate();
        }

        public virtual void TakeDamage(int damage)
        {
            Deactivate();
        }

        public virtual void Deactivate()
        {
            isActive = false;
            RenderSystem.Instance.Unregister(this);
        }

    }
}