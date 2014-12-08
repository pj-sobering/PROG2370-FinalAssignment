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

        public enum CellType { Empty, Wall, Destructable }

        CellType type;
        public CellType Type
        {
            get { return type; }
            set { type = value; }
        }

        public GridCell(Rectangle destination, CellType blockType)
        {
            this.Destination = destination;
            this.type = blockType;
        }
    }
}
