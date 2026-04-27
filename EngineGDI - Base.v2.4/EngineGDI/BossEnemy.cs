namespace EngineGDI 
{
    public class BossEnemy : Enemy
    {
        private int health;
        private Player player;

        public BossEnemy(string sprite, Vector2 startPos, Player player)
            :base(sprite, startPos)
        {
            this.player = player;
            health = 10;
        }

        public override void Update(float deltaTime)
        {
            
        }

        public void TakeDamage(int dmg)
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

            SpawnMinions();
        }

        private void SpawnMinions()
        {
            for (int i = 0; i < 5; i++)
            {
                //to see later how to do it
            }
        }
    }
}
