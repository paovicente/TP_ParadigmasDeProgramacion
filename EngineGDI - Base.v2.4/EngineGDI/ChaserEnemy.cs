namespace EngineGDI
{
    public class ChaserEnemy : Enemy
    {
        private Movement movement;
        private Player player;

        public ChaserEnemy(string sprite, Vector2 startPos, Player player)
            : base(sprite, startPos)
        {
            this.player = player;
            movement = new Movement(150f);
        }

        public override void Update(float deltaTime)
        {
            Vector2 direction = (player.Pos - pos).Normalize();
            movement.Move(ref pos, direction, deltaTime);
        }
    }

}