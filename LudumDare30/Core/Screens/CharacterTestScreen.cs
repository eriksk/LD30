using Core.Characters;
using Core.Collision;
using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.Metrics;
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
        TimerTrig negativityFlipWait;

        GameState state = GameState.WaitingToStart;
        KeyboardState keys, oldKeys;

        PlaneState planeState;
        Rectangle goal;

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

            character = new Character(content.Load<Texture2D>(@"gfx/bike"), map);
            character.SetScale(1.4f);
            var startPosition = map.StartPosition;
            character.SetPosition(startPosition.X, startPosition.Y);

            goal = map.Goal;

            negativityFlipWait = new TimerTrig(0);
            negativityFlipWait.Update(0);

            Restart();

            base.Load();
        }
        
        private void Restart()
        {
            var startPosition = map.StartPosition;
            character.SetPosition(startPosition.X, startPosition.Y);
            character.rotation = 0f;
            character.Alive = true;
            SetNegativity(PlaneState.Positive);
            state = GameState.WaitingToStart;
            negativityFlipWait.Reset();
            cam.SetPosition(-character.position.X, -character.position.Y);
            character.ClearSpeed();
        }

        private void Finish()
        {
            Restart();
        }

        public void SetNegativity(PlaneState planeState)
        {
            this.planeState = planeState;
            negate.Parameters["negate"].SetValue(planeState == PlaneState.Positive ? false : true);
            map.SetPlaneState(planeState);
            negativityFlipWait.Reset();
        }

        public override void Update(float dt)
        {
            oldKeys = keys;
            keys = Keyboard.GetState();

            if (state == GameState.WaitingToStart) 
            {
                if (keys.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up))
                {
                    state = GameState.Playing;
                }
            }
            else if (state == GameState.Playing)
            {
                if (negativityFlipWait.Done)
                {

                    character.Update(dt);

                    if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                    {
                        if (planeState == PlaneState.Positive)
                        {
                            SetNegativity(PlaneState.Negative);
                        }
                        else 
                        {
                            SetNegativity(PlaneState.Positive);
                        }
                    }
                }
                else
                {
                    negativityFlipWait.Update(dt);
                }

                if (!character.Alive)
                {
                    Restart();
                }
                else 
                {
                    if (goal.Intersects(character.Bounds)) 
                    {
                        Finish();
                    }
                }
                cam.Move(-character.position.X, -character.position.Y);
            }
            
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
