using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Threading.Tasks;

namespace Incompetence
{
    enum Direction
    {
        Down,
        Up, 
        Left,
        Right
    }
    public class Game1 : Game
    {
        float timer = 3;
        public static Color color = Color.White;
        public static bool itemsCollectedDone = false;
        public static int itemsCollected = 0;
        public static bool isGameStarted = false;
        public static int mapLevel = 1;
        public static int firstCollision = 0;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D tutorialBackgroundImg;
        Texture2D tutBossBackgroundImg;
        Texture2D bombb;
        Texture2D bombflash;

        Texture2D empty;

        Texture2D splashMenu;
        Texture2D controlSplash;
        Texture2D tree;
        Texture2D tree2;

        Texture2D swordPlaceHolder;
        Texture2D swordPlaceHolderRight;
        Texture2D swordPlaceHolderDown;
        Texture2D swordPlaceHolderLeft;

        Texture2D playerSprite;
        Texture2D playerDown;
        Texture2D playerUp;
        Texture2D playerLeft;
        Texture2D playerRight;
        Texture2D heart;

        Texture2D potionRed;
        Texture2D potionBlue;
        Texture2D potionGreen;

        Texture2D itemPreview;

        Texture2D tutorialMonster;

        SpriteFont gameFont;
        SpriteFont msgFont;

        Player player = new Player();
        Camera camera;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            this.camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tutorialBackgroundImg = Content.Load<Texture2D>("biggerTutMap");
            tutBossBackgroundImg = Content.Load<Texture2D>("tutorialBoss");
            bombb = Content.Load<Texture2D>("bomb");
            bombflash = Content.Load<Texture2D>("bombflash");

            empty = Content.Load<Texture2D>("empty");
            itemPreview = Content.Load<Texture2D>("itemPreview");

            splashMenu = Content.Load<Texture2D>("splash");
            controlSplash = Content.Load<Texture2D>("controlsSplash");
            tree = Content.Load<Texture2D>("tree");
            tree2 = Content.Load<Texture2D>("tree2");

            swordPlaceHolder = Content.Load<Texture2D>("swordPlaceHolder");
            swordPlaceHolderRight = Content.Load<Texture2D>("swordPlaceHolderRight");
            swordPlaceHolderDown = Content.Load<Texture2D>("swordPlaceHolderDown");
            swordPlaceHolderLeft = Content.Load<Texture2D>("swordPlaceHolderLeft");

            potionRed = Content.Load<Texture2D>("potionRed");
            potionGreen = Content.Load<Texture2D>("potionGreen");
            potionBlue = Content.Load<Texture2D>("potionBlue");

            tutorialMonster = Content.Load<Texture2D>("tutBosss");

            gameFont = Content.Load<SpriteFont>("galleryFont");
            msgFont = Content.Load<SpriteFont>("textMsg");

            playerDown = Content.Load<Texture2D>("main_forward");
            playerUp = Content.Load<Texture2D>("mainback");
            playerLeft = Content.Load<Texture2D>("main_left");
            playerRight = Content.Load<Texture2D>("main_right");
            playerSprite = Content.Load<Texture2D>("playerRight");
            player.animations[0] = new SpriteAnimation(playerDown, 3, 10);
            player.animations[1] = new SpriteAnimation(playerUp, 3, 10);
            player.animations[2] = new SpriteAnimation(playerLeft, 3, 10);
            player.animations[3] = new SpriteAnimation(playerRight, 3, 10);

            Item.items.Add(new PotionRed(new Vector2(138 + 320, 68 + 320)));
            Item.items.Add(new PotionBlue(new Vector2(170 + 320, 100 + 320)));
            Item.items.Add(new PotionGreen(new Vector2(202 + 320, 200 + 320)));
            Item.items.Add(new CraftItems(new Vector2(768 + 320, 96 + 320)));

            SplashClass.splashes.Add(new SplashScreen(new Vector2(0, 0)));
            Teleport.teleports.Add(new TeleportTutBoss(new Vector2(500 + 320, 30 + 320)));

