using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening
{
    public class TweenManager
    {
        protected List<ITween> tweens;

        public TweenManager()
        {
            tweens = new List<ITween>();
        }

        public virtual void Clear()
        {
            tweens.Clear();
        }

        public virtual void Add(ITween tween)
        {
            tweens.Add(tween);
        }

        public virtual void Update(float dt) 
        {
            foreach (var tween in tweens)
            {
                tween.Update(dt);                
            }
            RemoveDoneTweens();
        }

        private void RemoveDoneTweens()
        {
            for (int i = 0; i < tweens.Count; i++)
            {
                if (tweens[i].Done)
                {
                    tweens.RemoveAt(i--);
                }
            }
        }
    }
}
