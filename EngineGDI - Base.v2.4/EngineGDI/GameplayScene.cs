using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineGDI
{
    public class GameplayScene : Scene
    {
        private LevelData levelData;
        private int levelNumber;

        public GameplayScene(int levelNumber, LevelData data): base("Level " + levelNumber, data.Background)
        {
            this.levelNumber = levelNumber;
            this.levelData = data;
        }

        public override void Initialize()
        {
            GameManager.Instance.StartLevel(levelNumber,levelData);
        }

        public override void Update(float deltaTime)
        {
            GameManager.Instance.Update(deltaTime,Program.SCREEN_WIDTH);
        }

        public override void Render()
        {
            Engine.Draw(
                Background,
                0,
                0,
                1f,
                0.64f
            );

            GameManager.Instance.Render();
        }
    }
}
