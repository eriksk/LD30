using Microsoft.Xna.Framework;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.TreeAnimations.Events
{
    public class PositionEvent : AnimationEvent
    {
        Vector2 position;

        public PositionEvent(float time, Vector2 position, Interpolation interpolation)
            :base(time, interpolation)
        {
            this.position = position;
        }

        public override void Update(float dt, AnimationPart part, float animationProgress, AnimationEvent nextEvent)
        {
            float progress = GetProgress(nextEvent, animationProgress);
            part.position.X = interpolation.Apply(position.X, (nextEvent as PositionEvent).position.X, progress);
            part.position.Y = interpolation.Apply(position.Y, (nextEvent as PositionEvent).position.Y, progress);
        }
    }
}
