using se.skoggy.utils.Interpolations;
using se.skoggy.utils.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tweening.Stock
{
    public class AlphaFadeTween : Tween
    {
        byte start, end;

        public AlphaFadeTween(ITweenable subject, Interpolation interpolation, float duration, byte start, byte end)
            : base(subject, interpolation, duration)
        {
            this.start = start;
            this.end = end;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            subject.SetAlpha((byte)interpolation.Apply(start, end, Progress));
        }
    }
}
