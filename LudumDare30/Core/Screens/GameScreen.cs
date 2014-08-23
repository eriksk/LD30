using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Particles;
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
    public class GameScreen : BaseScreen
    {
        TmxMapLoader mapLoader;
        Map map;

        public GameScreen(IGameContext context)
            :base(context, "Ludum Dare 30", 1280, 720)
        {
            TransitionDuration = 2000f;
        }

        public override void Load()
        {
            mapLoader = new TmxMapLoader(content);
            map = new Map(mapLoader.Load("testmap"));
            base.Load();
        }

        public override void StateChanged()
        {
            base.StateChanged();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();
            context.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cam.Projection);
            spriteBatch.End();

        }
    }
}
