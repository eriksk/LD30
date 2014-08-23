﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.TMX
{
    public class MapObject
    {
        public float width, height;
        public float x, y;
        public string name;
        public float rotation;
        public bool visible;
        public string type;
        public Dictionary<string, string> properties;

        public MapObject()
        {
            properties = new Dictionary<string, string>();
        }

    }
}
