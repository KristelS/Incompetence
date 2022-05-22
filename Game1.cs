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
        float timer2 = 1;
        float npcSpeechTimer = 20;
        public static Color color = Color.White;
        public static Color pcolor = Color.White;
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
        Texture2D deadSplash;

        Texture2D banana;
        Texture2D ham;
        Texture2D fork;


        Texture2D tree;
        Texture2D tree2;

        Texture2D sword;
        Texture2D swordGrayscale;
        Texture2D swordPlaceHolderRight;
        Texture2D swordPlaceHolderDown;
        Texture2D swordPlaceHolderLeft;


        Texture2D playerSprite;
        Texture2D playerDown;
        Texture2D playerUp;
        Texture2D playerLeft;
        Texture2D playerRight;
        Texture2D heart;

        Texture2D textBubble;

        Texture2D potionRed;
        Texture2D potionBlue;
        Texture2D potionGreen;

        Texture2D itemPreview;

        Texture2D tutorialMonster;

        SpriteFont gameFont;
        SpriteFont msgFont;
        SpriteFont bubbleFont;

        Player player = new Player();
        Camera camera;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
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


            tutorialBackgroundImg = Content.Load<Texture2D>("maphopefullyFinal");
            tutBossBackgroundImg = Content.Load<Texture2D>("newTutBossBackground");

            bombb = Content.Load<Texture2D>("bomb");
            bombflash = Content.Load<Texture2D>("bombflash");

            empty = Content.Load<Texture2D>("empty");
            itemPreview = Content.Load<Texture2D>("itemPreview");

            banana = Content.Load<Texture2D>("banana");
            ham = Content.Load<Texture2D>("ham");
            fork = Content.Load<Texture2D>("fork");

            textBubble = Content.Load<Texture2D>("textbubl2");

            splashMenu = Content.Load<Texture2D>("Amenu");
            controlSplash = Content.Load<Texture2D>("controlsSplash");
            deadSplash = Content.Load<Texture2D>("deadSplash");

            tree = Content.Load<Texture2D>("tree");
            tree2 = Content.Load<Texture2D>("tree2");

            sword = Content.Load<Texture2D>("MainWeapon");
            swordGrayscale = Content.Load<Texture2D>("WeaponGrayscale");
            swordPlaceHolderRight = Content.Load<Texture2D>("MainWeaponRight");
            swordPlaceHolderDown = Content.Load<Texture2D>("MainWeaponDown");
            swordPlaceHolderLeft = Content.Load<Texture2D>("MainWeaponLeft");

            potionRed = Content.Load<Texture2D>("potionRed");
            potionGreen = Content.Load<Texture2D>("potionGreen");
            potionBlue = Content.Load<Texture2D>("potionBlue");

            tutorialMonster = Content.Load<Texture2D>("tutBosss");

            gameFont = Content.Load<SpriteFont>("galleryFont");
            msgFont = Content.Load<SpriteFont>("textMsg");
            bubbleFont = Content.Load<SpriteFont>("speech");


            playerDown = Content.Load<Texture2D>("main_forward");
            playerUp = Content.Load<Texture2D>("mainback");
            playerLeft = Content.Load<Texture2D>("main_left");
            playerRight = Content.Load<Texture2D>("main_right");
            playerSprite = Content.Load<Texture2D>("player");
            player.animations[0] = new SpriteAnimation(playerDown, 3, 10);
            player.animations[1] = new SpriteAnimation(playerUp, 3, 10);
            player.animations[2] = new SpriteAnimation(playerLeft, 3, 10);
            player.animations[3] = new SpriteAnimation(playerRight, 3, 10);

            Item.items.Add(new PotionRed(new Vector2(20*32, 11*32)));
            Item.items.Add(new PotionBlue(new Vector2(23*32,25*32)));
            Item.items.Add(new PotionGreen(new Vector2(38*32,20*32)));

            Item.items.Add(new Banana(new Vector2(14*32,21*32)));
            Item.items.Add(new Ham(new Vector2(23*32,17*32)));
            Item.items.Add(new Fork(new Vector2(34*32,19*32)));

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
                enemy.Update(gameTime, player.Position, player.isDead);

                // kasti suurused tuleb muuta vastavaks uuele karakterile
                Rectangle personRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, 32, 32);
                Rectangle monsterRectangle = new Rectangle((int)enemy.Position.X - 16, (int)enemy.Position.Y - 16, 48, 48);

                if (personRectangle.Intersects(monsterRectangle))
                {
                    // this is an amazing implementation of knockback
                    player.isHit = true;
                    player.hitDirection = enemy.Direction;
                    timer2 = 2;
                    pcolor = Color.Red;
                    player.Health--;

                    
                    
                }
            }

            // Start of dirty solutions to handling death
            if (player.Health < 1)
            {
                KeyboardState keyboardState = Keyboard.GetState();
                player.isDead = true;
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                   itemsCollected = 0;
                   player.Health = 3;

                   player.isDead = false;
                }    
            }
            //end of dirty solutions, for more see draw

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
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer2 -= dt;
            if (timer2 < 1.8)
                pcolor = Color.White;
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
                        spriteToDraw = sword;
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
                else if (it.GetType() == typeof(Banana))
                    spriteToDraw = banana;
                else if (it.GetType() == typeof(Ham))
                    spriteToDraw = ham;
                else if (it.GetType() == typeof(Fork))
                    spriteToDraw = fork;
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
            if (player.Health > 0)
            {
                player.animation.Draw(_spriteBatch);
            }

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


            //NPC!
            if (isGameStarted)
            {
                if (npcSpeechTimer > 5.5)
                {
                    _spriteBatch.Draw(textBubble, new Vector2(this.camera.Position.X + 140, this.camera.Position.Y + 110), Color.White);
                    npcSpeechTimer -= dt;
                    if (npcSpeechTimer < 19.5 && npcSpeechTimer > 17)
                    {
                        _spriteBatch.DrawString(bubbleFont, "There's an annoying ", new Vector2(this.camera.Position.X + 170, this.camera.Position.Y + 128), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "angel to be killed", new Vector2(this.camera.Position.X + 170, this.camera.Position.Y + 140), Color.Black);
                    }
                    if (npcSpeechTimer < 16.5 && npcSpeechTimer > 15)
                    {
                        _spriteBatch.DrawString(bubbleFont, ".. one would need ", new Vector2(this.camera.Position.X + 170, this.camera.Position.Y + 128), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "a weapon tho.. ", new Vector2(this.camera.Position.X + 170, this.camera.Position.Y + 140), Color.Black);
                    }
                    if (npcSpeechTimer < 14.5 && npcSpeechTimer > 12)
                    {
                        _spriteBatch.DrawString(bubbleFont, "Maybe this trash ", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 128), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "laying around here", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 140), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "could be helpful? ", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 152), Color.Black);
                    }
                    if (npcSpeechTimer < 11.5 && npcSpeechTimer > 10)
                    {
                        _spriteBatch.DrawString(bubbleFont, "Go pick up stuff ", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 128), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "using  [E]", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 140), Color.Black);
                    }
                    if (npcSpeechTimer < 9.5 && npcSpeechTimer > 6.5)
                    {
                        _spriteBatch.DrawString(bubbleFont, "When you're done ", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 128), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "use [E] on the", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 140), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "Crafting Circle ", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 152), Color.Black);
                    }
                    if (npcSpeechTimer < 6.5 && npcSpeechTimer > 5.5)
                    {
                        _spriteBatch.DrawString(bubbleFont, "Shoot enemies with ", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 128), Color.Black);
                        _spriteBatch.DrawString(bubbleFont, "[space]", new Vector2(this.camera.Position.X + 165, this.camera.Position.Y + 140), Color.Black);
                    }
                }
            }


            if (!itemsCollectedDone)
                _spriteBatch.DrawString(gameFont, itemsCollected + " / 3", new Vector2(this.camera.Position.X -275, this.camera.Position.Y -137), color);
                _spriteBatch.Draw(itemPreview, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);
                _spriteBatch.Draw(swordGrayscale, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);

            }

            if (itemsCollectedDone)
            {
                _spriteBatch.Draw(itemPreview, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);
                _spriteBatch.Draw(sword, new Vector2(this.camera.Position.X - 315, this.camera.Position.Y - 135), Color.White);
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
                else if (splash.GetType() == typeof(DeadSplash))
                {
                    spriteToDraw = deadSplash;
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
