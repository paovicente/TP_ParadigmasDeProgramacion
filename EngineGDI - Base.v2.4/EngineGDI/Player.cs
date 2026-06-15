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
        private readonly Transform transform;
        private readonly Renderer renderer;

        private float speed = 150f;
        private PlayerShoot shooter; // composition
        private Movement movement;

        private readonly Animation animation;

        public Transform Transform => transform;
        public Renderer Renderer => renderer;
        public float Speed => speed;
        public string Sprite => renderer.TexturePath;
        public PlayerShoot Shooter => shooter;
        public Movement Movement => movement;
        public Animation Animation => animation;

        public Player()
        {
            transform = new Transform();
            transform.Position = new Vector2(40, 450);
            transform.Scale = new Vector2(3f, 3f);

            string[] frames =
            {
                "PlayerFrame1.png",
                "PlayerFrame2.png",
                "PlayerFrame3.png",
                "PlayerFrame4.png"
            };

            renderer = new Renderer("PlayerFrame1.png", transform);
            animation = new Animation(renderer, frames, 0.25f);          

            shooter = new PlayerShoot();
            movement = new Movement(speed);
        }

        public void Update()
        {
            HandleInput();
            animation.Update(Program.deltaTime);
            shooter.Update(Program.deltaTime);
        }

        private void HandleInput()
        {
            Vector2 dir = new Vector2(0, 0);

            if (Engine.IsKeyDown(Keys.Left) || Engine.IsKeyDown(Keys.A))
                dir.X = -1;

            if (Engine.IsKeyDown(Keys.Right) || Engine.IsKeyDown(Keys.D))
                dir.X = 1;

            movement.Move(transform, dir, Program.deltaTime);

            if (Engine.OnKeyDown(Keys.Space))
            {
                //shoot
                shooter.Shoot(transform.Position + new Vector2(20f, -10f));
            }

        }

        public void Render()
        {
            renderer.Render();
            shooter.Render();
        }
    }
}
