/* ContentManager.cs
 * Kim Thanh Thai, 
 * Paul Sobering 
 * Created Dec 6 2014 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Loads all content and makes it accessable throughout the project.
    /// </summary>
    public static class ContentManager
    {
        static Vector2 stage;

        public static Vector2 Stage
        {
            get { return ContentManager.stage; }
        }

        static Texture2D player1Tex;

        public static Texture2D Player1Tex
        {
            get { return ContentManager.player1Tex; }
        }
        static Texture2D player2Tex;

        public static Texture2D Player2Tex
        {
            get { return ContentManager.player2Tex; }
        }
        static Texture2D bombTex;

        public static Texture2D BombTex
        {
            get { return ContentManager.bombTex; }
        }
        static Texture2D explosionTex;

        public static Texture2D ExplosionTex
        {
            get { return ContentManager.explosionTex; }
        }
        static Texture2D monsterTex;

        public static Texture2D MonsterTex
        {
            get { return ContentManager.monsterTex; }
        }

        static Texture2D wallTex;

        public static Texture2D WallTex
        {
            get { return ContentManager.wallTex; }
        }

        private static Texture2D deathTex;

        public static Texture2D DeathTex
        {
            get { return ContentManager.deathTex; }
        }

        private static Texture2D splash;

        public static Texture2D Splash
        {
            get { return ContentManager.splash; }
            set { ContentManager.splash = value; }
        }

        static Song song;

        public static Song Song
        {
            get { return ContentManager.song; }
        }

        static SoundEffect bombDrop;

        public static SoundEffect BombDrop
        {
            get { return ContentManager.bombDrop; }
        }
        static SoundEffect explosion;

        public static SoundEffect Explosion
        {
            get { return ContentManager.explosion; }
        }
        static SoundEffect creatureDie;

        public static SoundEffect CreatureDie
        {
            get { return ContentManager.creatureDie; }

        }
        static SoundEffect playerDie;

        public static SoundEffect PlayerDie
        {
            get { return ContentManager.playerDie; }
        }

        private static SpriteFont font;

        public static SpriteFont Font
        {
            get { return ContentManager.font; }
            set { ContentManager.font = value; }
        }

        static public void LoadAll(Game1 game)
        {
            stage = new Vector2(game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight);
            player1Tex = game.Content.Load<Texture2D>("images/player1");
            player2Tex = game.Content.Load<Texture2D>("images/player2");
            bombTex = game.Content.Load<Texture2D>("images/bomb");
            explosionTex = game.Content.Load<Texture2D>("images/explosion");
            wallTex = game.Content.Load<Texture2D>("images/box");
            deathTex = game.Content.Load<Texture2D>("images/death");
            monsterTex = game.Content.Load<Texture2D>("images/monster1");
            font = game.Content.Load<SpriteFont>("fonts/regularFont");
            song = game.Content.Load<Song>("sounds/battle");
            splash = game.Content.Load<Texture2D>("images/background");
        }
    }
}
