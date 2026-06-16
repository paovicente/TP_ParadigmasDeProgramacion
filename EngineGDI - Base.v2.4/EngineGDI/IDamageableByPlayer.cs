using System;

namespace EngineGDI
{
    public interface IDamageableByPlayer
    {
        void TakeDamage(int damage);
        bool IsActive { get; }
        void Deactivate();
        int PointsOnKill { get; }
        Transform Transform { get; }
        Vector2 CollisionSize { get; }
    }
}
