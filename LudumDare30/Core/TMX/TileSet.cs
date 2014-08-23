using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.TMX
{
    public class TileSet
    {
        public string name;
        public int imagewidth, imageheight;
        public int tilewidth, tileheight;
        public Dictionary<string, string> properties;

        public TileSet()
        {
            properties = new Dictionary<string, string>();
        }
    }
}
