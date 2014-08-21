using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Interpolations
{
    public class SineIn : Interpolation
    {
        public override float Apply(float a)
        {
            return 1 - (float)Math.Cos(a * (float)Math.PI / 2); 
        }
    }
}
