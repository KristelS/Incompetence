using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incompetence
{
    class Projectile
    {
        private Vector2 position;
        private int speed = 400;
        private int radius = 15;
        protected int damage;
        private Direction direction;
        private bool collided = false;

        public static List<Projectile> projectiles = new List<Projectile>();

        public Projectile(Vector2 newPos, Direction newDir)
        {
            position = newPos;
            direction = newDir;
        }

        public bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }
        public int Damage { get { return damage; } }
        public Direction Direction { get { return direction; } }
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public int Radius
        {
            get
            {
                return radius;
            }
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            switch (direction)
            {
                case Direction.Right:
                    position.X += speed * dt;
                    break;
                case Direction.Left:
                    position.X -= speed * dt;
                    break;
                case Direction.Down:
                    position.Y += speed * dt;
                    break;
                case Direction.Up:
                    position.Y -= speed * dt;
                    break;
                default:
                    break;
            }
        }
    }

    class Sword : Projectile
    {
        public Sword(Vector2 newPos, Direction newDir) : base(newPos, newDir)
        {
            damage = 5;
        }
    }
}
