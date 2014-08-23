using Core.Characters;
using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class Game
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

        Texture2D bikeTexture;
        TmxMapLoader mapLoader;
        ContentManager content;

        public Game() 
        {
        }

        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            mainTarget = new RenderTarget2D(graphicsDevice, Resolution.Width, Resolution.Height);
            negate = content.Load<Effect>(@"shaders/negate");
            mapLoader = new TmxMapLoader(content, "maps");

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            bikeTexture = content.Load<Texture2D>(@"gfx/bike");
        }

        public void LoadMap(string name)
        {
            map = new Map(mapLoader.Load(name));
            map.Load(content);

            character = new Character(bikeTexture, map);
            character.SetScale(1.4f);
            var startPosition = map.StartPosition;
            character.SetPosition(startPosition.X, startPosition.Y);

            goal = map.Goal;

            negativityFlipWait = new TimerTrig(0);
            negativityFlipWait.Update(0);

            Restart();
        }

        private void Finish()
        {
            state = GameState.Finished;
        }

        public GameState State { get { return state; } }

        private void Restart()
        {
            var startPosition = map.StartPosition;
            character.SetPosition(startPosition.X, startPosition.Y);
            character.rotation = 0f;
            character.Alive = true;
            SetNegativity(PlaneState.Positive);
            state = GameState.WaitingToStart;
            negativityFlipWait.Reset();
            character.ClearSpeed();
        }

        public void SetNegativity(PlaneState planeState)
        {
            this.planeState = planeState;
            negate.Parameters["negate"].SetValue(planeState == PlaneState.Positive ? false : true);
            map.SetPlaneState(planeState);
            negativityFlipWait.Reset();
        }

        public void Update(float dt, Camera cam) 
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
                Audio.Audio.I.PlayLooped("engine");
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
                    Audio.Audio.I.Play("explosion");
                    Audio.Audio.I.Stop("engine");
                    Restart();
                }
                else
                {
                    if (goal.Intersects(character.Bounds))
                    {
                        Audio.Audio.I.Stop("engine");
                        Finish();
                    }
                }
            }

            cam.Move(-character.position.X, -character.position.Y);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Camera cam) 
        {
            graphicsDevice.SetRenderTarget(mainTarget);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
            map.Draw(spriteBatch);
            character.Draw(spriteBatch);
            character.DrawDebug(spriteBatch, pixel);
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, negate);
            spriteBatch.Draw(mainTarget, new Rectangle(0, 0, Resolution.Width, Resolution.Height), Color.White);
            spriteBatch.End();
        }
    }
}
