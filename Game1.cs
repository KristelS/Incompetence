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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D tutorialBackgroundImg;
        Texture2D bombb;
        Texture2D bombflash;

        Texture2D playerSprite;
        Texture2D playerDown;
        Texture2D playerUp;
        Texture2D playerLeft;
        Texture2D playerRight;
        Texture2D heart;

        Texture2D potionRed;
        Texture2D potionBlue;
        Texture2D potionGreen;

        Texture2D tutorialMonster;

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

            tutorialBackgroundImg = Content.Load<Texture2D>("FirstBaseMap");
            bombb = Content.Load<Texture2D>("bomb");
            bombflash = Content.Load<Texture2D>("bombflash");

            potionRed = Content.Load<Texture2D>("potionRed");
            potionGreen = Content.Load<Texture2D>("potionGreen");
            potionBlue = Content.Load<Texture2D>("potionBlue");

            tutorialMonster = Content.Load<Texture2D>("tutmonster");

            playerDown = Content.Load<Texture2D>("playerDown");
            playerUp = Content.Load<Texture2D>("playerUp");
            playerLeft = Content.Load<Texture2D>("playerLeft");
            playerRight = Content.Load<Texture2D>("maincharrunright");
            playerSprite = Content.Load<Texture2D>("playerRight");
            player.animations[0] = new SpriteAnimation(playerDown, 4, 8);
            player.animations[1] = new SpriteAnimation(playerUp, 4, 8);
            player.animations[2] = new SpriteAnimation(playerLeft, 4, 8);
            player.animations[3] = new SpriteAnimation(playerRight, 4, 10);

            heart = Content.Load<Texture2D>("heart");

            Item.items.Add(new PotionRed(new Vector2(138, 68)));
            Item.items.Add(new PotionBlue(new Vector2(170, 100)));
            Item.items.Add(new PotionGreen(new Vector2(202, 200)));

            EnemyMonster.enemies.Add(new TutorialMonster(new Vector2(590, 590)));
            player.animation = player.animations[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            this.camera.Position = player.Position;
            this.camera.Zoom = player.cameraZoom;
            this.camera.Update(gameTime);

            foreach (Bomb bomb in Bomb.bombs)
            {
                bomb.Update(gameTime);
            }

            foreach (EnemyMonster enemy in EnemyMonster.enemies)
            {
                enemy.Update(gameTime, player.Position);
            }

            Bomb.bombs.RemoveAll(b => b.state == 2);
            Item.items.RemoveAll(i => i.IsPickedUp == true);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(this.camera);

            _spriteBatch.Draw(tutorialBackgroundImg, new Vector2(0, 0), Color.White);
            TimeSpan intervalBetweenPressed1 = TimeSpan.FromMilliseconds(30);
            TimeSpan lastTimePressed;

            lastTimePressed = gameTime.TotalGameTime;
            float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;



            foreach (Bomb bomb in Bomb.bombs)
            {
                _spriteBatch.Draw(bombb, new Vector2(bomb.Position.X + 16 - bomb.Radius, bomb.Position.Y + 16 - bomb.Radius), Color.White);
                if (bomb.state == 1)
                {
                    _spriteBatch.Draw(bombflash, new Vector2(bomb.Position.X + 16 - bomb.Radius, bomb.Position.Y + 16 - bomb.Radius), Color.White);


                }
            }

            foreach (EnemyMonster enemy in EnemyMonster.enemies)
            {
                Texture2D spriteToDraw;
                if (enemy.GetType() == typeof(TutorialMonster))
                {
                    spriteToDraw = tutorialMonster;
                }
                else
                    spriteToDraw = potionRed;


                _spriteBatch.Draw(spriteToDraw, enemy.Position, Color.White);
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
                else
                    spriteToDraw = bombb;
                    
                
                _spriteBatch.Draw(spriteToDraw, it.Position, Color.White);
            }

            for (int i = 0; i < player.Health; i++)
            {
                _spriteBatch.Draw(heart, new Vector2(this.camera.Position.X - _graphics.PreferredBackBufferWidth / 2 + i * 45, this.camera.Position.Y - _graphics.PreferredBackBufferHeight / 2), Color.White);
            }

            //to see the borders

            //foreach (Obstacle o in Obstacle.obstacles)
            //{
            //    Texture2D spriteToDraw;
            //    if (o.GetType() == typeof(Border))
            //        spriteToDraw = border;
            //    else
            //        spriteToDraw = bush;
            //    _spriteBatch.Draw(spriteToDraw, o.Position, Color.White);
            //}


            //_spriteBatch.Draw(player_Sprite, player.Position, Color.White);
            player.animation.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
