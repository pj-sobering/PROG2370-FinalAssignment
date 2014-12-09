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
        const int GRID_HEIGHT = 13;
        const int GRID_WIDTH = 17;
        float width, height;
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
                pRec.Width -= SPRITE_X_OFFSET + 5;
                pRec.Y += SPRITE_Y_OFFSET;
                pRec.Height -= SPRITE_Y_OFFSET;
                bool colliding = false;
                foreach (GridCell gc in grid)
                {
                    if (gc != null)
                    {
                        if (gc.Wall != null)
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
                    switch (p.PlayerState)
                    {
                        case Player.State.Up:
                            p.Position = new Vector2(p.Position.X, p.Position.Y + p.Speed.Y);
                            break;
                        case Player.State.Down:
                            p.Position = new Vector2(p.Position.X, p.Position.Y - p.Speed.Y);
                            break;
                        case Player.State.Left:
                            p.Position = new Vector2(p.Position.X + p.Speed.X, p.Position.Y);
                            break;
                        case Player.State.Right:
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
        public static GridCell EmptySpace(Rectangle playerSpace, GridCell[,] grid)
        {
            foreach (GridCell gc in grid)
            {
                if (gc.Type == GridCell.CellType.Empty && gc.Destination.Intersects(playerSpace))
                {
                    return gc;
                }
            }
            return new GridCell(playerSpace, GridCell.CellType.Empty, new Vector2(1,1)) ; 
        }

        public static Vector2 GridCoords(Rectangle space)
        {
            int x = (int)Math.Floor(space.X / (ContentManager.Stage.X / GRID_WIDTH));
            int y = (int)Math.Floor(space.Y / (ContentManager.Stage.Y / GRID_HEIGHT));
            return new Vector2(x, y);
        }

        //public static void ExplosionCollision(Explosion[] explosions)
        //{
        //    foreach (Player p in players)
        //    {
        //        foreach (Explosion e in explosions)
        //        {
        //            if (p.getBounds().Intersects(e.Destination))
        //            {
        //                p.PlayerState = Player.State.Dead; // :(
        //            }
        //        }
        //    }
        //}
    }
}
