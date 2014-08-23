using Core.Collision;
using Core.Gui;
using Core.Maps;
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
        Map map;
        bool alive;

        public Character(Texture2D texture, Map map)
            :base(texture)
        {
            this.map = map;
            velocity = Vector2.Zero;
            alive = true;
        }

        public bool Alive { get { return alive; } set { alive = value; } }

        public void ClearSpeed()
        {
            speed = 0;
            rotationSpeed = 0;
        }

        KeyboardState oldKeys, keys;
        float speed = 0f;
        float rotationSpeed = 0;
        float constantSpeed = 0.005f;
        float constantRotationSpeed = 0.4f;
        float maxSpeed = 0.005f;

        public float Speed { get { return (constantSpeed + speed) / maxSpeed; } }

        public override void Update(float dt)
        {
            oldKeys = keys;
            keys = Keyboard.GetState();


            rotationSpeed = (constantSpeed + speed) * 1.1f;

            if (keys.IsKeyDown(Keys.Left))
            {
                rotation -= (constantRotationSpeed * rotationSpeed) * dt;
            }
            if (keys.IsKeyDown(Keys.Right))
            {
                rotation += (constantRotationSpeed * rotationSpeed) * dt;
            }
            if (keys.IsKeyDown(Keys.Up)) 
            {
                speed += 0.00001f * dt;
                if (speed > maxSpeed) 
                {
                    speed = maxSpeed;
                }
            }

            velocity.X += (float)Math.Cos(rotation) * (constantSpeed + speed) * dt;
            velocity.Y += (float)Math.Sin(rotation) * (constantSpeed + speed) * dt;

            velocity *= 0.8f;

            position.X += velocity.X * dt;
            if (CorrectPositionX()) 
            {
                velocity.X = 0f;
                alive = false;
            }
            position.Y += velocity.Y * dt;
            if (CorrectPositionY()) 
            {
                velocity.Y = 0f;
                alive = false;
            }

            base.Update(dt);
        }

        private bool CorrectPositionY()
        {
            var grid = map.CollisionGrid;
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
            var grid = map.CollisionGrid;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            float shadowOffset = 4f;
            color = Color.Black;
            color.A = 80;
            position.X -= shadowOffset;
            position.Y += shadowOffset;
            base.Draw(spriteBatch);

            color = Color.White;
            position.X += shadowOffset;
            position.Y -= shadowOffset;
            base.Draw(spriteBatch);
        }

        public void DrawDebug(SpriteBatch spriteBatch, Texture2D pixel)
        {
            var grid = map.CollisionGrid;
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
