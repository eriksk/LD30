using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Ranges
{
    public class ColorRange
    {
        public Color min, max;

        public ColorRange() 
        {
        }

        public Color Random() 
        {
            if (Rand.Next() > 0.5f)
                return max;
            return min;
        }

        public void Set(ColorRange range)
        {
            this.min = range.min;
            this.max = range.max;
        }
    }
}
