namespace EngineGDI
{
    public class ChaserEnemy : Enemy
    {
        private Movement movement;
        private Player player;

        public override int PointsOnKill => 4;
        public override Vector2 RenderScale => new Vector2(2f, 2f);
        public override EnemyType Type => EnemyType.Chaser;

        public ChaserEnemy(string sprite, Vector2 startPos, Player player)
            : base(sprite, startPos)
        {
            health = 2;
            this.player = player;
            Size = new Vector2(2f, 2f);
            movement = new Movement(120f);
        }

        public override void Update(float deltaTime)
        {
            Vector2 toPlayer = player.Transform.Position - transform.Position;
            float mag = toPlayer.Magnitude();

            Vector2 direction;

            //to avoid divide by 0
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

            movement.Move(transform, direction, deltaTime);

            CheckPlayerCollision();
        }

        private void CheckPlayerCollision()
        {
            float distance =
                (player.Transform.Position - transform.Position).Magnitude();

            float hitDistance = 25f;

            if (distance <= hitDistance)
            {
                NotifyPlayerHit(5f);

                Deactivate();
            }
        }
    }
}
