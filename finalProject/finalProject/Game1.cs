/* Game1.cs
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

        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }
        SpriteBatch spriteBatch;

        private StartScene startScene;
        private HelpScene helpScene;
        public ActionScene actionScene;
        private HowToPlayScene howtoPlay;

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
            ContentManager.LoadAll(this);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);

            SpriteFont font = Content.Load<SpriteFont>("fonts/regularFont");
            Vector2 pos = new Vector2(100, 100);
            string message = "The general goal throughout the series is to complete the levels by strategically placing bombs" +
                             "\nin order to kill enemies and destroy obstacles. Exploding bombs can set off other bombs, " +
                             "\nkill or injure enemies, and destroy obstacles. However, they can also kill or injure the player character," +
                             "\ndestroy powerups, and sometimes 'anger' the exit, causing it to generate more enemies. " +
                             "\nMost Bomberman games also feature a multiplayer mode, where other Bombermen act as opponents, and the last " +
                             "\none standing is the winner.";
            helpScene = new HelpScene(this, spriteBatch, font, pos, message, Color.Gold);
            Components.Add(helpScene);

            SpriteFont font1 = Content.Load<SpriteFont>("fonts/regularFont");
            Vector2 pos1 = new Vector2(100, 100);
            string message1 = "Move Up: W      Key Up\n" + "\nMove Down: S      Key Down\n" + "\nMove Left: A      Key Left\n" +
                              "\nMove Right: D      Key Right\n" + "\nBomb: Space key      Pad Number 0";
            howtoPlay = new HowToPlayScene(this, spriteBatch, font, pos1, message1, Color.Gold);
            Components.Add(howtoPlay);

            actionScene = new ActionScene(this, spriteBatch);
            actionScene.Enabled = false;
            Components.Add(actionScene);

            startScene.show();
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
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    howtoPlay.show();
                }
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }

            }
            if (helpScene.Enabled || actionScene.Enabled || howtoPlay.Enabled)
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
            GraphicsDevice.Clear(Color.BurlyWood);
            base.Draw(gameTime);
        }
    }
}
