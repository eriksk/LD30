using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Animations.TreeAnimations
{
    public class AnimationPart
    {
        public string name;
        public Vector2 position, scale, origin;
        public Color color;
        public Rectangle source;
        public float rotation;
        public SpriteEffects flip;
        private Vector2 originInPixels;
        public int sortIndex;

        public List<AnimationPart> children;
        public AnimationPart parent;
        public List<AnimationEvent> events;

        public AnimationPart()
        {
            sortIndex = 0;
            flip = SpriteEffects.None;
            parent = null;
            name = "";
            scale = Vector2.One;
            origin = Vector2.One * 0.5f;
            color = Color.White;
            children = new List<AnimationPart>();
            events = new List<AnimationEvent>();
        }

        public AnimationPart AddChild(AnimationPart child) 
        {
            child.parent = this;
            children.Add(child);
            return this;
        }

        public AnimationPart GetPartByName(string name)
        {
            if (this.name == name)
                return this;
            foreach (var child in children)
            {
                var childPart = child.GetPartByName(name);
                if (childPart != null)
                    return childPart;
            }
            return null;
        }

        public void AddEvent(AnimationEvent animationEvent)
        {
            if (animationEvent.time < 0f || animationEvent.time > 1f)
                throw new ArgumentOutOfRangeException("event time must be a unit value (> 0f && <= 1f");

            events.Add(animationEvent);
            SortEvents();
        }

        public void SortEvents()
        {
            for (int i = 0; i < events.Count; i++)
            {
                AnimationEvent ev = events[i];
                for (int j = i; j < events.Count; j++)
                {
                    if (i == j)
                        continue;

                    AnimationEvent nextEvent = events[j];
                    if (ev.time > nextEvent.time)
                    {
                        events[j] = ev;
                        events[i] = nextEvent;
                    }
                }
            }
        }

        #region Transform

        public Vector2 Position
        {
            get
            {
                if (parent == null)
                {
                    return position;
                }
                else
                {
                    return CalculatePositionFromParent();
                }
            }
        }

        public float Rotation
        {
            get
            {
                if (parent == null)
                    return rotation;

                return CalculateRotationFromParent();
            }
        }

        public void SetWorldRotation(float rotation)
        {
            this.rotation = rotation - parent.Rotation;
        }

        public Vector2 Scale
        {
            get
            {
                if (parent == null)
                    return scale;
                return CalculateScaleFromParent();
            }
        }

        private Vector2 CalculateScaleFromParent()
        {
            return parent.Scale * scale;
        }

        private float CalculateRotationFromParent()
        {
            return parent.Rotation + rotation;
        }

        private Vector2 CalculatePositionFromParent()
        {
            return GetPositionRelativeTo(parent.Position, parent.Position + position * parent.Scale, parent.Rotation);
        }

        public Vector2 GetPositionRelativeTo(Vector2 origin, Vector2 position, float rotation)
        {
            float s = (float)Math.Sin(rotation);
            float c = (float)Math.Cos(rotation);

            position.X -= origin.X;
            position.Y -= origin.Y;

            float xNew = position.X * c - position.Y * s;
            float yNew = position.X * s + position.Y * c;

            position.X = xNew + origin.X;
            position.Y = yNew + origin.Y;

            return position;
        }

        #endregion

        protected bool CanAnimate 
        {
            get { return events.Count > 0; }
        }

        private AnimationEvent GetNextEvent(int currentIndex, AnimationEvent c)
        {
            int i = currentIndex + 1;
            while (true) 
            {
                if (i > events.Count - 1)
                    i = 0;

                if (events[i].GetType() == c.GetType())
                    return events[i];
                i++;
            }
        }

        private AnimationEvent GetNextEvent(int currentIndex) 
        {
            if (events.Count < currentIndex + 2) 
            {
                return events[0];
            }
            return events[currentIndex + 1];
        }

        public void Update(float dt, float progress)
        {
            foreach (var child in children)
            {
                child.Update(dt, progress);
            }

            if (!CanAnimate)
                return;

            for (int i = 0; i < events.Count; i++)
            {
                var animationEvent = events[i];
                if (animationEvent.time < progress && (GetNextEvent(i, animationEvent).time > progress || GetNextEvent(i, animationEvent).time < animationEvent.time || events.Count == 1)) 
                {
                    animationEvent.Update(dt, this, progress, GetNextEvent(i, animationEvent));
                }                
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (var child in children)
            {
                if(child.sortIndex < sortIndex)
                    child.Draw(spriteBatch, texture);
            }

            originInPixels.X = source.Width * origin.X;
            originInPixels.Y = source.Height * origin.Y;

            spriteBatch.Draw(texture, Position, source, color, Rotation, originInPixels, Scale, flip, 0f);

            foreach (var child in children)
            {
                if (child.sortIndex >= sortIndex)
                    child.Draw(spriteBatch, texture);
            }
        }
    }
}
