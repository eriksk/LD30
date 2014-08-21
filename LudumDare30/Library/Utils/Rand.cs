using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils
{
    public class Rand
    {
        static Random rand = new Random(DateTime.Now.Millisecond);

        public static float Next() 
        {
            return (float)rand.NextDouble();
        }

        public static int Next(int max) 
        {
            return rand.Next(max);
        }
        public static int Next(int min, int max)
        {
            if (min > max)
                return min;

            return rand.Next(min, max);
        }
        public static float Next(float min, float max)
        {
            return min + (max - min) * Next();
        }
    }
}
