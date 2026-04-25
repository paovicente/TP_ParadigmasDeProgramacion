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

        //public getters, can be read from outside the class but not write
        public Vector2 Pos => pos;
        public float Speed => speed;
        public string Sprite => sprite;
        public PlayerShoot Shooter => shooter;
        public Movement Movement => movement;

        public Player(string sprite, Vector2 pos)
        {
            this.sprite = sprite;
            this.pos = pos;

            shooter = new PlayerShoot();
            movement = new Movement(speed);
        }

        public void Update()
        {
            HandleInput();
            shooter.Update(Program.deltaTime);
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
