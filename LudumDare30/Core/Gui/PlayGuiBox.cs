using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Tweening;
using se.skoggy.utils.Tweening.Stock;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Gui
{
    public class PlayGuiBox
    {
        DrawableText pressToPlay;
        DrawableText deathCountText;

        public PlayGuiBox()
        {
            pressToPlay = new DrawableText("Press up arrow to start", TextAlign.Center);
            deathCountText = new DrawableText("Death Count", TextAlign.Center);
            deathCountText.color = Color.Red;
        }
        
        public void Show(TweenManager tweenManager, int deathCount)
        {
            deathCountText.Content = "Death Count: " + deathCount;
            tweenManager.Add(new PositionTween(pressToPlay, Interpolation.Elastic, 1000f, new Vector2(0, 0), new Vector2(0, -64f)));
            tweenManager.Add(new ScaleXYTween(pressToPlay, Interpolation.Elastic, 1000f, 0f, 2f));
            tweenManager.Add(new PositionTween(deathCountText, Interpolation.Elastic, 500f, new Vector2(0, 0), new Vector2(0, 64f)));
            tweenManager.Add(new ScaleXYTween(deathCountText, Interpolation.Elastic, 500f, 0f, 2f));
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            pressToPlay.Draw(spriteBatch, font);
            deathCountText.Draw(spriteBatch, font);
        }
    }
}
