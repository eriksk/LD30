using Core.Characters;
using Core.Maps;
using Core.TMX;
using Core.Trails;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class FlatLandTestDriveScreen : BaseScreen
    {

        Character glenn;
        Map map;
        TrailManager trailManager;

        public FlatLandTestDriveScreen(IGameContext context)
            :base(context, "test", Resolution.Width, Resolution.Height)
        {

        }

        public override void Load()
        {
            map = new Map(new TmxMapLoader(content, "maps").Load("flatland"));
            map.Load(content);
            glenn = new Character(content.Load<Texture2D>(@"gfx/car"), map);

            trailManager = new TrailManager();
            trailManager.Load(content);

            base.Load();
        }

        public override void Update(float dt)
        {
            map.Update(dt);
            glenn.Update(dt);

            trailManager.Update(dt, glenn);

            cam.Move(-glenn.position.X, -glenn.position.Y);
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            map.DrawBackground(spriteBatch, glenn.position);
            trailManager.Draw(spriteBatch);
            map.DrawForeground(spriteBatch, glenn.position);
            glenn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
