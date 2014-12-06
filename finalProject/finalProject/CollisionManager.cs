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
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private Player p1;
        private Player p2;
        private Wall wall;

        public CollisionManager(Game game, Player p1, Player p2, Wall wall)
            : base(game)
        {
            // TODO: Construct any child components here
            this.p1 = p1;
            this.p2 = p2;
            this.wall = wall;
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
            Rectangle p1Rect = p1.getBound();
            Rectangle p2Rect = p2.getBound();
            Rectangle wallRect = wall.getBound();
            
            base.Update(gameTime);
        }
    }
}
