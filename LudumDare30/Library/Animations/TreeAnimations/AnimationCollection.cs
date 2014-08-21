using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.TreeAnimations
{
    public class AnimationCollection
    {
        public string name;
        protected List<Animation> animations;

        public AnimationCollection()
        {
            name = "new animation";
            animations = new List<Animation>();
        }
    }
}
