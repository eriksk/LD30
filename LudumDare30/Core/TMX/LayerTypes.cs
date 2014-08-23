using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMX
{
    public enum LayerType 
    {
        TileLayer,
        ObjectLayer
    }

    public class LayerTypes
    {
        public static LayerType Parse(string type) 
        {
            if (type == "tilelayer")
                return LayerType.TileLayer;
            if (type == "objectgroup")
                return LayerType.ObjectLayer;

            throw new NotImplementedException("Type " + type + " does not exist");
        }
    }
}
