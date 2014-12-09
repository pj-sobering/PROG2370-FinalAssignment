/* Bomb.cs
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
    public class Bomb : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Rectangle destination;
        private List<Rectangle> frames = new List<Rectangle>();
        private Texture2D tex = ContentManager.BombTex;
        const int SPRITE_WIDTH = 30;
        const int SPRITE_HEIGHT = 30; 
        const int SPRITE_FRAMES = 4; // Number of frames in the animation
        const int DELAY = 40;
        GridCell[,] grid;
        Vector2 coords;
        int timer = 0;
        int frameIndex = 0;
        Game1 game;
        Player player;
        private SoundEffect hit;
        private bool multiSound = true;
        public bool MultiSound
        {
            get { return multiSound; }
            set { multiSound = value; }
        }
        
        public Bomb(Game1 game, SpriteBatch spriteBatch, Rectangle destination, GridCell[,] grid, Vector2 coords, Player player, 
                    SoundEffect hit)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            float x = destination.X + (destination.Width /2) - (SPRITE_WIDTH /2);
            float y = destination.Y;
            this.destination = destination;
            position = new Vector2(x, y);
            this.coords = coords;
            this.game = game;
            this.grid = grid;
            this.player = player;
            this.hit = game.Content.Load<SoundEffect>("sounds/explode");
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            AnimationFrames();
            base.Initialize();
        }

        /// <summary>
        /// Creates lists of Rectangles to use as viewports of a texture.
        /// </summary>
        private void AnimationFrames()
        {
            for (int i = 0; i < 5; i++)
            {
                Rectangle r = new Rectangle(SPRITE_WIDTH * i, 0, SPRITE_WIDTH, SPRITE_HEIGHT);
                frames.Add(r);
            }
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (timer < DELAY)
            {
                timer++;
            }
            else
            {
                if (frameIndex < SPRITE_FRAMES)
                {
                    frameIndex++;
                }
                timer = 0;
            }
            if (frameIndex == SPRITE_FRAMES && multiSound == true)
            {
                Explosion center = new Explosion(game, spriteBatch, destination, Explosion.Direction.Center);
                game.Components.Add(center);
                GridCell up = grid[(int)coords.X - 1, (int)coords.Y];
                GridCell down = grid[(int)coords.X +1, (int)coords.Y];
                GridCell left = grid[(int)coords.X, (int)coords.Y -1];
                GridCell right = grid[(int)coords.X, (int)coords.Y +1];
                if (up.Type != GridCell.CellType.Wall)
                {
                    Explosion exp = new Explosion(game,spriteBatch, up.Destination, Explosion.Direction.Up);
                    if (up.Wall != null && up.Wall.Destructible == true)
                    {
                        up.Wall.Visible = false;
                        up.Wall.Enabled = false;
                        up.Wall = null;
                        up.Type = GridCell.CellType.Empty;
                        player.Score++;
                    }
                    game.Components.Add(exp);
                }
                if (down.Type != GridCell.CellType.Wall)
                {
                    Explosion exp = new Explosion(game,spriteBatch, down.Destination, Explosion.Direction.Down);
                    if (down.Wall != null && down.Wall.Destructible == true)
                    {
                        down.Wall.Visible = false;
                        down.Wall.Visible = false;
                        down.Wall = null;
                        down.Type = GridCell.CellType.Empty;
                        player.Score++;
                    }
                    game.Components.Add(exp);
                }
                if (left.Type != GridCell.CellType.Wall)
                {

                    Explosion exp = new Explosion(game, spriteBatch, left.Destination, Explosion.Direction.Left);
                    if (left.Wall != null && left.Wall.Destructible == true)
                    {
                        left.Wall.Visible = false;
                        left.Wall.Visible = false;
                        left.Wall = null;
                        left.Type = GridCell.CellType.Empty;
                        player.Score++;
                    }
                    game.Components.Add(exp);
                }
                if (right.Type != GridCell.CellType.Wall)
                {
                    Explosion exp = new Explosion(game, spriteBatch, right.Destination, Explosion.Direction.Right);
                    if (right.Wall != null && right.Wall.Destructible == true)
                    {
                        right.Wall.Visible = false;
                        right.Wall.Visible = false;
                        right.Wall = null;
                        right.Type = GridCell.CellType.Empty;
                        player.Score++;
                    }
                    game.Components.Add(exp);
                }
                this.Enabled = false;
                multiSound = false;
                hit.Play();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ContentManager.BombTex, position, frames.ElementAt(frameIndex), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
