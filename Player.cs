using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Incompetence
{
    class Player
    {
        private Vector2 position = new Vector2(500+320, 300+320);
        private int speed = 200;
        private Direction direction = Direction.Down;

        public bool isMoving = false;
        public float timer;
      
        public bool isHit = false;
        public bool isDead = false;
        public Direction hitDirection;

        private int radius = 16;

        private KeyboardState kStateOld = Keyboard.GetState();
        public float cameraZoom = 2F;

        private int health = 3;

        private static readonly TimeSpan intervalBetweenPressed1 = TimeSpan.FromMilliseconds(3000);
        private TimeSpan lastTimePressed;

        public SpriteAnimation animation;

        public SpriteAnimation[] animations = new SpriteAnimation[5];
       
        public Vector2 Position { get { return position; } }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

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
            if (keyboardState.IsKeyDown(Keys.G) && Game1.isGameStarted == false)
            {
                SplashScreen.splashes.Add(new SplashHelp(new Vector2(0, 0)));
            }

            if (keyboardState.IsKeyDown(Keys.Enter) && Game1.isGameStarted == false)
            {
                
                Game1.isGameStarted = true;
                SplashScreen.splashes.RemoveAll(s => s.Position == new Vector2(0, 0));

            }

            if (Game1.isGameStarted && !isDead)
            {

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

                if (keyboardState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
                {
                    if (Game1.itemsCollectedDone)
                        Projectile.projectiles.Add(new Sword(position, direction));
                }

                kStateOld = keyboardState;
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
                            if (item.GetType() != typeof(CraftItems))
                            {
                                Item.items.Remove(item);
                                if (item.Radius == 1) //craft-item specific indenticator
                                    Game1.itemsCollected += 1;
                                if (Game1.itemsCollected == 3)
                                {
                                    Game1.color = Color.Green;
                                }
                                
                                break;
                            }
                            else
                            {
                                //item merging code here
                                Game1.itemsCollectedDone = true;
                                Game1.color = Color.White;
                            }
                        }

                    }
                }

                timer -= deltaTime;
                if (isHit)

            {
                switch (hitDirection)
                {
                    case Direction.Right:
                            if (!Obstacle.didCollide(new Vector2(position.X - 35, position.Y)) && !Obstacle.didCollide(new Vector2(position.X - 70, position.Y)) && position.X - 35 < 928 + 332 && position.X - 70 < 928 + 332)
                                position.X -= 80;
                        isHit = false;
                        break;
                    case Direction.Left:
                            if (!Obstacle.didCollide(new Vector2(position.X + 35, position.Y)) && !Obstacle.didCollide(new Vector2(position.X + 70, position.Y)) && position.X + 35 > 320 && position.X + 70 > 320)
                                position.X += 80;
                            isHit = false;
                        break;
                    case Direction.Up:
                            if (!Obstacle.didCollide(new Vector2(position.X , position.Y - 35)) && !Obstacle.didCollide(new Vector2(position.X , position.Y - 70)) && position.Y - 35 < 608 + 320 && position.Y - 70 < 608 + 320)
                                position.Y -= 80; 
                            isHit = false;
                        break;
                    case Direction.Down:
                            if (!Obstacle.didCollide(new Vector2(position.X, position.Y + 35)) && !Obstacle.didCollide(new Vector2(position.X, position.Y + 70)) && position.Y + 35 > 320 && position.Y + 70 > 320)
                                position.Y += 80;
                            isHit = false;
                        break;
                    default:
                        break;
                }
            }

            animation = animations[(int)direction];
              
                if (isMoving)
                {
                    Vector2 tempPos = position;
                    Rectangle personRectangle =
                                    new Rectangle((int)position.X, (int)position.Y,
                                    16, 48);
                    switch (direction)
                    {
                        case Direction.Right:
                            tempPos.X += speed * deltaTime;
                            if (!Obstacle.didCollide(tempPos) && tempPos.X < 928 + 332)
                            {
                                if (tempPos.X > 928 + 322)
                                {
                                    if (Game1.firstCollision == 0)
                                        Game1.firstCollision = 1;
                                }
                                position.X += speed * deltaTime;
                                if (Teleport.didCollideTeleports(tempPos))
                                {
                                    
                                    foreach (Teleport tele in Teleport.teleports)
                                    {
                                        Rectangle blockRectangle =
                                            new Rectangle((int)tele.Position.X + 16, (int)tele.Position.Y + 16,
                                            32, 32);

                                        if (personRectangle.Intersects(blockRectangle))
                                        {
                                            Teleport.teleports.Remove(tele);
                                            Game1.mapLevel = 2;
                                            ChangeMap();
                                            setX(500+320);
                                            setY(300+320);
                                            break;
                                        }

                                    }
                                }
                            }
                            break;
                        case Direction.Left:
                            tempPos.X -= speed * deltaTime;
                            if (!Obstacle.didCollide(tempPos) && tempPos.X > 320)
                            {
                                if (tempPos.X < 321)
                                {
                                    if (Game1.firstCollision == 0)
                                        Game1.firstCollision = 1;
                                }
                                position.X -= speed * deltaTime;
                                if (Teleport.didCollideTeleports(tempPos))
                                {

                                    foreach (Teleport tele in Teleport.teleports)
                                    {
                                        Rectangle blockRectangle =
                                            new Rectangle((int)tele.Position.X + 16, (int)tele.Position.Y + 16,
                                            32, 32);

                                        if (personRectangle.Intersects(blockRectangle))
                                        {
                                            Teleport.teleports.Remove(tele);
                                            Game1.mapLevel = 2;
                                            ChangeMap();
                                            setX(500+320);
                                            setY(300+320);
                                            break;
                                        }

                                    }
                                }
                            }
                            break;
                        case Direction.Down:
                            tempPos.Y += speed * deltaTime;
                            if (!Obstacle.didCollide(tempPos) && tempPos.Y < 608 + 320)
                            {
                                if (tempPos.Y > 608 + 310)
                                {
                                    if (Game1.firstCollision == 0)
                                        Game1.firstCollision = 1;
                                }
                                position.Y += speed * deltaTime;
                                if (Teleport.didCollideTeleports(tempPos))
                                {

                                    foreach (Teleport tele in Teleport.teleports)
                                    {
                                        Rectangle blockRectangle =
                                            new Rectangle((int)tele.Position.X + 16, (int)tele.Position.Y + 16,
                                            32, 32);

                                        if (personRectangle.Intersects(blockRectangle))
                                        {
                                            Teleport.teleports.Remove(tele);
                                            Game1.mapLevel = 2;
                                            ChangeMap();
                                            setX(500+320);
                                            setY(300+320);
                                            break;
                                        }

                                    }
                                }
                            }
                            break;
                        case Direction.Up:
                            tempPos.Y -= speed * deltaTime;
                            if (!Obstacle.didCollide(tempPos) && tempPos.Y > 320)
                            {
                                if (tempPos.Y < 321)
                                {
                                    if (Game1.firstCollision == 0)
                                        Game1.firstCollision = 1;
                                }
                                position.Y -= speed * deltaTime;
                                if (Teleport.didCollideTeleports(tempPos))
                                {
                                    if (tempPos.Y > 608 + 320)
                                {
                                    if (Game1.firstCollision == 0)
                                        Game1.firstCollision = 1;
                                }
                                    foreach (Teleport tele in Teleport.teleports)
                                    {
                                        Rectangle blockRectangle =
                                            new Rectangle((int)tele.Position.X + 16, (int)tele.Position.Y + 16,
                                            32, 32);

                                        if (personRectangle.Intersects(blockRectangle))
                                        {
                                            Teleport.teleports.Remove(tele);
                                            Game1.mapLevel = 2;
                                            ChangeMap();
                                            setX(500+320);
                                            setY(300+320);
                                            break;
                                        }

                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                animation = animations[(int)direction];

                animation.Position = new Vector2(position.X - 16, position.Y - 24);

                if (isMoving)
                    animation.Update(gameTime);
                else
                    animation.setFrame(1);

            }

            
        }
        public static void ChangeMap()
        {
            Projectile.projectiles.Clear();
            EnemyMonster.enemies.Clear();
            Bomb.bombs.Clear();
            Item.items.Clear();
            Obstacle.obstacles.Clear();
            Obstacle.treesBorders.Clear();
            EnemyMonster.enemies.Add(new TutorialMonster(new Vector2(590, 590)));
            Obstacle.SpawnLevel2Obstacles();
        }
    }
}
