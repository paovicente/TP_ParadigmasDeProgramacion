using System.Collections.Generic;

namespace EngineGDI
{
    public class LevelData
    {
        public float Duration { get; private set; }

        public int PointsToWin { get; private set; }

        public List<EnemyType> Enemies { get; private set; }

        public LevelData(float duration,int pointsToWin,List<EnemyType> enemies)
        {
            Duration = duration;
            PointsToWin = pointsToWin;
            Enemies = enemies;
        }
    }
}