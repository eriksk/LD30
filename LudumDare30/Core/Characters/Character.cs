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
    public class Character : GameObject
    {
        public Vector2 velocity;

        public Character(Texture2D texture)
            :base(texture)
        {
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
            position.Y += velocity.Y * dt;

            base.Update(dt);
        }
    }
}
