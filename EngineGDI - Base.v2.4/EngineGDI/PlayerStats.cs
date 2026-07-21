using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public class PlayerStats
    {
        private readonly Dictionary<EnemyType, int> kills =
        new Dictionary<EnemyType, int>();

        public PlayerStats()
        {
            Enemy.EnemyKilled += RegisterKill;
        }

        public void RegisterKill(EnemyType type)
        {
            if (!kills.ContainsKey(type))
                kills[type] = 0;

            kills[type]++;
        }

        public int GetKills(EnemyType type)
        {
            if (!kills.ContainsKey(type))
                return 0;

            return kills[type];
        }
    }
}