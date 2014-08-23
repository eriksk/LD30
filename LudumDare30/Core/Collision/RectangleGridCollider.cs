using Core.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Collision
{
    public class RectangleGridCollider
    {
        CollisionGrid grid;
        ICollidable collidable;
        int cellSize;

        public RectangleGridCollider(CollisionGrid grid, ICollidable collidable, int cellSize) 
        {
            this.grid = grid;
            this.collidable = collidable;
            this.cellSize = cellSize;
        }

        Rectangle debugBounds;

        public void Correct() 
        {
            Rectangle bounds = collidable.Bounds;
            debugBounds = bounds;

            int col = (int)(bounds.Center.X / cellSize);
            int row = (int)(bounds.Center.Y / cellSize);

            Vector2 position = new Vector2();

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (grid[col + i, row + j])
                    {
                        if (new Rectangle((col + i) * cellSize, (row * j) * cellSize, cellSize, cellSize).Intersects(bounds)) 
                        {

                            float centerX = ((col + i) * cellSize) + cellSize / 2;
                            float centerY = ((row + j) * cellSize) + cellSize / 2;

                            if (centerX > bounds.Center.X)
                            {
                                position.X = ((col + i) * cellSize) + cellSize;
                            }
                            else 
                            {
                                position.X = ((col + i) * cellSize);
                            }
                        }
                    }
                }
            }
            
        }

        public void DrawDebug(SpriteBatch spriteBatch, Texture2D pixel)
        {
            int col = (int)(debugBounds.Center.X / cellSize);
            int row = (int)(debugBounds.Center.Y / cellSize);

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (grid[col + i, row + j])
                    {
                        Rectangle cell = new Rectangle((col + i) * cellSize, (row + j) * cellSize, cellSize, cellSize);
                        if (cell.Intersects(debugBounds))
                        {
                            new DrawableRectangle(pixel, cell.X, cell.Y, cell.Width, cell.Height, new Color(255, 0, 0, 30), Color.White)
                                .Draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}
