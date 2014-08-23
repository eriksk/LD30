using Core.Characters;
using Core.Collision;
using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        Texture2D pixel;

        public CharacterTestScreen(IGameContext context)
            :base(context, "character test", Resolution.Width, Resolution.Height)
        {

        }

        public override void Load()
        {


            mainTarget = new RenderTarget2D(context.GraphicsDevice, width, height);
            negate = content.Load<Effect>(@"shaders/negate");

            pixel = new Texture2D(context.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[]{ Color.White });

            TmxMapLoader mapLoader = new TmxMapLoader(content, "maps");

            map = new Map(mapLoader.Load("testmap"));
            map.Load(content);

            character = new Character(content.Load<Texture2D>(@"gfx/player"), map.CollisionGrid);
            character.SetPosition(4 * 32, 3 * 32);

            base.Load();
        }

        KeyboardState keys, oldKeys;

        bool negative = false;

        public override void Update(float dt)
        {
            oldKeys = keys;
            keys = Keyboard.GetState();

            character.Update(dt);

            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space)) 
            {
                negative = !negative;
                negate.Parameters["negate"].SetValue(negative);
            }
            
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
            character.DrawDebug(spriteBatch, pixel);
            spriteBatch.End();

            context.GraphicsDevice.SetRenderTarget(null);
            context.GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, negate);
            spriteBatch.Draw(mainTarget, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();
        }
    }
}
