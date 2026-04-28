namespace EngineGDI
{
    public class ChaserEnemy : Enemy
    {
        private Movement movement;
        private Player player;

        public override int PointsOnKill => 3;

        public ChaserEnemy(string sprite, Vector2 startPos, Player player)
            : base(sprite, startPos)
        {
            this.player = player;
            movement = new Movement(150f);
        }

        public override void Update(float deltaTime)
        {
            Vector2 toPlayer = player.Pos - pos;
            float mag = toPlayer.Magnitude();

            // Evitar Normalize() sobre vector nulo o no finito (quedaría (0,0) y no se mueve).
            Vector2 direction;
            if (float.IsNaN(mag) || float.IsInfinity(mag) || mag < 1e-5f)
            {
                direction = new Vector2(0f, 1f);
            }
            else
            {
                direction = new Vector2(toPlayer.X / mag, toPlayer.Y / mag);
                if (float.IsNaN(direction.X) || float.IsNaN(direction.Y)
                    || float.IsInfinity(direction.X) || float.IsInfinity(direction.Y))
                {
                    direction = new Vector2(0f, 1f);
                }
            }

            movement.Move(ref pos, direction, deltaTime);
        }
    }
}
