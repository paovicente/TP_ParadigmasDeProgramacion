using System;

namespace EngineGDI 
{
    public class BossEnemy : Enemy
    {
        private Player player;
        private Movement movement;
        private Vector2 direction;

        private static Random rng = new Random();

        public override int PointsOnKill => 6;
        public override Vector2 RenderScale => new Vector2(0.5f, 0.5f);
        public override Vector2 CollisionSize => new Vector2(64f, 64f);

        public BossEnemy(string sprite, Vector2 startPos, Player player)
            :base(sprite, startPos)
        {
            this.player = player;
            health = 5;

            Size = new Vector2(2f, 2f);
            movement = new Movement(50f);

            PickRandomDirection();
        }

        public override void Update(float deltaTime)
        {
            movement.Move(transform, direction, deltaTime);

            CheckPlayerCollision();

            KeepInsideScreen();
        }

        private void PickRandomDirection()
        {
            float x = (float)(rng.NextDouble() * 2 - 1);
            float y = (float)(rng.NextDouble()* 2 - 1);
            direction = new Vector2(x, y).Normalize();
        }

        private void KeepInsideScreen()
        {
            if (transform.Position.X < 0 || transform.Position.X > Program.SCREEN_WIDTH)
            {
                direction.X *= -1;
            }

            if (transform.Position.Y < 0 || transform.Position.Y > Program.SCREEN_HEIGHT)
            {
                direction.Y *= -1;
            }
        }

        public override void TakeDamage(int dmg)
        {
            health -= dmg;

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isActive = false;

            RequestSpawn(transform.Position);
        }

        private void CheckPlayerCollision()
        {
            float distance =
                (player.Transform.Position - transform.Position).Magnitude();

            float hitDistance = 25f;

            if (distance <= hitDistance)
            {
                NotifyPlayerHit(10f);

                Deactivate();
            }
        }

    }
}
