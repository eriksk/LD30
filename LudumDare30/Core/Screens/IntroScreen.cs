using Core.Conversations;
using Core.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Metrics;
using se.skoggy.utils.Screens;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class IntroScreen : BaseScreen
    {
        DrawableText text;
        SpriteFont font;

        DialogueWindow dialogueWindow;

        public IntroScreen(IGameContext context)
            : base(context, "Intro", Resolution.Width, Resolution.Height)
        {
        }

        public override void Load()
        {
            font = content.Load<SpriteFont>(@"fonts/xirod_32");

            Texture2D pixel = new Texture2D(context.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[]{ Color.White });

            dialogueWindow = new DialogueWindow(pixel, new Conversation(new[] { "Hello", "Welcome", "this is glenn" }, 1000), 0, 0);

            base.Load();
        }

        public override void Update(float dt)
        {
            dialogueWindow.Update(dt);

            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            context.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            dialogueWindow.Draw(spriteBatch, font);
            spriteBatch.End();
        }
    }
}
