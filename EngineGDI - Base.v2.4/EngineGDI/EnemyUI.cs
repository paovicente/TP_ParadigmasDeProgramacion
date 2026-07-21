using System;
using System.Drawing;
using System.Numerics;

namespace EngineGDI
{
    public class EnemyUI : IRenderable
    {
        private readonly PlayerStats playerStats;
        private readonly EnemyType enemyType;

        private readonly Transform iconTransform;
        private readonly Transform textTransform;

        private readonly Renderer iconRenderer;
        private readonly TextRenderer textRenderer;

        public EnemyUI(
            PlayerStats stats,
            EnemyType type,
            Vector2 position,
            Vector2 scale)
        {
            playerStats = stats;
            enemyType = type;

            iconTransform = new Transform();
            iconTransform.Position = position;
            iconTransform.Scale = scale;

            textTransform = new Transform();
            textTransform.Position = position + new Vector2(70f, 0f);

            iconRenderer = new Renderer(GetSprite(type), iconTransform);

            textRenderer = new TextRenderer(textTransform)
            {
                FontName = "Arial",
                FontSize = 24,
                Color = Color.White
            };

            RenderSystem.Instance.Register(this);
        }

        public virtual void Render()
        {
            iconRenderer.Render();

            textRenderer.Text = playerStats.GetKills(enemyType).ToString();
            textRenderer.Render();
        }

        private string GetSprite(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Bouncing:
                    return "BouncingEnemy.png";

                case EnemyType.Spiral:
                    return "SpiralEnemy.png";

                case EnemyType.Chaser:
                    return "ChaserEnemy.png";

                case EnemyType.Boss:
                    return "BossEnemy.png";

                default:
                    return "";
            }
        }
    }
}