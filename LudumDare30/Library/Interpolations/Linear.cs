using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Interpolations
{
    public class Linear : Interpolation
    {
        public override float Apply(float a)
        {
            return a;
        }
    }
}
