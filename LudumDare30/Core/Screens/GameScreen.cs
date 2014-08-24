using Core.Maps;
using Core.TMX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Particles;
using se.skoggy.utils.Screens;
using se.skoggy.utils.Tweening.Stock;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screens
{
    public class GameScreen : BaseScreen
    {
        Game game;
        string[] maps;
        int currentMap = 0;
        GameObject overlay;

        public GameScreen(IGameContext context)
            :base(context, "Ludum Dare 30", Resolution.Width, Resolution.Height)
        {
            TransitionDuration = 0f;
        }

        public override void Load()
        {
            game = new Game();
            game.Load(content, context.GraphicsDevice);

            maps = File.ReadAllLines(content.RootDirectory + "/" + "maps/maps.txt");

            overlay = new GameObject(content.Load<Texture2D>(@"gfx/overlay"));
            
            currentMap = 0;
            LoadCurrentMap();

            base.Load();
        }

        private void LoadCurrentMap()
        {
            game.LoadMap(maps[currentMap], tweenManager);
        }

        public override void StateChanged()
        {
            base.StateChanged();
            if (Done) 
            {
                context.ChangeScreen(new GameOverScreen(context));
            }
        }

        public override void Update(float dt)
        {
            if (Running)
            {
                if (game.State == GameState.Finished)
                {
                    currentMap++;
                    if (currentMap > maps.Length - 1)
                    {
                        TransitionOut();
                    }
                    else
                    {
                        LoadCurrentMap();
                    }
                }
                else
                {
                    game.Update(dt, cam, tweenManager);
                }
            }
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();
            game.Draw(spriteBatch, context.GraphicsDevice, cam);


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null);
            overlay.color = Color.White;
            overlay.color.A = 200;
            overlay.SetPosition((Resolution.Width / 2), (Resolution.Height / 2));
            overlay.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
