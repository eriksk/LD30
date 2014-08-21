using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Ranges
{
    public class Gravity
    {
        public Vector2 force;

        public Gravity()
        {
            force = new Vector2();
        }

        public void Set(Gravity gravity)
        {
            this.force = gravity.force;
        }
    }
}
