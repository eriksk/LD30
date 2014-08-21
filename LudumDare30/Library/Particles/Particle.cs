using Microsoft.Xna.Framework;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class Particle : PoolableGameObject
    {
        public float current, duration;
        public Color color;
        public Color startColor, endColor;
        public Vector2 startScale, endScale;

        public Particle()
        {
            color = Color.White;
            startScale = Vector2.Zero;
            endScale = Vector2.Zero;
        }
    }
}
