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

        public GameScreen(IGameContext context)
            :base(context, "Ludum Dare 30", Resolution.Width, Resolution.Height)
        {
            TransitionDuration = 2000f;
        }

        public override void Load()
        {
            game = new Game();
            game.Load(content, context.GraphicsDevice);

            maps = File.ReadAllLines(content.RootDirectory + "/" + "maps/maps.txt");
            
            currentMap = 0;
            LoadCurrentMap();

            base.Load();
        }

        private void LoadCurrentMap()
        {
            game.LoadMap(maps[currentMap]);
        }

        public override void StateChanged()
        {
            base.StateChanged();
        }

        public override void Update(float dt)
        {
            if (game.State == GameState.Finished)
            {
                currentMap++;
                LoadCurrentMap();
            }
            else
            {
                game.Update(dt, cam);
            }
            
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();
            game.Draw(spriteBatch, context.GraphicsDevice, cam);
        }
    }
}
