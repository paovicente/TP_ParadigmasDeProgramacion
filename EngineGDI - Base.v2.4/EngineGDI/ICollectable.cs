using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public interface ICollectable
    {
        bool IsActive { get; }

        Transform Transform { get; }

        Vector2 CollisionSize { get; }

        void Collect(Player player);

        void Deactivate();

        void Update(float deltaTime);
    }
}
