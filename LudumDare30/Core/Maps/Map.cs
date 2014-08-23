using Core.TMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Maps
{
    public class Map
    {
        TmxMapData tmxMap;

        public Map(TmxMapData tmxMap) 
        {
            this.tmxMap = tmxMap;
        }
    }
}
