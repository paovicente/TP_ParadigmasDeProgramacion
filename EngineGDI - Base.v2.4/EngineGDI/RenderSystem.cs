using System.Collections.Generic;

namespace EngineGDI
{
    public class RenderSystem
    {
        private static RenderSystem instance;

        public static RenderSystem Instance
        {
            get
            {
                if (instance == null)
                    instance = new RenderSystem();

                return instance;
            }
        }

        private List<IRenderable> renderables;

        private RenderSystem()
        {
            renderables = new List<IRenderable>();
        }

        public void Register(IRenderable renderable)
        {
            if (!renderables.Contains(renderable))
            {
                renderables.Add(renderable);
            }
        }

        public void Unregister(IRenderable renderable)
        {
            renderables.Remove(renderable);
        }

        public void RenderAll()
        {
            foreach (IRenderable renderable in renderables)
            {
                renderable.Render();
            }
        }

        public void Clear()
        {
            renderables.Clear();
        }
    }
}