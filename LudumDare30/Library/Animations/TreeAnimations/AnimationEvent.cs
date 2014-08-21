using Microsoft.Xna.Framework;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.TreeAnimations
{
    public abstract class AnimationEvent
    {
        public Interpolation interpolation;
        public float time;

        public AnimationEvent(float time, Interpolation interpolation)
        {
            this.time = time;
            this.interpolation = interpolation;
        }

        protected float GetProgress(AnimationEvent nextEvent, float animationProgress)
        {
            if (nextEvent.time < time) 
                return (animationProgress - time) / (1f - time);
            
            return (animationProgress - time) / (nextEvent.time - time);
        }

        public abstract void Update(float dt, AnimationPart part, float animationProgress, AnimationEvent nextEvent);
    }
}
