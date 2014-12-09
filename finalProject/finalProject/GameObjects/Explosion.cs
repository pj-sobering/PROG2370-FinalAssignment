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
    public class Explosion : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Rectangle destination;

        public Rectangle Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        const int SPRITE_WIDTH = 16;
        const int SPRITE_HEIGHT = 16;
        const int SPRITE_FRAMES = 5;
        const int DELAY = 5;
        private List<Rectangle> framesMiddle, framesVertical, framesRight, framesLeft, 
            framesRightCap, framesLeftCap, framesTopCap, framesBottomCap;
        enum Sprite_Y_Index  { Middle = 0, Vertical = 1, Right = 2, Left = 3, LeftCap = 4, BottomCap = 5, RightCap = 6, TopCap = 7 }
        public enum Direction { Left, Right, Up, Down, Center, upCap, downCap, rightCap, leftCap}
        Direction direction;
        int frameIndex = 0;
        int timer = 0;


        public Explosion(Game1 game, SpriteBatch spriteBatch, Rectangle destination, Direction direction)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.direction = direction;
            this.destination = destination;
            CreateFrames();
        }

        private void CreateFrames()
        {
            framesMiddle = new List<Rectangle>();
            framesVertical = new List<Rectangle>();
            framesRight = new List<Rectangle>();
            framesLeft = new List<Rectangle>();
            framesRightCap = new List<Rectangle>();
            framesLeftCap = new List<Rectangle>();
            framesTopCap = new List<Rectangle>();
            framesBottomCap = new List<Rectangle>();

            //Middle
            for (int i = 0; i < SPRITE_FRAMES; i++)
            {
                Rectangle r = new Rectangle(i * SPRITE_WIDTH, (int)Sprite_Y_Index.Middle * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesMiddle.Add(r);
            }
            //Vertical
            for (int i = 0; i < SPRITE_FRAMES; i++)
            {
                Rectangle r = new Rectangle(i * SPRITE_WIDTH, (int)Sprite_Y_Index.Vertical * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesVertical.Add(r);
            }
            //Right
            for (int i = 0; i < SPRITE_FRAMES; i++)
            {
                Rectangle r = new Rectangle(i * SPRITE_WIDTH, (int)Sprite_Y_Index.Right * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesRight.Add(r);
            }
            //Left
            for (int i = SPRITE_FRAMES; i > 0; i--)
            {
                Rectangle r = new Rectangle((i -1) * SPRITE_WIDTH, (int)Sprite_Y_Index.Left * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesLeft.Add(r);
            }
            //LeftCap
            for (int i = SPRITE_FRAMES; i > 0; i--)
            {
                Rectangle r = new Rectangle((i -1) * SPRITE_WIDTH, (int)Sprite_Y_Index.LeftCap * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesLeftCap.Add(r);
            }
            //Bottom Cap
            for (int i = 0; i < SPRITE_FRAMES; i++)
            {
                Rectangle r = new Rectangle(i * SPRITE_WIDTH, (int)Sprite_Y_Index.BottomCap * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesBottomCap.Add(r);
            }
            //Right Cap
            for (int i = 0; i < SPRITE_FRAMES; i++)
            {
                Rectangle r = new Rectangle(i * SPRITE_WIDTH, (int)Sprite_Y_Index.RightCap * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesRightCap.Add(r);
            }
            //Top Cap
            for (int i = 0; i < SPRITE_FRAMES; i++)
            {
                Rectangle r = new Rectangle(i * SPRITE_WIDTH, (int)Sprite_Y_Index.TopCap * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesTopCap.Add(r);
            }
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
            if (frameIndex == SPRITE_FRAMES)
            {
                this.Visible = false;
                this.Enabled = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            switch (direction)
            {
                case Direction.Center:
                    spriteBatch.Begin();
                    spriteBatch.Draw(ContentManager.ExplosionTex, destination, framesMiddle.ElementAt(frameIndex), Color.White);
                    spriteBatch.End();
                    break;
                case Direction.Down:
                    spriteBatch.Begin();
                    spriteBatch.Draw(ContentManager.ExplosionTex, destination, framesBottomCap.ElementAt(frameIndex), Color.White);
                    spriteBatch.End();
                    break;
                case Direction.Up:
                    spriteBatch.Begin();
                    spriteBatch.Draw(ContentManager.ExplosionTex, destination, framesTopCap.ElementAt(frameIndex), Color.White);
                    spriteBatch.End();
                    break;
                case Direction.Left:
                    spriteBatch.Begin();
                    spriteBatch.Draw(ContentManager.ExplosionTex, destination, framesLeftCap.ElementAt(frameIndex), Color.White);
                    spriteBatch.End();
                    break;
                case Direction.Right:
                    spriteBatch.Begin();
                    spriteBatch.Draw(ContentManager.ExplosionTex, destination, framesRightCap.ElementAt(frameIndex), Color.White);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
