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
    public class Player1 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Texture2D tex;
        private Vector2 speed;
        private Vector2 stage;
        private int delay = 5;


        public Player1(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed, Vector2 stage)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.tex = tex;
            this.speed = speed;
            this.stage = stage;
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

            KeyboardState ks = Keyboard.GetState();
            Texture2D p1Front = Game.Content.Load<Texture2D>("p1Images/frontStand");
            Texture2D p1Back = Game.Content.Load<Texture2D>("p1Images/backStand");

            if (ks.IsKeyDown(Keys.W))
            {
                position.Y -= speed.Y;
                if (position.Y < 0)
                {
                    position.Y = 0;
                    
                }
            }
            if (ks.IsKeyDown(Keys.S))
            {
                position.Y += speed.Y;
                if (position.Y > stage.Y - tex.Height)
                {
                    position.Y = stage.Y - tex.Height;
                }
            }
            if (ks.IsKeyDown(Keys.A))
            {
                position.X -= speed.X;
                if (position.X < 0)
                {
                    position.X = 0;

                }
            }
            if (ks.IsKeyDown(Keys.D))
            {
                position.X += speed.X;
                if (position.X > stage.X - tex.Width)
                {
                    position.X = stage.X - tex.Width;

                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
