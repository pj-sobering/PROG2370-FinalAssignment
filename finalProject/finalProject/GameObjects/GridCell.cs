using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace finalProject
{
    public class GridCell
    {
        
        Rectangle destination;

        public Rectangle Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        Vector2 coords;

        public Vector2 Coords
        {
            get { return coords; }
            set { coords = value; }
        }

        Wall wall;

        public Wall Wall
        {
            get { return wall; }
            set { wall = value; }
        }

        public GridCell(Rectangle destination, Vector2 coords, Wall wall = null)
        {
            this.Destination = destination;
            this.coords = coords;
            this.wall = wall;
        }

        
    }
}
