using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.GameObjects
{
    public class PoolableGameObject
    {
        public Vector2 position, scale, velocity;
        public float rotation;
        public int source;

        public PoolableGameObject()
        {
            scale = new Vector2(1f, 1f);
        }
    }
}
