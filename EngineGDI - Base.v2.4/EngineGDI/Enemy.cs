using EngineGDI;
using System;

namespace EngineGDI
{
    public abstract class Enemy
    {
        protected Transform transform = new Transform();
        protected Renderer renderer;
        protected bool isActive = true;

        public Vector2 Pos => transform.Position;
        public Vector2 Size
        {
            get => transform.Scale;
            set => transform.Scale = new Vector2(0.05f, 0.05f);
        }
        public string Sprite => renderer.TexturePath;
        public bool IsActive => isActive;

        /// <summary>Points earned by the player when he kills this enemy.</summary>
        public virtual int PointsOnKill => 0;
        
        public virtual Vector2 RenderScale => Size;

        //events
        public event Action<float>OnPlayerHit;
        public event Action<Vector2>OnSpawnRequested;

        public Enemy(string sprite, Vector2 startPos)
        {
            renderer.TexturePath = sprite;
            transform.Position = startPos;
        }

        public abstract void Update(float deltaTime);

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
        }

    }
}