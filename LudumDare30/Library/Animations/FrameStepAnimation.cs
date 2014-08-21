using se.skoggy.utils.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations
{
    public class FrameStepAnimation
    {
        private readonly int[] frames;
        private readonly TimerTrig trig;
        private int currentFrame;

        public FrameStepAnimation(int[] frames, float interval)
        {
            this.frames = frames;
            trig = new TimerTrig(interval);
        }

        public virtual int Frame
        {
            get { return frames[currentFrame]; }
        }

        public virtual void Update(float dt)
        {
            if (!trig.IsTrigged(dt))
                return;

            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;
        }
    }
}
