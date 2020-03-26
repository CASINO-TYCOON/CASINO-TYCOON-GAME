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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; graphics.PreferredBackBufferWidth = 700; graphics.PreferredBackBufferHeight = 500; graphics.ApplyChanges();
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
            screenHeight = 500;
            state = GameState.start;
            introRect = new Rectangle(0, 0, screenWidth, screenHeight);
            introTitleRect = new Rectangle(210, 135, 300, 110);
            casino = new Casino(Content.Load<Texture2D>("Casino/casinoFloor"), Content.Load<Texture2D>("Casino/slots"));
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
                if(kb.IsKeyDown(Keys.Right))
                {
                    casino.move(Casino.Direction.right);
                }
                if (kb.IsKeyDown(Keys.Left))
                {
                    casino.move(Casino.Direction.left);
                }
                if (kb.IsKeyDown(Keys.Up))
                {
                    casino.move(Casino.Direction.up);
                }
                if (kb.IsKeyDown(Keys.Down))
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
            spriteBatch.DrawString(spritefont1, "Press Enter to Start", new Vector2(450, 450), Color.White);
            spriteBatch.End();
        }
        public void drawPlay()
        {
            casino.Draw(spriteBatch);
        }
    }
}
