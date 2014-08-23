using Core.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Maps
{
    public class Door : MapItem
    {
        DoorType type;
        public string id;

        public Door(string id, Cell cell, DoorType type)
            :base(cell)
        {
            this.id = id;
            this.type = type;
        }

        public bool IsOpen(CollisionGrid grid) 
        {
            int value = grid.GetCell(cell.col, cell.row);
            if (type == DoorType.Vertical)
            {
                return value == 8;
            }
            else 
            {
                return value == 10;
            }
        }

        public void Toggle(CollisionGrid grid)
        {
            if (IsOpen(grid))
            {
                Close(grid);
            }
            else 
            {
                Open(grid);
            }
        }

        public void Open(CollisionGrid grid) 
        {
            if (type == DoorType.Vertical)
            {
                grid.SetCell(cell.col, cell.row, 8);
            }
            else 
            {
                grid.SetCell(cell.col, cell.row, 10);
            }
        }

        public void Close(CollisionGrid grid)
        {
            if (type == DoorType.Vertical)
            {
                grid.SetCell(cell.col, cell.row, 7);
            }
            else
            {
                grid.SetCell(cell.col, cell.row, 9);
            }
        }
    }
}