            Obstacle.SpawnObstacles();
            foreach (Obstacle tree in Obstacle.obstacles)
            {
                if (tree.GetType() == typeof(Tree))
                {
                    Obstacle.treesBorders.Add(new Border(new Vector2(tree.Position.X + 42, tree.Position.Y + 135))); //+40, +135
                }
                if (tree.GetType() == typeof(Tree2))
                {
                    Obstacle.treesBorders.Add(new Border(new Vector2(tree.Position.X + 40, tree.Position.Y + 125))); //+40, +135
                }
            }



            heart = Content.Load<Texture2D>("heart");

            Item.items.Add(new PotionRed(new Vector2(138, 68)));
            Item.items.Add(new PotionBlue(new Vector2(170, 100)));
            Item.items.Add(new PotionGreen(new Vector2(202, 200)));


            player.animation = player.animations[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            if (isGameStarted)
            {
                
                this.camera.Position = player.Position;
                this.camera.Zoom = player.cameraZoom;
                this.camera.Update(gameTime);
            }
            else this.camera.Position = new Vector2(640, 360);

            

            foreach (Bomb bomb in Bomb.bombs)
            {
                bomb.Update(gameTime);
            }

            foreach (EnemyMonster enemy in EnemyMonster.enemies)
            {
                enemy.Update(gameTime, player.Position);

                // kasti suurused tuleb muuta vastavaks uuele karakterile
                Rectangle personRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, 32, 32);
                Rectangle monsterRectangle = new Rectangle((int)enemy.Position.X - 16, (int)enemy.Position.Y - 16, 48, 48);

                if (personRectangle.Intersects(monsterRectangle))
                {
                    // this is an amazing implementation of knockback
                    player.isHit = true;
                    player.hitDirection = enemy.Direction;
                    
                    player.Health--;
                }
            }

            foreach (Projectile proj in Projectile.projectiles)
            {
                proj.Update(gameTime);
            }

            foreach (Projectile proj in Projectile.projectiles)
            {
                Rectangle personRectangle =
                    new Rectangle((int)proj.Position.X, (int)proj.Position.Y,
                    32, 32);
                foreach (EnemyMonster enemy in EnemyMonster.enemies)
                {
                    Rectangle blockRectangle =
                        new Rectangle((int)enemy.Position.X + 16, (int)enemy.Position.Y + 16,
                        32, 32);

                    if (personRectangle.Intersects(blockRectangle))
                    {
                        proj.Collided = true;
                        enemy.Health -= proj.Damage;
                    }
                }

                if (Obstacle.didCollide(proj.Position))
                {
                    proj.Collided = true;
                }
            }

            Projectile.projectiles.RemoveAll(p => p.Collided);
            EnemyMonster.enemies.RemoveAll(e => e.Health <= 0);

            

            Bomb.bombs.RemoveAll(b => b.state == 2);
            Item.items.RemoveAll(i => i.IsPickedUp == true);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(this.camera);

            if (mapLevel == 2)
            {
                _spriteBatch.Draw(tutBossBackgroundImg, new Vector2(0, 0), Color.White);

            }
            else
                _spriteBatch.Draw(tutorialBackgroundImg, new Vector2(0, 0), Color.White);




            foreach (Bomb bomb in Bomb.bombs)
            {
                _spriteBatch.Draw(bombb, new Vector2(bomb.Position.X + 16 - bomb.Radius, bomb.Position.Y + 16 - bomb.Radius), Color.White);
                if (bomb.state == 1)
                {
                    _spriteBatch.Draw(bombflash, new Vector2(bomb.Position.X + 16 - bomb.Radius, bomb.Position.Y + 16 - bomb.Radius), Color.White);


                }
            }

            foreach (Projectile proj in Projectile.projectiles)
            {
                //todo: make sprites draw by TypeOf (since we have different weapons)
                Texture2D spriteToDraw;
                if (proj.GetType() == typeof(Sword))
                {
                    if (proj.Direction == Direction.Right)
                    {
                        spriteToDraw = swordPlaceHolderRight;
                    }
                    else if (proj.Direction == Direction.Left)
                    {
                        spriteToDraw = swordPlaceHolderLeft;
                    }
                    else if (proj.Direction == Direction.Down)
                    {
                        spriteToDraw = swordPlaceHolderDown;
                    }
                    else
                        spriteToDraw = swordPlaceHolder;
                }
                else
                    spriteToDraw = potionRed;

                _spriteBatch.Draw(spriteToDraw, new Vector2(proj.Position.X - proj.Radius, proj.Position.Y - proj.Radius), Color.White);

            }

