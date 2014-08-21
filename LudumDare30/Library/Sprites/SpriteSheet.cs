using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Sprites
{
    public class SpriteSheet
    {
        public int width;
        public int height;
        public int tileWidth;
        public int tileHeight;
        public int columns, rows;

        public SpriteSheet(int width, int height, int tileWidth, int tileHeight) 
        {
            this.width = width;
            this.height = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            columns = width / tileWidth;
            rows = height / tileHeight;
        }

        public Rectangle GetPart(int col, int row) 
        {
            return GetPart(col, row, 1, 1);
        }
        
        public Rectangle GetPart(int col, int row, int columns, int rows)
        {
            return new Rectangle(col * tileWidth, row * tileHeight, tileWidth * columns, tileHeight * rows);
        }

        public Rectangle GetPart(int index)
        {
            int col = index % columns;
            int row = index / rows;

            return GetPart(col, row, 1, 1);
        }
    }
}
