using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Interpolations
{
    public abstract class Interpolation
    {
        public abstract float Apply(float a);

        public virtual float Apply(float start, float end, float a) 
        {
            return start + (end - start) * Apply(a);
        }

        public static readonly Interpolation Linear = new Linear();
        public static readonly Interpolation Pow2 = new Pow(2);
        public static readonly Interpolation Pow3 = new Pow(3);
        public static readonly Interpolation Pow4 = new Pow(4);
        public static readonly Interpolation Pow5 = new Pow(5);
        public static readonly Interpolation Exp5 = new Exp(2, 5);
        public static readonly Interpolation Exp10 = new Exp(2, 10);
        public static readonly Interpolation Elastic = new Elastic(2, 10);
    
    }
}
