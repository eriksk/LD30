using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMX
{
    public class TmxMapData
    {
        public int width, height;
        public int tileheight, tilewidth;
        public int version;
        public string orientation;

        public TileSet[] tilesets;
        public Layer[] layers;
        public Dictionary<string, string> properties;

        public TmxMapData()
        {
            tilesets = new TileSet[0];
            layers = new Layer[0];
            properties = new Dictionary<string, string>();
        }
    }
}
