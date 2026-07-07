using EngineGDI.EngineGDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public static class PowerUpFactory
    {
        private static Random random = new Random();

        public static PowerUp CreateRandomPowerUp(Vector2 position)
        {
            switch (random.Next(2))
            {
                case 0:
                    return new DamagePowerUp(position);

                case 1:
                    return new FireRatePowerUp(position);

                default:
                    return new DamagePowerUp(position);
            }
        }
    }
}
