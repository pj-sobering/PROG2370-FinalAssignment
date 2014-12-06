using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace finalProject
{
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
