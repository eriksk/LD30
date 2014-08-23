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

        public PlayGuiBox()
        {
            pressToPlay = new DrawableText("Press up arrow to start", TextAlign.Center);
        }
        
        public void Show(TweenManager tweenManager) 
        {
            tweenManager.Add(new ScaleXYTween(pressToPlay, Interpolation.Elastic, 1000f, 0f, 2f));
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            pressToPlay.Draw(spriteBatch, font);
        }
    }
}
