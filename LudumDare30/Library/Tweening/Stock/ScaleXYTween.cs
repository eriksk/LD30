using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening.Stock
{
    public class ScaleXYTween : Tween
    {
        float startScale;
        float endScale;

        public ScaleXYTween(ITweenable subject, Interpolation interpolation, float duration, float startScale, float endScale)
            :base(subject, interpolation, duration)
        {
            this.startScale = startScale;
            this.endScale = endScale;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            subject.SetScale(interpolation.Apply(startScale, endScale, Progress));
        }
    }
}
