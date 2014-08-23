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

        public CharacterTestScreen(IGameContext context)
            :base(context, "character test", Resolution.Width, Resolution.Height)
        {

        }

        public override void Load()
        {



            base.Load();
        }


        public override void Update(float dt)
        {
            
            base.Update(dt);
        }

        public override void Draw()
        {
            base.Draw();

        }
    }
}
