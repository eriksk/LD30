using Core.Collision;
using Core.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Characters
{
    public class Character : GameObject, ICollidable
    {
        public Vector2 velocity;
        public CollisionGrid grid;

        public Character(Texture2D texture, CollisionGrid grid)
            :base(texture)
        {
            this.grid = grid;
            velocity = Vector2.Zero;
        }


        KeyboardState oldKeys, keys;

        public override void Update(float dt)
        {
            oldKeys = keys;
            keys = Keyboard.GetState();

            float speed = 0.003f;

            if (keys.IsKeyDown(Keys.Left))
            {
                velocity.X -= speed * dt;
            }
            if (keys.IsKeyDown(Keys.Right))
            {
                velocity.X += speed * dt;
            }
            if (keys.IsKeyDown(Keys.Up))
            {
                velocity.Y -= speed * dt;
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                velocity.Y += speed * dt;
            }

            velocity *= 0.8f;

            position.X += velocity.X * dt;
            if (CorrectPositionX()) 
            {
                velocity.X = 0f;
            }
            position.Y += velocity.Y * dt;
            if (CorrectPositionY()) 
            {
                velocity.Y = 0f;
            }

            base.Update(dt);
        }

        private bool CorrectPositionY()
        {
            int col = (int)(Bounds.Center.X / Size);
            int row = (int)(Bounds.Center.Y / Size);

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (grid[col + i, row + j])
                    {
                        Rectangle cell = new Rectangle((col + i) * Size, (row + j) * Size, Size, Size);
                        if (cell.Intersects(Bounds))
                        {
                            if (cell.Center.Y > Bounds.Center.Y)
                            {
                                position.Y = cell.Top - 16;
                                return true;
                            }
                            else
                            {
                                position.Y = cell.Bottom + 16;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CorrectPositionX()
        {
            int col = (int)(Bounds.Center.X / Size);
            int row = (int)(Bounds.Center.Y / Size);

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (grid[col + i, row + j])
                    {
                        Rectangle cell = new Rectangle((col + i) * Size, (row + j) * Size, Size, Size);
                        if (cell.Intersects(Bounds))
                        {
                            if (cell.Center.X > Bounds.Center.X)
                            {
                                position.X = cell.Left - 16;
                                return true;
                            }
                            else 
                            {
                                position.X = cell.Right + 16;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X - 16, (int)position.Y - 16, 32, 32); }
        }

        public int Size { get { return 32; } }

        public void DrawDebug(SpriteBatch spriteBatch, Texture2D pixel)
        {
            int col = (int)(Bounds.Center.X / Size);
            int row = (int)(Bounds.Center.Y / Size);

            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (grid[col + i, row + j])
                    {
                        Rectangle cell = new Rectangle((col + i) * Size, (row + j) * Size, Size, Size);
                        if (cell.Intersects(Bounds))
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
