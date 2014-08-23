using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gui
{
    public class DrawableRectangle
    {
        Rectangle rectangle;
        Color fillColor;
        Color outlineColor;
        Texture2D pixel;

        public DrawableRectangle(Texture2D pixel)
            : this(pixel, 0, 0, 0, 0, Color.White, Color.White)
        {
        }

        public DrawableRectangle(Texture2D pixel, int x, int y, int width, int height, Color fillColor, Color outlineColor)
        {
            this.pixel = pixel;
            rectangle = new Rectangle(x, y, width, height);
            this.fillColor = fillColor;
            this.outlineColor = outlineColor;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(pixel, rectangle, fillColor);
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), outlineColor); // left
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height), outlineColor); // right
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), outlineColor); // top
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width + 1, 1), outlineColor); // bottom
        }
    }
}
