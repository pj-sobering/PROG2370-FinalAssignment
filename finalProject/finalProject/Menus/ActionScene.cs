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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Vector2 stage;
        Wall topWall;
        Wall botWall;
        Wall leftWall;
        Wall rightWall;
        Player p1;
        Player p2;

        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.stage = ContentManager.Stage;


            Texture2D topWallTex = game.Content.Load<Texture2D>("images/wallTopBot");
            Vector2 topWallPos = new Vector2(0, 0);
            topWall = new Wall(game, spriteBatch, topWallTex, topWallPos);
            this.Components.Add(topWall);

            Texture2D botWallTex = game.Content.Load<Texture2D>("images/wallTopBot");
            Vector2 botWallPos = new Vector2(0, stage.Y - botWallTex.Height);
            botWall = new Wall(game, spriteBatch, botWallTex, botWallPos);
            this.Components.Add(botWall);

            Texture2D leftWallTex = game.Content.Load<Texture2D>("images/wallLeftRight");
            Vector2 leftWallPos = new Vector2(0, stage.Y - (topWallTex.Height + leftWallTex.Height));
            leftWall = new Wall(game, spriteBatch, leftWallTex, leftWallPos);
            this.Components.Add(leftWall);

            Texture2D rightWallTex = game.Content.Load<Texture2D>("images/wallLeftRight");
            Vector2 rightWallPos = new Vector2(stage.X - rightWallTex.Width, stage.Y - (topWallTex.Height + leftWallTex.Height));
            rightWall = new Wall(game, spriteBatch, rightWallTex, rightWallPos);
            this.Components.Add(rightWall);

            //Vector2 pSpeed = new Vector2(2, 2); // base speed is the same for both players;

            //KeyBindings p1Bindings = new KeyBindings(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space);
            //Vector2 p1Pos = new Vector2(0, 0);

            //p1 = new Player(game, spriteBatch, ContentManager.Player1Tex, p1Pos, pSpeed, stage, p1Bindings);
            //this.Components.Add(p1);

            //KeyBindings p2Bindings = new KeyBindings(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.NumPad0);
            //Vector2 p2Pos = new Vector2(stage.X - p1.Width, stage.Y - p1.Height); // dimensions of both players are constant, using p1's values because it's already instantiated.
            //p2 = new Player(game, spriteBatch, ContentManager.Player2Tex, p2Pos, pSpeed, stage, p2Bindings);
            //this.Components.Add(p2);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
