using EngineGDI.EngineGDI;

namespace EngineGDI
{
    /*with the power-up the boss is defeated in 3 shots instead of 5.
     chaser defeated in 2 shots instead of 3
     bouncing and spiral defeated in 1 shot instead of 2
     */

    public class DamagePowerUp : PowerUp
    {
        public DamagePowerUp(Vector2 position)
            : base("DamagePowerUp.png", position)
        {
        }

        public override void Collect(Player player)
        {
            player.Shooter.Damage += 1;
            player.Shooter.BulletSprite = "BulletDamagePower.png";
        }
    }
}
