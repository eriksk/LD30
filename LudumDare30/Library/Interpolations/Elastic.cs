using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Interpolations
{
    public class Elastic : Interpolation
    {
		float value, power;

		public Elastic (float value, float power) 
        {
			this.value = value;
			this.power = power;
		}

        public override float Apply(float a)
        {
			if (a <= 0.5f) 
            {
				a *= 2;
				return (float)Math.Pow(value, power * (a - 1)) * (float)Math.Sin(a * 20) * 1.0955f / 2;
			}
			a = 1 - a;
			a *= 2;
            return 1 - (float)Math.Pow(value, power * (a - 1)) * (float)Math.Sin((a) * 20) * 1.0955f / 2;
		}
    }
}
