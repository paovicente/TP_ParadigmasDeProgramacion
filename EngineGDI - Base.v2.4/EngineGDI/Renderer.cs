namespace EngineGDI
{
    public class Renderer
    {
        public string TexturePath { get; set; }

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        private readonly Transform transform;

        public Renderer(string texturePath, Transform transform)
        {
            TexturePath = texturePath;
            this.transform = transform;
        }

        public void Render()
        {
            Engine.Draw(
                TexturePath,
                transform.Position.X,
                transform.Position.Y,
                transform.Scale.X,
                transform.Scale.Y,
                transform.Rotation,
                OffsetX,
                OffsetY
            );
        }
    }
}
