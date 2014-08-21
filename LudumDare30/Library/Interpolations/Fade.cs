using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Interpolations
{
    public class Fade : Interpolation
    {
        public override float Apply(float a)
        {
            return MathHelper.Clamp(a * a * a * (a * (a * 6 - 15) + 10), 0, 1);
        }
    }
}
