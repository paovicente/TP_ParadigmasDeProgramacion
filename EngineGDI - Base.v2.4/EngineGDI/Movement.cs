namespace EngineGDI
{
    public class Movement
    {
        public float Speed;

        public Movement(float speed)
        {
            Speed = speed;
        }

        public void Move(Transform transform, Vector2 direction, float deltaTime)
        {
            transform.Position += direction * Speed * deltaTime;
        }
    }
}