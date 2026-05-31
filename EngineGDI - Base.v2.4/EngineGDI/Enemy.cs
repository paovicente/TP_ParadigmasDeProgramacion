using EngineGDI;
using System;

namespace EngineGDI
{
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

        /// <summary>Points earned by the player when he kills this enemy.</summary>
        public virtual int PointsOnKill => 0;
        
        public virtual Vector2 RenderScale => size;

        //events
        public event Action<float>OnPlayerHit;
        public event Action<Vector2>OnSpawnRequested;

        public Enemy(string sprite, Vector2 startPos)
        {
            this.sprite = sprite;
            this.pos = startPos;
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
            if (pos.Y > Program.SCREEN_HEIGHT)
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