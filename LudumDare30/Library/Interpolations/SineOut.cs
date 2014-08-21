using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Interpolations
{
    public class SineOut : Interpolation
    {
        public override float Apply(float a)
        {
            return (float)Math.Sin(a * (float)Math.PI / 2);
        }
    }
}
