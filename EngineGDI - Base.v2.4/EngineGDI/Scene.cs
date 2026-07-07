using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public abstract class Scene
    {
        public string Name { get; protected set; }
        public string Background { get; protected set; }

        protected Scene(string name, string background)
        {
            Name = name;
            Background = background;
        }


        public virtual void Initialize()
        {

        }


        public virtual void Update(float deltaTime)
        {

        }


        public virtual void Render()
        {
            Engine.Draw(
                Background,
                0,
                0,
                1f,
                0.5f
            );
        }
    }
}
