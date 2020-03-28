using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CasinoTycoon_v1._1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont spritefont1;
        public enum GameState { start, play, pause, end};
        GameState state;
        Texture2D intro;
        Rectangle introRect;
        Texture2D introTitle;
        Rectangle introTitleRect;
        int screenWidth;
        int screenHeight;

        public enum Direction { up, down, left, right};
        //Direction direction;

        Casino casino;

        Texture2D player;
        Rectangle playerRect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; graphics.PreferredBackBufferWidth = 700; graphics.PreferredBackBufferHeight = 600; graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = 700;
            screenHeight = 600;
            state = GameState.start;
            introRect = new Rectangle(0, 0, screenWidth, screenHeight);
            introTitleRect = new Rectangle(200, 165, 320, 130);
            casino = new Casino(Content.Load<Texture2D>("Casino/casinoFloor"), Content.Load<Texture2D>("Casino/slots"), Content.Load<Texture2D>("Casino/door"));
            playerRect = new Rectangle(screenWidth / 2, screenHeight / 2+10, 75, 75);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritefont1 = Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
            intro = Content.Load<Texture2D>("Intro/introScreen");
            introTitle = Content.Load<Texture2D>("Intro/introTitle");
            player = Content.Load<Texture2D>("3_24");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if(state == GameState.start)
            {
                //Allow for GamePad support
                if(kb.IsKeyDown(Keys.Enter))
                {
                    state = GameState.play;
                }
            }

            if(state == GameState.play)
            {
                
                if (kb.IsKeyDown(Keys.Right) && playerRect.X + playerRect.Width < screenWidth)
                {
                        playerRect.X += 5;
                }
                if (kb.IsKeyDown(Keys.Left) && playerRect.X > 0)
                {
                        playerRect.X -= 5;
                }
                if (kb.IsKeyDown(Keys.Up) && playerRect.Y > 0)
                {
                        playerRect.Y -= 5;
                }
                if (kb.IsKeyDown(Keys.Down) && playerRect.Y + playerRect.Height < screenHeight)
                {
                        playerRect.Y += 5;
                }
                //Console.WriteLine("Player: " + playerRect.X + playerRect.Y);
                if(playerRect.X + 100 >= screenWidth)
                {
                    casino.move(Casino.Direction.right);
                }
                if(playerRect.X <= 0)
                {
                    casino.move(Casino.Direction.left);
                }
                if(playerRect.Y <= 0)
                {
                    casino.move(Casino.Direction.up);
                }
                if(playerRect.Y + 100 >= screenHeight)
                {
                    casino.move(Casino.Direction.down);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch(state)
            {
                case GameState.start:
                    drawStart();
                    break;
                case GameState.play:
                    drawPlay();
                    break;
                    //case GameState.pause:
                    //    drawPause();
                    //    break;
                    //case GameState.end:
                    //    drawEnd();
                    //    break;
            }

            base.Draw(gameTime);
        }

        public void drawStart()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(intro, introRect, Color.White);
            spriteBatch.Draw(introTitle, introTitleRect, Color.White);
            spriteBatch.DrawString(spritefont1, "Press Enter to Start", new Vector2(450, 525), Color.White);
            spriteBatch.End();
        }
        public void drawPlay()
        {
            casino.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(player, playerRect, Color.White);
            spriteBatch.End();
        }
    }
}
