using System;
using System.Collections.Generic;

namespace EngineGDI
{
    public static class LevelDatabase
    {
        public static Dictionary<int, LevelData> Levels = new Dictionary<int, LevelData>()
        {
            {
                1,
                new LevelData(
                    1,
                    30f,
                    20,
                    "fondo1.png",
                    new List<EnemyType>
                    {
                        EnemyType.Bouncing,
                        EnemyType.Spiral
                    })
            },
            {
                2,
                new LevelData(
                    2,
                    50f,
                    40,
                    "fondo2.png",
                    new List<EnemyType>
                    {
                        EnemyType.Bouncing,
                        EnemyType.Spiral,
                        EnemyType.Chaser
                    })
            },
            {
                3,
                new LevelData(
                    3,
                    80f,
                    70,
                    "fondo3.png",
                    new List<EnemyType>
                    {
                        EnemyType.Boss
                    })
            }
        };
    }
}
