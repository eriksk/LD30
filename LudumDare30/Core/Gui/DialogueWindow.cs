using Core.Conversations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gui
{
    public class DialogueWindow
    {
        DrawableRectangle bubble;
        Conversation conversation;
        DrawableText drawableText;

        public DialogueWindow(Texture2D pixel, Conversation conversation, int x, int y, int width = 256, int height = 128)
        {
            bubble = new DrawableRectangle(pixel, x - width / 2, y - height / 2, width, height, new Color(100, 100, 100, 100), Color.DarkGray);
            this.conversation = conversation;
            drawableText = new DrawableText(conversation.Text, TextAlign.Center);
            drawableText.SetPosition(x, y);
        }

        public bool Done { get { return conversation.Done; } }

        public void Update(float dt)
        {
            conversation.Update(dt);
            drawableText.Content = conversation.Text;
        }
        
        public void Draw(SpriteBatch spriteBatch, SpriteFont font) 
        {
            bubble.Draw(spriteBatch);
            drawableText.Draw(spriteBatch, font);
        }
    }
}
