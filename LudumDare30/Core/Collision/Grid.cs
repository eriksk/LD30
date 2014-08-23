using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Collision
{
    public class CollisionGrid
    {
        bool[] data;
        int width;
        int height;

        public CollisionGrid(bool[] data, int width, int height) 
        {
            this.data = data;
            this.width = width;
            this.height = height;
        }

        public bool this[int col, int row]
        {
            get
            {
                if (col < 0 || col > width - 1)
                    return true;
                if (row < 0 || row > height - 1)
                    return true;

                return data[col + row * width];
            }
        }
    }
}
