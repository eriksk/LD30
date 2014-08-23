using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Screens;
using se.skoggy.utils.Tweening.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class MapTestScreen : BaseScreen
    {
        TmxMapLoader mapLoader;
        Map map;

        public MapTestScreen(IGameContext context)
            :base(context, "maptest", Resolution.Width, Resolution.Height)
        {

        }

        public override void Load()
        {
            mapLoader = new TmxMapLoader(content, "maps");

            map = new Map(mapLoader.Load("testmap"));
            map.Load(content);

            base.Load();
        }
        

        public override void Update(float dt)
        {
            map.Update(dt);
            cam.SetPosition(-map.Center.X, -map.Center.Y);
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            map.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
