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
        const int SPRITE_WIDTH = 42;
        public static int Width
        {
            get { return SPRITE_WIDTH; }
        }

        const int SPRITE_HEIGHT = 30;
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
        private Game1 game;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private const int NUMBER_OF_FRAMES = 15;
        public enum State { Up, Down, Left, Right, Dead };
        private State monsterState = State.Down;
        public State MonsterState
        {
            get { return monsterState; }
            set { monsterState = value; }
        }

        public Monster(Game1 game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed,
                       int delay, State direction)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.delay = delay;
            dimension = new Vector2(41, 40);
            this.monsterState = direction;
            this.game = game;
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
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
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
            Random rand = new Random();
            int randomDirection = rand.Next(4);
            position += speed;

            if (position.X > ContentManager.Stage.X - SPRITE_WIDTH || position.Y > ContentManager.Stage.Y - SPRITE_HEIGHT)
            {
                if (randomDirection == 1)
                {
                    position.Y -= speed.Y;
                    monsterState = State.Up;
                }
                if (randomDirection == 2)
                {
                    position.X += speed.Y;
                    monsterState = State.Right;
                }
                if (randomDirection == 3)
                {
                    position.Y += speed.Y;
                }
                if (randomDirection == 4)
                {
                    position.X -= speed.X;
                }
            }

            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > NUMBER_OF_FRAMES)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(tex, position, frames.ElementAt<Rectangle>(frameIndex), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, SPRITE_WIDTH, SPRITE_HEIGHT);
        }
    }
}
