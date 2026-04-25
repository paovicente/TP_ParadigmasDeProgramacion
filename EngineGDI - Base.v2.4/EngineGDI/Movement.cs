namespace EngineGDI
{
    public class Movement
    {
        public float Speed;

        public Movement(float speed)
        {
            Speed = speed;
        }

        /// <summary>
        /// The position is passed by reference so it gets updated directly.
        /// </summary>
        public void Move(ref Vector2 position, Vector2 direction, float deltaTime)
        {
            position += direction * Speed * deltaTime;
        }
    }
}