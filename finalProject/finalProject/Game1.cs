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

namespace finalProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;

        Texture2D backgroundTex;
        Player p1;
        Player2 p2;
        Wall topWall;
        Wall botWall;
        Wall leftWall;
        Wall rightWall;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            Console.WriteLine(graphics.PreferredBackBufferWidth + " " + graphics.PreferredBackBufferHeight);
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);

            helpScene = new HelpScene(this, spriteBatch);
            Components.Add(helpScene);
            actionScene = new ActionScene(this, spriteBatch);
            Components.Add(actionScene);

            startScene.show();

            // TODO: use this.Content to load your game content here
            backgroundTex = Content.Load<Texture2D>("images/background");
            Vector2 stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);



            Texture2D topWallTex = Content.Load<Texture2D>("images/wallTopBot");
            Vector2 topWallPos = new Vector2(0, 0);
            topWall = new Wall(this, spriteBatch, topWallTex, topWallPos);
            this.Components.Add(topWall);

            Texture2D botWallTex = Content.Load<Texture2D>("images/wallTopBot");
            Vector2 botWallPos = new Vector2(0, stage.Y - botWallTex.Height);
            botWall = new Wall(this, spriteBatch, botWallTex, botWallPos);
            this.Components.Add(botWall);

            Texture2D leftWallTex = Content.Load<Texture2D>("images/wallLeftRight");
            Vector2 leftWallPos = new Vector2(0, stage.Y - (topWallTex.Height + leftWallTex.Height));
            leftWall = new Wall(this, spriteBatch, leftWallTex, leftWallPos);
            this.Components.Add(leftWall);

            Texture2D rightWallTex = Content.Load<Texture2D>("images/wallLeftRight");
            Vector2 rightWallPos = new Vector2(stage.X - rightWallTex.Width, stage.Y - (topWallTex.Height + leftWallTex.Height));
            rightWall = new Wall(this, spriteBatch, rightWallTex, rightWallPos);
            this.Components.Add(rightWall);



            Texture2D p1Front = Content.Load<Texture2D>("images/player1");
            Vector2 p1FrontPos = new Vector2(0, 0);
            Vector2 p1FrontSpeed = new Vector2(2, 2);
            p1 = new Player(this, spriteBatch, p1Front, p1FrontPos, p1FrontSpeed, stage);
            this.Components.Add(p1);

            Texture2D p2Front = Content.Load<Texture2D>("images/player2");
            Vector2 p2FrontPos = new Vector2(stage.X - p2Front.Width, stage.Y - p2Front.Height);
            Vector2 p2FrontSpeed = new Vector2(2, 2);
            p2 = new Player2(this, spriteBatch, p2Front, p2FrontPos, p2FrontSpeed, stage);
            this.Components.Add(p2);

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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    actionScene.show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }

                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }

            }
            if (helpScene.Enabled || actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
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
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            //spriteBatch.Draw(backgroundTex, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
            //                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
