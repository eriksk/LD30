using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleEmitterSettings
    {
        public string Name { get; set; }
        public BlendState blendState;
        public FloatRange frequency;
        public FloatRange lifeTime;
        public ColorRange startColor, endColor;
        public Vector2Range position;
        public Vector2Range velocity;
        public Vector2Range startScale, endScale;
        public Gravity gravity;
        public FloatRange rotation;
        public int spawnCount;
        public int[] sources;
        public bool loop;
        public int capacity;

        public ParticleEmitterSettings()
        {
            Name = "";
            loop = true;
            blendState = BlendState.AlphaBlend;
            frequency = new FloatRange();
            lifeTime = new FloatRange();
            position = new Vector2Range();
            velocity = new Vector2Range();
            startScale = new Vector2Range() { min = Vector2.One, max = Vector2.One };
            endScale = new Vector2Range() { min = Vector2.One, max = Vector2.One };
            rotation = new FloatRange();
            startColor = new ColorRange() { min = Color.White, max = Color.White };
            endColor = new ColorRange() { min = Color.White, max = Color.White };
            gravity = new Gravity();
            sources = new int[] { 0 };
            spawnCount = 1;
            capacity = 64;
        }

        public int RandomSource()
        {
            return sources[Rand.Next(sources.Length)];
        }

        public void Set(ParticleEmitterSettings settings)
        {
            this.blendState = settings.blendState;
            this.frequency.Set(settings.frequency);
            this.lifeTime.Set(settings.lifeTime);
            this.startColor.Set(settings.startColor);
            this.endColor.Set(settings.endColor);
            this.position.Set(settings.position);
            this.velocity.Set(settings.velocity);
            this.startScale.Set(settings.startScale);
            this.endScale.Set(settings.endScale);
            this.gravity.Set(settings.gravity);
            this.rotation.Set(settings.rotation);
            this.sources = settings.sources;
            this.loop = settings.loop;
            this.capacity = settings.capacity;
            this.Name = settings.Name;
        }
    }
}
