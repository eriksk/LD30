using Core.Globals;
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
    public class GameOverScreen : BaseScreen
    {
        DrawableText gameOverText, thanksForPlaying;
        SpriteFont font;

        TimerTrig quitTimer;
        GameObject overlay;

        public GameOverScreen(IGameContext context)
            :base(context, "Game Over", Resolution.Width, Resolution.Height)
        {
        }

        public override void Load()
        {
            gameOverText = new DrawableText("Game Over", TextAlign.Center);
            thanksForPlaying = new DrawableText("Thanks for playing", TextAlign.Center);
            gameOverText.color = Colors.Primary;
            thanksForPlaying.color = Colors.Primary;

            Tween(new ScaleXYTween(gameOverText, Interpolation.Elastic, TransitionDuration, 0f, 3f));
            Tween(new PositionTween(gameOverText, Interpolation.Elastic, TransitionDuration, Vector2.Zero, new Vector2(0, -128)));
            Tween(new ScaleXYTween(thanksForPlaying, Interpolation.Elastic, TransitionDuration, 0f, 2f));

            
            font = content.Load<SpriteFont>(@"fonts/xirod_32");

            overlay = new GameObject(content.Load<Texture2D>(@"gfx/overlay"));

            quitTimer = new TimerTrig(3000);

            base.Load();
        }

        public override void StateChanged()
        {
            base.StateChanged();
            if (Done) 
            {
                context.Exit();
            }
            if (TransitioningOut)
            {
                Tween(new ScaleXYTween(gameOverText, Interpolation.Elastic, TransitionDuration, 3f, 0f));
                Tween(new PositionTween(gameOverText, Interpolation.Elastic, TransitionDuration, new Vector2(0, -128), Vector2.Zero));
                Tween(new ScaleXYTween(thanksForPlaying, Interpolation.Elastic, TransitionDuration, 2f, 0f));
            }
        }

        public override void Update(float dt)
        {
            if (Running) 
            {
                if (quitTimer.IsTrigged(dt)) 
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
            gameOverText.Draw(spriteBatch, font);
            thanksForPlaying.Draw(spriteBatch, font);
            overlay.Draw(spriteBatch);
            spriteBatch.End();

        }
    }
}
