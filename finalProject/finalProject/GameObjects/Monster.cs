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
    public class Monster : Microsoft.Xna.Framework.DrawableGameComponent
    {
        const int DELAY = 10;
        const int SPRITE_WIDTH = 25;
        public static int Width
        {
            get { return SPRITE_WIDTH; }
        }

        const int SPRITE_HEIGHT = 25;
        public static int Height
        {
            get { return SPRITE_HEIGHT; }
        }

        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private List<Rectangle> frames;
        private int frameIndex = 0;
        
        private GridCell[,] grid;

        private int delayCounter;
        private const int NUMBER_OF_FRAMES = 15;
        public enum State { Up, Down, Left, Right, Dead };
        private State monsterState = State.Down;
        public State MonsterState
        {
            get { return monsterState; }
            set { monsterState = value; }
        }

        public Monster(Game1 game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed, GridCell[,] grid)
            : base(game)
        {
            // TODO: Construct any child components here
            this.grid = grid;
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            AnimationFrames();
            
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

        private void AnimationFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int x = j * SPRITE_WIDTH;
                    int y = i * SPRITE_HEIGHT;
                    Rectangle r = new Rectangle(x, y, SPRITE_WIDTH, SPRITE_HEIGHT);
                    frames.Add(r);
                }
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            if (monsterState != State.Dead)
            {
                delayCounter++;
                if (delayCounter > DELAY)
                {
                    frameIndex++;
                    if (frameIndex == NUMBER_OF_FRAMES)
                    {
                        frameIndex = 0;
                    }
                    delayCounter = 0;
                }
            }
            else
            {
                // Death animation
                this.Enabled = false;
                this.Visible = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(frameIndex), Color.White);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, SPRITE_WIDTH, SPRITE_HEIGHT);
        }
    }
}
