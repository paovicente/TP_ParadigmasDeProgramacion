namespace EngineGDI
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(EnemyType type, Vector2 spawnPos, Player player = null)
        {
            switch (type)
            {
                case EnemyType.Chaser:
                    return new ChaserEnemy("ChaserEnemy.png",spawnPos,player);

                case EnemyType.Bouncing:
                    return new BouncingEnemy("BouncingEnemy.png",spawnPos);

                case EnemyType.Spiral:
                    return new SpiralEnemy("SpiralEnemy.png",spawnPos);

                case EnemyType.Boss:
                    return new BossEnemy("BossEnemy.png", spawnPos,player);
            }

            return null;
        }
    }
}