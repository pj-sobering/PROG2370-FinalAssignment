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
    public class Wall : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Rectangle destination;
        private bool destructible;
        private bool edge;

        public bool Destructible
        {
            get { return destructible; }
            set { destructible = value; }
        }

        public Wall(Game game, SpriteBatch spriteBatch, Rectangle destination, bool destructible, bool edge = false)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.destination = destination;
            this.destructible = destructible;
            this.edge = edge;
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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (destructible == false)
            {
                if (edge == false)
                {
                    spriteBatch.Draw(ContentManager.WallTex, destination, Color.White);
                }
                else
                {
                    spriteBatch.Draw(ContentManager.WallTex, destination, Color.SlateGray);
                }
            }
            else 
            {
                spriteBatch.Draw(ContentManager.WallTex, destination, Color.Brown);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBound()
        {
            return destination;
        }
    }
}
