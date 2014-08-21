using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening
{
    public class Tween : ITween
    {
        protected ITweenable subject;
        protected float current, duration;
        protected Interpolation interpolation;

        public Tween(ITweenable subject, Interpolation interpolation, float duration)
        {
            this.subject = subject;
            this.interpolation = interpolation;
            this.duration = duration;
        }

        public bool Done
        {
            get { return current >= duration; }
        }

        public float Progress
        {
            get { return current / duration > 1f ? 1f : current / duration; }
        }

        public virtual void Update(float dt)
        {
            current += dt;
            if (current > duration)
                current = duration;
        }
    }
}
