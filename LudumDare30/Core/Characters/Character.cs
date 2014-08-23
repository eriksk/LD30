using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Characters
{
    public class Character : GameObject
    {
        public Character(Texture2D texture)
            :base(texture)
        {

        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
