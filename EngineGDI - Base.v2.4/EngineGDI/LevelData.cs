using System.Collections.Generic;

namespace EngineGDI
{
    public class LevelData
    {
        public int LevelNumber { get; private set; }

        public float Duration { get; private set; }

        public int PointsToWin { get; private set; }

        public string Background { get; private set; }

        public List<EnemyType> Enemies { get; private set; }
        public LevelData(int levelNumber,float duration,int pointsToWin,string background,List<EnemyType> enemies)
        {
            LevelNumber = levelNumber;
            Duration = duration;
            PointsToWin = pointsToWin;
            Background = background;
            Enemies = enemies;
        }
    }
}