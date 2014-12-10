/* Player.cs
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
    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        const int SPRITE_PADDING = 20; // the space between images in the sprite file - this is removed from the sprite width to improve collision detection
        const int SPRITE_FRAME_LIMIT = 4; // number of frames per movement in the sprite file - Does not have to be constant, but in this case we only work with 4 frames per movement.
        const int DELAY = 10; // The number of frames to wait before progressing to the next frame of an animation (60 fps)
        const int BOMBS_PER_PLAYER = 1; // The number of bombs a player is allowed to drop at a time.
        const int SPRITE_WIDTH = 42; // width of a single image in the sprite file
        const int SPRITE_HEIGHT = 30; // height of a single image in the sprite file
        const int DEAD_TIME = 140; // the number of frames to wait before the death animation ends (60 fps).
        public enum State { Up, Down, Left, Right, Dead };

        public int Width
        {
            get { return SPRITE_WIDTH; }
        } 
        public int Height
        {
            get { return SPRITE_HEIGHT; }
        }

        private SpriteBatch spriteBatch;
        private State startState;
        private Vector2 startPosition; 

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

        private Game1 game;
        private List<Rectangle> framesUp = new List<Rectangle>();
        private List<Rectangle> framesDown = new List<Rectangle>();
        private List<Rectangle> framesLeft = new List<Rectangle>();
        private List<Rectangle> framesRight = new List<Rectangle>();
        private KeyBindings bindings;
        int delayCounter = 0;
        int frameIndex = 0;

        private State playerState;


        public State PlayerState
        {
            get { return playerState; }
            set { playerState = value; }
        }
        private Bomb[] bombArray = new Bomb[BOMBS_PER_PLAYER];
        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        private SoundEffect hit;
        private SoundEffect hit2;
        private int deadCounter = 0;
        public int DeadCounter
        {
            get { return deadCounter; }
            set { deadCounter = value; }
        }
        private int reviveCounter = 0;
        public int ReviveCounter
        {
            get { return reviveCounter; }
            set { reviveCounter = value; }
        }

        /// <summary>
        /// Constructs a Player object
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="stage"></param>
        /// <param name="keyBindings"></param>
        public Player(Game1 game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed,
            KeyBindings keyBindings, State state)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.tex = tex;
            this.speed = speed;
            this.frameIndex = 0;
            this.bindings = keyBindings;
            this.DrawOrder = 2;
            this.game = game;
            this.playerState = state;
            this.startPosition = position;
            this.startState = state;
            this.hit = game.Content.Load<SoundEffect>("sounds/bombPlanted");
            this.hit2 = game.Content.Load<SoundEffect>("sounds/sadTrombone");
            AnimationFrames();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            for (int i = 0; i < bombArray.Length; i++)
            {
                bombArray[i] = null;
            }
            base.Initialize();
        }

        /// <summary>
        /// Creates lists of Rectangles to use as viewports of a texture.
        /// </summary>
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
            if (playerState != State.Dead)
            {
                if (ks.IsKeyDown(bindings.Up) && !ks.IsKeyDown(bindings.Right)
                    && !ks.IsKeyDown(bindings.Left) && !ks.IsKeyDown(bindings.Down))
                {

                    position.Y -= speed.Y;
                    playerState = State.Up;
                    if (position.Y < 0)
                    {
                        position.Y = 0;
                    }
                    delayCounter++;
                }
                if (ks.IsKeyDown(bindings.Down) && !ks.IsKeyDown(bindings.Right)
                    && !ks.IsKeyDown(bindings.Left) && !ks.IsKeyDown(bindings.Right))
                {
                    playerState = State.Down;
                    position.Y += speed.Y;
                    if (position.Y > ContentManager.Stage.Y - SPRITE_HEIGHT)
                    {
                        position.Y = ContentManager.Stage.Y - SPRITE_HEIGHT;
                    }
                    delayCounter++;
                }
                if (ks.IsKeyDown(bindings.Left) && !ks.IsKeyDown(bindings.Right)
                    && !ks.IsKeyDown(bindings.Up) && !ks.IsKeyDown(bindings.Down))
                {
                    playerState = State.Left;
                    position.X -= speed.X;
                    if (position.X < 0)
                    {
                        position.X = 0;

                    }
                    delayCounter++;
                }
                if (ks.IsKeyDown(bindings.Right) && !ks.IsKeyDown(bindings.Up)
                    && !ks.IsKeyDown(bindings.Left) && !ks.IsKeyDown(bindings.Down))
                {
                    playerState = State.Right;
                    position.X += speed.X;
                    if (position.X > ContentManager.Stage.X - SPRITE_WIDTH)
                    {
                        position.X = ContentManager.Stage.X - SPRITE_WIDTH;
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

                if (ks.IsKeyUp(bindings.Up) && ks.IsKeyUp(bindings.Down)
                    && ks.IsKeyUp(bindings.Left) && ks.IsKeyUp(bindings.Right))
                {
                    frameIndex = 0;
                }


                if (ks.IsKeyDown(bindings.Bomb))
                {
                    for (int i = 0; i < bombArray.Length; i++)
                    {
                        if (bombArray[i] == null || bombArray[i].Enabled == false)
                        {
                            // Gets the players bounds and focuses the coordinates on a small point in the center 
                            // This gives the collision manager greater accuracy in determining a destination for the bomb
                            Rectangle playerSpace = getBounds();
                            playerSpace.X += SPRITE_WIDTH / 2;
                            playerSpace.Y += SPRITE_HEIGHT / 2;
                            playerSpace.Height = 1;
                            playerSpace.Width = 1;
                            GridCell gridCell = CollisionManager.EmptySpace(playerSpace, game.actionScene.grid);
                            bombArray[i] = new Bomb(game, spriteBatch, gridCell.Destination, game.actionScene.grid, gridCell.Coords, this, hit);
                            game.Components.Add(bombArray[i]);
                            bombArray[i].DrawOrder = 0;
                            hit.Play();
                            break;
                        }
                    }

                }
            }
            else
            {
                // Death animation
                if (deadCounter <= DEAD_TIME)
                {
                    position.Y -= 0.3f;
                    deadCounter++;
                }
                else
                {
                    deadCounter = 0;
                    RevivePlayer();
                    reviveCounter++;
                    if (reviveCounter == 3)
                    {
                        hit2.Play();
                        MediaPlayer.Stop();
                        this.Enabled = false;
                    }
                }
            }

            base.Update(gameTime);
        }

        public void RevivePlayer()
        {
            this.Position = startPosition;
            this.PlayerState = startState;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
            switch(playerState)
            {
                    
                case State.Up:
                    spriteBatch.Draw(tex, position, framesUp.ElementAt(frameIndex), Color.White, 0, new Vector2(0,0), 1f, SpriteEffects.None, 0);
                break;
                case State.Left:
                    spriteBatch.Draw(tex, position, framesLeft.ElementAt(frameIndex), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                break;
                case State.Right:
                    spriteBatch.Draw(tex, position, framesRight.ElementAt(frameIndex), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                break;
                case State.Down:
                    spriteBatch.Draw(tex, position, framesDown.ElementAt(frameIndex), Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                break;
                case State.Dead:
                    spriteBatch.Draw(ContentManager.DeathTex, position, Color.White);
                break;
                default:
                    spriteBatch.Draw(tex, position,
                    framesDown.ElementAt(frameIndex), Color.White);
                break;
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
