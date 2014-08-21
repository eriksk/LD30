using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.GameObjects
{
    public class GameObject : ITweenable
    {
        protected Vector2 originInPixels;

        public Vector2 position, scale, origin;
        public Color color;
        public Texture2D texture;
        public Rectangle source;
        public SpriteEffects flip;
        public float rotation;

        public GameObject()
            : this(null)
        {
        }

        public GameObject(Texture2D texture)
        {
            this.texture = texture;
            color = Color.White;
            position = new Vector2();
            scale = new Vector2(1f, 1f);
            origin = new Vector2(0.5f, 0.5f);
            rotation = 0f;
            source = texture == null ? new Rectangle() : new Rectangle(0, 0, texture.Width, texture.Height);
            flip = SpriteEffects.None;
            originInPixels = new Vector2();
        }

        #region Setters

        public virtual GameObject SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
            return this;
        }
        public virtual GameObject SetPosition(Vector2 position)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
            return this;
        }
        public virtual GameObject SetScale(float scalar)
        {
            scale.X = scalar;
            scale.Y = scalar;
            return this;
        }
        public virtual GameObject SetScale(float x, float y)
        {
            scale.X = x;
            scale.Y = y;
            return this;
        }
        public virtual GameObject SetRotation(float rotation)
        {
            this.rotation = rotation;
            return this;
        }
        public virtual GameObject SetSource(int x, int y, int width, int height)
        {
            source.X = x;
            source.Y = y;
            source.Width = width;
            source.Height = height;
            return this;
        }
        public virtual GameObject SetFlip(SpriteEffects flip)
        {
            this.flip = flip;
            return this;
        }
        public virtual GameObject SetOrigin(float x, float y)
        {
            origin.X = x;
            origin.Y = y;
            return this;
        }

        #endregion

        public virtual void Update(float dt)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            originInPixels.X = source.Width * origin.X;
            originInPixels.Y = source.Height * origin.Y;

            spriteBatch.Draw(texture, position, source, color, rotation, originInPixels, scale, flip, 0f);
        }

        #region ITweenable

        void ITweenable.SetPosition(float x, float y)
        {
            SetPosition(x, y);
        }

        public void SetPositionX(float x)
        {
            position.X = x;
        }

        public void SetPositionY(float y)
        {
            position.Y = y;
        }

        public void AddRotation(float rotationToAdd)
        {
            rotation += rotationToAdd;
        }

        void ITweenable.SetRotation(float rotation)
        {
            SetRotation(rotation);
        }

        void ITweenable.SetScale(float scalar)
        {
            SetScale(scalar);
        }

        void ITweenable.SetScale(float x, float y)
        {
            SetScale(x, y);
        }

        public void SetAlpha(byte a)
        {
            color.A = a;
        }

        public void SetColor(byte r, byte g, byte b, byte a)
        {
            color.R = r;
            color.G = g;
            color.B = b;
            color.A = a;
        }
        
        #endregion
    }
}