            //foreach (Teleport tele in Teleport.teleports)
            //{
            //    Texture2D spriteToDraw;
            //    if (tele.GetType() == typeof(TeleportTutBoss))
            //    {
            //        spriteToDraw = bombb;
            //    }
            //    else
            //        spriteToDraw = bombflash;
            //    _spriteBatch.Draw(spriteToDraw, tele.Position, Color.White);
            //}

            foreach (EnemyMonster enemy in EnemyMonster.enemies)
            {
                Texture2D spriteToDraw;
                if (enemy.GetType() == typeof(TutorialMonster))
                {
                    spriteToDraw = tutorialMonster;
                }
                else
                    spriteToDraw = potionRed;

                _spriteBatch.Draw(spriteToDraw, new Vector2(enemy.Position.X - 16, enemy.Position.Y - 16), Color.White);
            }

            foreach (Item it in Item.items)
            {
                Texture2D spriteToDraw;
                if (it.GetType() == typeof(PotionRed))
                {
                    spriteToDraw = potionRed;
                }
                else if (it.GetType() == typeof(PotionBlue))
                {
                    spriteToDraw = potionBlue;
                }
                else if (it.GetType() == typeof(PotionGreen))
                    spriteToDraw = potionGreen;
                else if (it.GetType() == typeof(CraftItems))
                {
                    spriteToDraw = empty;
                }
                else
                    spriteToDraw = bombb;


                _spriteBatch.Draw(spriteToDraw, it.Position, Color.White);
            }

            

            //to see the borders




            //_spriteBatch.Draw(player_Sprite, player.Position, Color.White);
            player.animation.Draw(_spriteBatch);

            if (mapLevel == 1)
                _spriteBatch.Draw(tree, new Vector2(350+320, 200+320), Color.White);


            foreach (Obstacle o in Obstacle.obstacles)
            {
                Texture2D spriteToDraw;
                if (o.GetType() == typeof(Nothing))
                {
                    spriteToDraw = empty;
                }
                else if (o.GetType() == typeof(Tree2))
                {
                    spriteToDraw = tree2;
                }
                else
                    spriteToDraw = tree;
                _spriteBatch.Draw(spriteToDraw, o.Position, Color.White);
            }
            //foreach (Obstacle o in Obstacle.obstacles)
            //{
            //    Texture2D spriteToDraw;
            //    if (o.GetType() == typeof(Border))
            //        spriteToDraw = bombb;
            //    else
            //        spriteToDraw = bombflash;
            //    _spriteBatch.Draw(spriteToDraw, o.Position, Color.White);
            //}
            if (!itemsCollectedDone)
                _spriteBatch.DrawString(gameFont, itemsCollected + " / 3", new Vector2(this.camera.Position.X -275, this.camera.Position.Y -137), color);

            if (!itemsCollectedDone)
            {
                _spriteBatch.Draw(itemPreview, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);
                
            }
            if (itemsCollectedDone)
            {
                _spriteBatch.Draw(itemPreview, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);
                _spriteBatch.Draw(swordPlaceHolder, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);
            }

            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (firstCollision == 1)
            {
                _spriteBatch.DrawString(msgFont, "Yep, that's a wall", new Vector2(player.Position.X - 30, player.Position.Y - 50), Color.Purple);
                timer -= deltaTime;
                if (timer < 1)
                    firstCollision = 2;
            }

            for (int i = 0; i < player.Health; i++)
            {
                _spriteBatch.Draw(heart, new Vector2(this.camera.Position.X - 315 + i * 45, this.camera.Position.Y - 177), Color.White);
            }

            //add here a picture instead of the new item

            foreach (SplashClass splash in SplashClass.splashes)
            {
                Texture2D spriteToDraw;
                if (splash.GetType() == typeof(SplashScreen))
                {
                    spriteToDraw = splashMenu;
                }
                else
                    spriteToDraw = controlSplash;
                _spriteBatch.Draw(spriteToDraw, splash.Position, Color.White);
            }

            

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
