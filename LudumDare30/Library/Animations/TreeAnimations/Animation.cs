using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.TreeAnimations
{
    public class Animation
    {
        public string name;
        public AnimationPart root;
        public AnimationTimeLine timeline;

        public Animation()
        {
            timeline = new AnimationTimeLine();
        }

        /// <summary>
        /// Navigates the tree of parts and tries to get a part by matching name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AnimationPart GetPartByName(string name)
        {
            if (root == null)
                return null;

            if (root.name == name)
                return root;

            return root.GetPartByName(name);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            root.Draw(spriteBatch, texture);
        }

        public void Update(float dt, float progress)
        {
            root.Update(dt, progress);
        }
    }
}
