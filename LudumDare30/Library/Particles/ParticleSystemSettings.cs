using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleSystemSettings
    {
        public string Name { get; set; }
        public ParticleEmitterSettings[] emitters;

        public ParticleSystemSettings(ParticleEmitterSettings[] emitterSettings)
        {
            Name = "";
            this.emitters = emitterSettings;
        }
    }
}
