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
    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Texture2D tex;

        private Vector2 speed;
        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private Vector2 stage;
        private string direction;
        private List<Rectangle> framesUp = new List<Rectangle>();
        private List<Rectangle> framesDown = new List<Rectangle>();
        private List<Rectangle> framesLeft = new List<Rectangle>();
        private List<Rectangle> framesRight = new List<Rectangle>();
        int delayCounter = 0;
        int frameIndex = 0; 
        const string UP = "up";
        const string DOWN = "down";
        const string LEFT = "left";
        const string RIGHT = "right";
        const int DELAY = 10;
        const int SPRITE_WIDTH = 42;
        const int SPRITE_HEIGHT = 58;
        const int SPRITE_PADDING = 20;
        const int SPRITE_FRAME_LIMIT = 4;
        Bomb bomb;
        Texture2D bombTex;

        public Player(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed,
                      Vector2 stage, Texture2D bombTex)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.tex = tex;
            this.speed = speed;
            this.stage = stage;
            this.frameIndex = 0;
            this.bombTex = bombTex;
            this.bomb = new Bomb(game, spriteBatch, bombTex, position, stage);

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
            Vector2 originMultiplyer = new Vector2();

            //UP
            originMultiplyer.X = 0;
            originMultiplyer.Y = 0;
            for (int i = 0; i < 5; i++)
            {
                Rectangle r = new Rectangle((int)originMultiplyer.X * SPRITE_WIDTH + ((SPRITE_WIDTH + SPRITE_PADDING) * i),
                                            (int)originMultiplyer.Y * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesUp.Add(r);
            }

            //LEFT
            originMultiplyer.X = SPRITE_FRAME_LIMIT;
            originMultiplyer.Y = 1;
            for (int i = 1; i < 5; i++)
            {
                Rectangle r = new Rectangle((int)originMultiplyer.X * SPRITE_WIDTH + ((SPRITE_WIDTH + SPRITE_PADDING) * i) + SPRITE_PADDING,
                                            (int)originMultiplyer.Y * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesLeft.Add(r);
            }
           
            //RIGHT
            originMultiplyer.X = SPRITE_FRAME_LIMIT;
            originMultiplyer.Y = 0;
            for (int i = 1; i < 5; i++)
            {
                Rectangle r = new Rectangle((int)originMultiplyer.X * SPRITE_WIDTH + ((SPRITE_WIDTH + SPRITE_PADDING) * i) + SPRITE_PADDING,
                                            (int)originMultiplyer.Y * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesRight.Add(r);
            }

            //DOWN
            originMultiplyer.X = 0;
            originMultiplyer.Y = 1;

            for (int i = 0; i < 5; i++)
            {
                Rectangle r = new Rectangle((int)originMultiplyer.X * SPRITE_WIDTH + ((SPRITE_WIDTH + SPRITE_PADDING) * i),
                                            (int)originMultiplyer.Y * SPRITE_HEIGHT, SPRITE_WIDTH, SPRITE_HEIGHT);
                framesDown.Add(r);
            }
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            KeyboardState ks = Keyboard.GetState(); 
            if (ks.IsKeyDown(Keys.W) && !ks.IsKeyDown(Keys.D) && !ks.IsKeyDown(Keys.A) && ! ks.IsKeyDown(Keys.S))
            {
                
                position.Y -= speed.Y;
                direction = UP;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
                delayCounter++;
            }
            if (ks.IsKeyDown(Keys.S) && !ks.IsKeyDown(Keys.D) && !ks.IsKeyDown(Keys.A) && !ks.IsKeyDown(Keys.W))
            {
                direction = DOWN;
                position.Y += speed.Y;
                if (position.Y > stage.Y - SPRITE_HEIGHT)
                {
                    position.Y = stage.Y - SPRITE_HEIGHT;
                }
                delayCounter++;
            }
            if (ks.IsKeyDown(Keys.A) && !ks.IsKeyDown(Keys.D) && !ks.IsKeyDown(Keys.W) && !ks.IsKeyDown(Keys.S))
            {
                direction = LEFT;
                position.X -= speed.X;
                if (position.X < 0)
                {
                    position.X = 0;

                }
                delayCounter++;
            }
            if (ks.IsKeyDown(Keys.D) && !ks.IsKeyDown(Keys.W) && !ks.IsKeyDown(Keys.A) && !ks.IsKeyDown(Keys.S))
            {
                direction = RIGHT;
                position.X += speed.X;
                if (position.X > stage.X - SPRITE_WIDTH)
                {
                    position.X = stage.X - SPRITE_WIDTH;
                }
                delayCounter++;
            }

            if (delayCounter > DELAY)
            {
                frameIndex++;
                delayCounter = 0;
            }
            if (frameIndex > 3)
            {
                frameIndex = 1;
            }

            if (ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.S) && ks.IsKeyUp(Keys.A) && ks.IsKeyUp(Keys.D))
            {
                frameIndex = 0;
            }

            if (ks.IsKeyDown(Keys.Space) && bomb.BombList.Count < 1)
            {
                bomb.BombList.Add(new Bomb(Game, spriteBatch, bombTex, position, stage));
                Game.Components.Add(bomb);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch(direction)
            {
                case UP:
                    spriteBatch.Draw(tex, position,
                    framesUp.ElementAt(frameIndex), Color.White);

                break;
                case LEFT:
                    spriteBatch.Draw(tex, position,
                    framesLeft.ElementAt(frameIndex), Color.White);
                break;
                case RIGHT:
                    spriteBatch.Draw(tex, position,
                    framesRight.ElementAt(frameIndex), Color.White);
                break;
                case DOWN:
                    spriteBatch.Draw(tex, position,
                    framesDown.ElementAt(frameIndex), Color.White);
                break;
                default:
                spriteBatch.Draw(tex, position,
                framesDown.ElementAt(frameIndex), Color.White);
                break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y, SPRITE_WIDTH, SPRITE_HEIGHT);
        }
    }
}
