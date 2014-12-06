/* CollisionManager.cs
 * Kim Thanh Thai, 
 * Paul Sobering 
 * Created Dec 6 2014 
 */
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
        Player p1;
        Player p2;
        Wall topWall;
        Wall botWall;
        Wall leftWall;
        Wall rightWall;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
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
            ContentManager.LoadAll(this);
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

            Vector2 pSpeed = new Vector2(2, 2); // base speed is the same for both players;

            KeyBindings p1Bindings = new KeyBindings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space);
            Vector2 p1Pos = new Vector2(0, 0);
            
            p1 = new Player(this, spriteBatch, ContentManager.Player1Tex, p1Pos, pSpeed, stage, p1Bindings);
            this.Components.Add(p1);

            KeyBindings p2Bindings = new KeyBindings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.NumPad0);
            Vector2 p2Pos = new Vector2(stage.X - p1.Width, stage.Y - p1.Height); // dimensions of both players are constant, using p1's values because it's already instantiated.
            p2 = new Player(this, spriteBatch, ContentManager.Player2Tex, p2Pos, pSpeed, stage, p2Bindings);
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
            base.Draw(gameTime);
        }
    }
}
