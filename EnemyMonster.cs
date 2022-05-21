using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Incompetence
{
    class EnemyMonster
    {
        private Vector2 position;
        protected int health;
        protected int speed;
        protected int radius;

        public static List<EnemyMonster> enemies = new List<EnemyMonster>();

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public int Radius
        {
            get { return radius; }
        }

        public EnemyMonster(Vector2 newPos)
        {
            position = newPos;
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 moveDir = playerPos - position;
            moveDir.Normalize();

            Vector2 tempPos = position;

            tempPos += moveDir * speed * dt;
            if (!Obstacle.didCollide(tempPos))
            {
                position += moveDir * speed * dt;
            }
        }
    }

    class TutorialMonster : EnemyMonster
    {
        public TutorialMonster(Vector2 newPos) : base(newPos)
        {
            speed = 50;
            health = 30;
        }
    }

    class Eye : EnemyMonster
    {
        public Eye(Vector2 newPos) : base(newPos)
        {
            speed = 80;
            radius = 45;
            health = 5;
        }
    }
}
