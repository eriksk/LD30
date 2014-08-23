using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.TMX
{
    public class Layer
    {
        public int[] data;
        public int width, height;
        public string name;
        public float opacity;
        public string type;
        public bool visible;
        public int x, y;
        public string draworder;
        public MapObject[] objects;
        public Dictionary<string, string> properties;

        public Layer()
        {
            objects = new MapObject[0];
            properties = new Dictionary<string, string>();
        }
    }
}
