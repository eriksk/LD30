using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Maps
{
    public class Cell
    {
        public int col, row;

        public Cell()
        {

        }

        public Cell(int col, int row)
        {
            this.col = col;
            this.row = row;
        }
    }
}
