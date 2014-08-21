using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Metrics
{
    public class TimerTrig
    {
        private float interval;
        private float current;

        public TimerTrig(float interval)
        {
            this.interval = interval;
            current = 0f;
        }

        public float Interval { get { return interval; } }

        public void Reset()
        {
            current = 0f;
        }

        public void Set(float interval) 
        {
            this.interval = interval;
            Reset();
        }

        public void Update(float timePassed)
        {
            current += timePassed;
        }

        public bool IsTrigged()
        {
            return !(current < interval);
        }

        public bool IsTrigged(float timePassed)
        {
            current += timePassed;
            if (current < interval)
                return false;

            current -= interval;
            return true;
        }
    }
}
