using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.TreeAnimations.Events
{
    public class RotationEvent : AnimationEvent
    {
        public float rotation;

        public RotationEvent(float time, float rotation, Interpolation interpolation)
            : base(time, interpolation)
        {
            this.rotation = rotation;
        }

        public override void Update(float dt, AnimationPart part, float animationProgress, AnimationEvent nextEvent)
        {
            float progress = GetProgress(nextEvent, animationProgress);
            part.rotation = interpolation.Apply(rotation, (nextEvent as RotationEvent).rotation, progress);
        } 
    }
}
