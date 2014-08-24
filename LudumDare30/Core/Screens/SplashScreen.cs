using Core.Globals;
using Library.Tweening.Stock;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Metrics;
using se.skoggy.utils.Screens;
using se.skoggy.utils.Tweening.Stock;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class SplashScreen : BaseScreen
    {
        DrawableText gameName;
        GameObject overlay;
        SpriteFont font;

        TimerTrig wait;

        public SplashScreen(IGameContext context)
            :base(context, "Splash", Resolution.Width, Resolution.Height)
        {
            TransitionDuration = 1000;
            wait = new TimerTrig(2000f);
        }

        public override void Load()
        {
            Audio.Audio.I.PlayLooped("menu_song");

            gameName = new DrawableText("GLENN", TextAlign.Center);
            gameName.color = Colors.Primary;

            overlay = new GameObject(content.Load<Texture2D>(@"gfx/overlay"));

            font = content.Load<SpriteFont>(@"fonts/xirod_32");

            Tween(new ScaleXYTween(gameName, Interpolation.Pow2, TransitionDuration, 0f, 1f));

            base.Load();
        }

        public override void StateChanged()
        {
            base.StateChanged();
            if (TransitioningOut)
            {
                Tween(new ScaleXYTween(gameName, Interpolation.Pow2, TransitionDuration, gameName.scale.X, 0f));
            }
            if (Done) 
            {
                context.ChangeScreen(new IntroScreen(context));
            }
        }

        public override void Update(float dt)
        {
            if (Running) 
            {
                if (wait.IsTrigged(dt)) 
                {
                    TransitionOut();
                }
            }
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            context.GraphicsDevice.Clear(Colors.Background);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            gameName.Draw(spriteBatch, font);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            overlay.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
