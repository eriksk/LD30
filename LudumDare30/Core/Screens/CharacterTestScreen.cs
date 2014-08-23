using Core.Characters;
using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class CharacterTestScreen : BaseScreen
    {
        Character character;
        Map map;

        Effect negate;
        RenderTarget2D mainTarget;

        public CharacterTestScreen(IGameContext context)
            :base(context, "character test", Resolution.Width, Resolution.Height)
        {

        }

        public override void Load()
        {
            character = new Character(content.Load<Texture2D>(@"gfx/player"));

            mainTarget = new RenderTarget2D(context.GraphicsDevice, width, height);
            negate = content.Load<Effect>(@"shaders/negate");

            TmxMapLoader mapLoader = new TmxMapLoader(content, "maps");

            map = new Map(mapLoader.Load("testmap"));
            map.Load(content);

            base.Load();
        }

        public override void Update(float dt)
        {
            character.Update(dt);

            negate.Parameters["negate"].SetValue(false);

            cam.SetPosition(-map.Center.X, -map.Center.Y);
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

            context.GraphicsDevice.SetRenderTarget(mainTarget);
            context.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            map.Draw(spriteBatch);
            character.Draw(spriteBatch);
            spriteBatch.End();

            context.GraphicsDevice.SetRenderTarget(null);
            context.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, negate);
            spriteBatch.Draw(mainTarget, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();
        }
    }
}
