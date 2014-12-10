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
        private Scene helpScene;
        private Scene howtoPlay;
        private Scene about;
        private Scene gameOver;
        private ActionScene actionScene;

        public ActionScene ActionScene
        {
          get { return actionScene; }
          set { actionScene = value; }
        }

        /// <summary>
        ///     Constructor for Game 1
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
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

        /// <summary>
        ///     Hides all components of type GameScene.
        /// </summary>
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
            startScene = new StartScene(this, spriteBatch);
            Components.Add(startScene);

            SpriteFont font = Content.Load<SpriteFont>("fonts/regularFont");
            string helpText = "The general goal throughout the series is to complete the levels by" +
                             "\nstrategically placing bombs in order to kill enemies and destroy obstacles." +
                             "\nExploding bombs can set off other bombs, kill or injure enemies," +
                             "\nand destroy obstacles. However, they can also kill or injure the player " +
                             "\ncharacter, destroy powerups, and sometimes 'anger' the exit, causing it" +
                             "\nto generate more enemies. Most Bomberman games also feature a multiplayer" +
                             "\nmode where other Bombermen act as opponents, and the last none standing" +
                             "\nis the winner.";
            Vector2 pos = new Vector2(ContentManager.Stage.X / 2 - (font.MeasureString(helpText).X / 2), ContentManager.Stage.Y / 2 - (font.MeasureString(helpText).Y / 2));
            helpScene = new Scene(this, spriteBatch, font, pos, helpText, Color.Indigo);
            Components.Add(helpScene);

            string controls = "Move Up: W   /   Key Up\n" + "\nMove Down: S   /   Key Down\n" + "\nMove Left: A   /   Key Left\n" +
                              "\nMove Right: D   /   Key Right\n" + "\nBomb: Space key   /   Pad Number 0 (Turn NumLock OFF!)" ;
            pos = new Vector2(ContentManager.Stage.X / 2 - (font.MeasureString(controls).X / 2), ContentManager.Stage.Y / 2 - (font.MeasureString(controls).Y / 2));
            howtoPlay = new Scene(this, spriteBatch, font, pos, controls, Color.Indigo);
            Components.Add(howtoPlay);

            string message2 = "Bomberman\n\nPaul Sobering\nKim Thai";
            about = new Scene(this, spriteBatch, font, pos, message2, Color.Indigo);
            Components.Add(about);


            pos = new Vector2(ContentManager.Stage.X / 2 - font.MeasureString("Game Over").X / 2, ContentManager.Stage.Y / 2 - font.MeasureString("Game Over").Y / 2);
            gameOver = new Scene(this, spriteBatch, font, pos, "Game Over", Color.Red);
            Components.Add(gameOver);

            actionScene = new ActionScene(this, spriteBatch);
            actionScene.Enabled = false;
            Components.Add(actionScene);
            startScene.show();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(ContentManager.Song);
            
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
                    actionScene.DrawOrder = 3;
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
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    about.show();
                }
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }
            }
            if (helpScene.Enabled || actionScene.Enabled || howtoPlay.Enabled || about.Enabled || gameOver.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (Player.Lives < 0)
            {
                hideAllScenes();
                gameOver.show();
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (actionScene.Level)
            {
                case 1: GraphicsDevice.Clear(Color.BurlyWood);
                    break;
                case 2: GraphicsDevice.Clear(Color.Green);
                    break;
            }
            if (actionScene.Enabled == false && helpScene.Enabled == false
                && howtoPlay.Enabled == false && about.Enabled == false && Player.Lives > 0)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(ContentManager.Splash, new Rectangle(0, 0, ContentManager.Splash.Width /2, ContentManager.Splash.Height /2), Color.White);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
