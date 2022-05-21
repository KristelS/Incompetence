using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Incompetence
{
    class Player
    {
        private Vector2 position = new Vector2(500, 300);
        private int speed = 200;
        private Direction direction = Direction.Down;
        private bool isMoving = false;
        private int radius = 16;
        public float cameraZoom = 1;

        private static readonly TimeSpan intervalBetweenPressed1 = TimeSpan.FromMilliseconds(3000);
        private TimeSpan lastTimePressed;

        public SpriteAnimation animation;

        public SpriteAnimation[] animations = new SpriteAnimation[5];
        public Vector2 Position { get { return position; } }
        public int Radius { get { return radius; } }
        public void setX(float newX)
        {
            position.X = newX;
        }
        public void setY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            isMoving = false;

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                direction = Direction.Right;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                direction = Direction.Left;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                direction = Direction.Up;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                direction = Direction.Down;
                isMoving = true;
            }
            if (keyboardState.IsKeyDown(Keys.Z))
            {
                cameraZoom = 3.0F;
            }
            if (keyboardState.IsKeyDown(Keys.C))
            {
                cameraZoom = 1;
            }
            if (keyboardState.IsKeyDown(Keys.B))
            {

                if (lastTimePressed + intervalBetweenPressed1 < gameTime.TotalGameTime)
                {
                    Bomb.bombs.Add(new Bomb(position));
                    lastTimePressed = gameTime.TotalGameTime;
                }
            }
            if (keyboardState.IsKeyDown(Keys.E) && Item.didCollideItem(position))
            {
                Rectangle personRectangle =
                new Rectangle((int)position.X, (int)position.Y,
                32, 32);
                foreach (Item item in Item.items)
                {
                    Rectangle blockRectangle =
                        new Rectangle((int)item.Position.X + 16, (int)item.Position.Y + 16,
                        32, 32);

                    if (personRectangle.Intersects(blockRectangle))
                    {
                        Item.items.Remove(item);
                        break;
                    }

                }
            }


            if (isMoving)
            {
                Vector2 tempPos = position;

                switch (direction)
                {
                    case Direction.Right:
                        tempPos.X += speed * deltaTime;
                        if (!Obstacle.didCollide(tempPos))
                        {
                            position.X += speed * deltaTime;
                        }
                        break;
                    case Direction.Left:
                        tempPos.X -= speed * deltaTime;
                        if (!Obstacle.didCollide(tempPos) && tempPos.X > 0)
                        {
                            position.X -= speed * deltaTime;
                        }
                        break;
                    case Direction.Down:
                        tempPos.Y += speed * deltaTime;
                        if (!Obstacle.didCollide(tempPos))
                        {
                            position.Y += speed * deltaTime;
                        }
                        break;
                    case Direction.Up:
                        tempPos.Y -= speed * deltaTime;
                        if (!Obstacle.didCollide(tempPos) && tempPos.Y > 0)
                        {
                            position.Y -= speed * deltaTime;
                        }
                        break;
                    default:
                        break;
                }
            }

            animation = animations[(int)direction];

            animation.Position = new Vector2(position.X - 48, position.Y - 48);

            if (isMoving)
                animation.Update(gameTime);
            else
                animation.setFrame(0);
        }
    }
}
