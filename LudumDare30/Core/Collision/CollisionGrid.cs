using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Collision
{
    public class CollisionGrid
    {
        int[] data;
        int width;
        int height;
        int[] nonCollidableCells;

        public CollisionGrid(int[] data, int width, int height) 
        {
            this.data = data;
            this.width = width;
            this.height = height;
            nonCollidableCells = new int[] 
            {
                0, 8, 10
            };
        }

        public void SetCell(int col, int row, int value)
        {
            data[col + row * width] = value;
        }

        public int GetCell(int col, int row)
        {
            return data[col + row * width];
        }

        public bool this[int col, int row]
        {
            get
            {
                if (col < 0 || col > width - 1)
                    return true;
                if (row < 0 || row > height - 1)
                    return true;

                return IsCollidable(data[col + row * width]);
            }
        }

        private bool IsCollidable(int value) 
        {
            bool collidable = true;
            for (int i = 0; i < nonCollidableCells.Length; i++)
            {
                if (nonCollidableCells[i] == value)
                    collidable = false;
            }
            return collidable;
        }
    }
}
