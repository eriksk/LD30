using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Ranges
{
    public class Vector2Range
    {
        public Vector2 min, max;

        public Vector2Range()
        {

        }

        public float RandomX()
        {
            return Rand.Next(min.X, max.X);
        }

        public float RandomY()
        {
            return Rand.Next(min.Y, max.Y);
        }

        public Vector2 Random()
        {
            return Vector2.Lerp(min, max, Rand.Next());
        }

        public void Set(Vector2Range range)
        {
            this.min = range.min;
            this.max = range.max;
        }
    }
}
