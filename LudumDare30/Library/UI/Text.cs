using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.UI
{
    public class Text : ITweenable
    {
        public Vector2 position, origin, scale;
        public Color color;
        public float rotation;

        public SpriteEffects flip;
        private readonly TextAlign align;
        protected string text;
        private bool dirty;

        public Text(string text, TextAlign align = TextAlign.Left)
        {
            this.text = text;
            this.align = align;
            color = Color.White;
            position = new Vector2();
            origin = new Vector2(0.5f, 0.5f);
            scale = new Vector2(1f, 1f);
            flip = SpriteEffects.None;
            rotation = 0f;
            dirty = true;
        }

        private void Clean(SpriteFont font)
        {
            var size = font.MeasureString(text);

            switch (align)
            {
                case TextAlign.Left:
                    origin.X = 0f * size.X;
                    break;
                case TextAlign.Center:
                    origin.X = 0.5f * size.X;
                    break;
                case TextAlign.Right:
                    origin.X = 1f * size.X;
                    break;
            }

            origin.Y = 0.5f * size.Y;

            dirty = false;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (dirty)
                Clean(font);

            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, flip, 0f);
        }
        
        #region ITweenable

        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
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

        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }

        public void SetScale(float scalar)
        {
            scale.X = scalar;
            scale.Y = scalar;
        }

        public void SetScale(float x, float y)
        {
            scale.X = x;
            scale.Y = y;
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
