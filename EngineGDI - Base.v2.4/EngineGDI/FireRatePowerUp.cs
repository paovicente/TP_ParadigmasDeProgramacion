using EngineGDI.EngineGDI;


namespace EngineGDI
{
    public class FireRatePowerUp : PowerUp
    {
        public FireRatePowerUp(Vector2 position)
            : base("FireRatePowerUp.png", position)
        {
        }

        public override void Collect(Player player)
        {
            player.Shooter.FireRate *= 0.7f;
        }
    }
}
