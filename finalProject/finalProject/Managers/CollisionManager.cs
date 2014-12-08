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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        const int SPRITE_X_OFFSET = 10;
        const int SPRITE_Y_OFFSET = 5;
        private Player[] players;
        public GridCell[,] grid;
        private Game1 game;

        public CollisionManager(Game1 game, Player[] players, GridCell[,] grid)
            : base(game)
        {
            // TODO: Construct any child components here
            this.players = players;
            this.grid = grid;
            this.game = game;
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
            foreach (Player p in players)
            {
                Rectangle pRec = p.getBounds();
                pRec.X += SPRITE_X_OFFSET;
                pRec.Width -= SPRITE_X_OFFSET;
                pRec.Y += SPRITE_Y_OFFSET;
                pRec.Height -= SPRITE_Y_OFFSET;
                bool colliding = false;
                foreach (GridCell gc in grid)
                {
                    if (gc != null)
                    {
                        if (gc.Type == GridCell.CellType.Wall || gc.Type == GridCell.CellType.Destructable)
                        {
                            if (pRec.Intersects(gc.Destination))
                            {
                                colliding = true;
                            }
                        }
                    }
                }
                if (colliding == true)
                {
                    switch (p.direction)
                    {
                        case Player.Direction.Up:
                            p.Position = new Vector2(p.Position.X, p.Position.Y + p.Speed.Y);
                            break;
                        case Player.Direction.Down:
                            p.Position = new Vector2(p.Position.X, p.Position.Y - p.Speed.Y);
                            break;
                        case Player.Direction.Left:
                            p.Position = new Vector2(p.Position.X + p.Speed.X, p.Position.Y);
                            break;
                        case Player.Direction.Right:
                            p.Position = new Vector2(p.Position.X - p.Speed.X, p.Position.Y);
                            break;
                    }
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Returns a rectangle that represents a grid space, used to snap objects to grid.
        /// </summary>
        /// <param name="playerSpace"></param>
        /// <returns></returns>
        public static Rectangle EmptySpace(Rectangle playerSpace, GridCell[,] grid)
        {
            foreach (GridCell gc in grid)
            {
                if (gc.Type == GridCell.CellType.Empty && gc.Destination.Intersects(playerSpace))
                {
                    return gc.Destination;
                }
            }
            return playerSpace; 
        }
    }
}
