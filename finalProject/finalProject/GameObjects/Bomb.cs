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
        private List<Rectangle> frames = new List<Rectangle>();
        const int SPRITE_WIDTH = 40;
        const int SPRITE_HEIGHT = 40; // 
        const int SPRITE_FRAMES = 4; // Number of frames in the animation
        const int DELAY = 40;
        int timer = 0;
        int frameIndex = 0;

        public Bomb(Game game, SpriteBatch spriteBatch, Vector2 position)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.position = position;
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
            if (frameIndex == SPRITE_FRAMES)
            {
                this.Enabled = false;
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
