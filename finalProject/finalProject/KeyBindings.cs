/* KeyBindings.cs
 * Kim Thanh Thai, 
 * Paul Sobering 
 * Created Dec 6 2014 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace finalProject
{
    /// <summary>
    /// Stores the keybindings to be used by a player.
    /// </summary>
    public class KeyBindings
    {
        private Keys up;

        public Keys Up
        {
            get { return up; }
            set { up = value; }
        }
        private Keys down;

        public Keys Down
        {
            get { return down; }
            set { down = value; }
        }
        private Keys left;

        public Keys Left
        {
            get { return left; }
            set { left = value; }
        }
        private Keys right;

        public Keys Right
        {
            get { return right; }
            set { right = value; }
        }
        private Keys bomb;

        public Keys Bomb
        {
            get { return bomb; }
            set { bomb = value; }
        }

        /// <summary>
        /// Creates an object that binds keys to possible player actions.
        /// </summary>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="bomb"></param>
        public KeyBindings(Keys up, Keys down, Keys left, Keys right, Keys bomb)
        {
            this.Up = up;
            this.Down = down;
            this.Left = left;
            this.Right = right;
            this.Bomb = bomb;
        }
    }
}
