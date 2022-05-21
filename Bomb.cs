using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Incompetence
{
    class Bomb
    {
        public float timer = 2;
        public int state = 0;
        private Vector2 position;
        private int radius = 16;
        public float animInterval = 20f;
        public float animTimer = 0f;
        public int currentFrame, spriteWidth, spriteHeight;
        public bool isVisible = true;
        public Rectangle sourceRect;
        public Vector2 origin;

        public static List<Bomb> bombs = new List<Bomb>();

        public SpriteAnimation bombAnim;
        public Bomb(Vector2 newPos)
        {
            position = newPos;
        }

        public Vector2 Position { get { return position; } }

        public int Radius { get { return radius; } }
        
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            animTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            foreach (Bomb bomb in bombs)
            {
                bomb.timer -= dt;

                if (bomb.timer < 1)
                {
                    bomb.state = 1;
                    if (bomb.timer < 0.5)
                    {
                        bomb.state = 2;
                    }

                }


            }

            foreach (Bomb bomb in bombs)
            {
                if (animTimer > animInterval)
                {
                    currentFrame++;
                    animTimer = 0f;
                }
            }

            
        }


    }
}
