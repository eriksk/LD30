using Microsoft.Xna.Framework;
using se.skoggy.utils.GameObjects;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening.Stock
{
    public class PositionTween : Tween
    {
        Vector2 startPosition;
        Vector2 endPosition;

        public PositionTween(ITweenable subject, Interpolation interpolation, float duration, Vector2 startPosition, Vector2 endPosition)
            : base(subject, interpolation, duration)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            subject.SetPosition(
                    interpolation.Apply(startPosition.X, endPosition.X, Progress),
                    interpolation.Apply(startPosition.Y, endPosition.Y, Progress)
                );
        }
    }
}
