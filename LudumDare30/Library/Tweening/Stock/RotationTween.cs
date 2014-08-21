using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening.Stock
{
    public class RotationTween: Tween
    {
        float startRotation;
        float endRotation;
        private UI.Text text;
        private float TransitionDuration;
        private Microsoft.Xna.Framework.Vector2 Left;
        private Microsoft.Xna.Framework.Vector2 Center;

        public RotationTween(ITweenable subject, Interpolation interpolation, float duration, float startRotation, float endRotation)
            : base(subject, interpolation, duration)
        {
            this.startRotation = startRotation;
            this.endRotation = endRotation;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            subject.SetRotation(interpolation.Apply(startRotation, endRotation, Progress));
        }
    }
}
