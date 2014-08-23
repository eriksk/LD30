using Core.Characters;
using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Metrics;
using se.skoggy.utils.Particles;
using se.skoggy.utils.Tweening;
using se.skoggy.utils.Tweening.Stock;
using se.skoggy.utils.UI;
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

        TimerTrig reloadAfterDeathTrig = new TimerTrig(1000);
        DrawableText deadText;
        SpriteFont font;

        ParticleManager particleManager;
        ParticleSystem explosion;

        public Game() 
        {
        }

        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.content = content;
            mainTarget = new RenderTarget2D(graphicsDevice, Resolution.Width, Resolution.Height);
            negate = content.Load<Effect>(@"shaders/negate");
            mapLoader = new TmxMapLoader(content, "maps");

            deadText = new DrawableText("DEAD", TextAlign.Center);
            font = content.Load<SpriteFont>(@"fonts/xirod_32");

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            bikeTexture = content.Load<Texture2D>(@"gfx/bike");

            particleManager = new ParticleManager();
            particleManager.Load(content);

            ParticleSystemLoader particleLoader = new ParticleSystemLoader(content, "effects");

            explosion = new ParticleSystem(particleLoader.Load("fire_blast"));
            particleManager.AddSystem(explosion);
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

        public void Update(float dt, Camera cam, TweenManager tweenManager) 
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

                        Audio.Audio.I.Play("switch_positive");
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
                    state = GameState.Died;
                    reloadAfterDeathTrig.Reset();
                    tweenManager.Add(new ScaleXYTween(deadText, Interpolation.Elastic, 1000f, 0f, 3f));
                    explosion.position.X = character.position.X;
                    explosion.position.Y = character.position.Y;
                    explosion.Reset();
                    explosion.Play();
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
            else if (state == GameState.Died) 
            {
                if (reloadAfterDeathTrig.IsTrigged(dt) || (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))) 
                {
                    Restart();
                }
            }

            particleManager.Update(dt);
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

            particleManager.Draw(spriteBatch, cam);

            if (state == GameState.Died)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Projection);
                deadText.SetPosition(character.position.X, character.position.Y);
                deadText.Draw(spriteBatch, font);
                spriteBatch.End();
            }

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, negate);
            spriteBatch.Draw(mainTarget, new Rectangle(0, 0, Resolution.Width, Resolution.Height), Color.White);
            spriteBatch.End();
        }
    }
}
