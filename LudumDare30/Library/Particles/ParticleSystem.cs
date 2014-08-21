using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleSystem
    {
        ParticleSystemSettings settings;
        List<ParticleEmitter> emitters;
        
        public Vector2 position;

        public ParticleSystem(ParticleSystemSettings settings)
        {
            position = Vector2.Zero;
            Initialize(settings);
        }

        public void Initialize(ParticleSystemSettings settings)
        {
            emitters = new List<ParticleEmitter>();
            foreach (var emitterSettings in settings.emitters)
            {
                emitters.Add(new ParticleEmitter(emitterSettings));
            }
        }

        public void Reset()
        {
            foreach (var emitter in emitters)
            {
                emitter.Reset();
            }
        }

        public void Stop()
        {
            foreach (var emitter in emitters)
            {
                emitter.Stop();
            }
        }

        public void Play()
        {
            foreach (var emitter in emitters)
            {
                emitter.Play();
            }
        }

        public void ResetAndRecreate()
        {
            foreach (var emitter in emitters)
            {
                emitter.ResetAndRecreate();
            }
        }
        
        public void Update(float dt)
        {
            foreach (var emitter in emitters)
            {
                emitter.Update(dt);                
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera cam, Rectangle[] sources, GameObject template)
        {
            foreach (var emitter in emitters)
            {
                emitter.Draw(cam, spriteBatch, position, sources, template);
            }
        }

    }
}
