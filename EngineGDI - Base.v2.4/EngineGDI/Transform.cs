namespace EngineGDI
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Transform()
        {
            Position = new Vector2(0f, 0f);
            Rotation = 0f;
            Scale = new Vector2(1f, 1f);
        }
    }
}

