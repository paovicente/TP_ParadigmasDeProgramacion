using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineGDI
{
    public class Player
    {
        //private
        private Vector2 pos;
        private float speed = 150f;
        private PlayerShoot shooter; //composition
        private Movement movement;
        private string sprite;

        private readonly string[] animationFrames;
        private int frameIndex;
        private float frameTimer;
        private const float AnimationDurationSeconds = 1f; //4 frames in 1 second

        //public getters, can be read from outside the class but not write
        public Vector2 Pos => pos;
        public float Speed => speed;
        public string Sprite => sprite;
        public PlayerShoot Shooter => shooter;
        public Movement Movement => movement;

        public Player(string sprite, Vector2 pos)
        {
            animationFrames = new[]
            {
                "PlayerFrame1.png",
                "PlayerFrame2.png",
                "PlayerFrame3.png",
                "PlayerFrame4.png"
            };

            frameIndex = 0;
            frameTimer = 0f;
            this.sprite = animationFrames[0];
            this.pos = pos;

            shooter = new PlayerShoot();
            movement = new Movement(speed);
        }

        public void Update()
        {
            HandleInput();
            UpdateAnimation(Program.deltaTime);
            shooter.Update(Program.deltaTime);
        }

        private void UpdateAnimation(float deltaTime)
        {
            if (animationFrames.Length == 0)
                return;

            float frameDuration = AnimationDurationSeconds / animationFrames.Length; //0.25s per frame
            frameTimer += deltaTime;

            while (frameTimer >= frameDuration)
            {
                frameTimer -= frameDuration;
                frameIndex = (frameIndex + 1) % animationFrames.Length;
                sprite = animationFrames[frameIndex];
            }
        }

        private void HandleInput()
        {
            Vector2 dir = new Vector2(0, 0);

            if (Engine.IsKeyDown(Keys.Left) || Engine.IsKeyDown(Keys.A))
                dir.X = -1;

            if (Engine.IsKeyDown(Keys.Right) || Engine.IsKeyDown(Keys.D))
                dir.X = 1;

            movement.Move(ref pos, dir, Program.deltaTime);

            if (Engine.OnKeyDown(Keys.Space))
            {
                //shoot
                shooter.Shoot(pos);
            }

        }
    }
}
