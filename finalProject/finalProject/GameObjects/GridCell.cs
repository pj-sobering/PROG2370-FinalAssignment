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

        ContentManager.BlockType blockType; 

        public ContentManager.BlockType BlockType
        {
            get { return blockType; }
            set { blockType = value; }
        }

        public GridCell(Rectangle destination, ContentManager.BlockType blockType)
        {
            this.Destination = destination;
            this.blockType = blockType;
        }
    }
}
