using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using se.skoggy.utils.Cameras;
using se.skoggy.utils.Metrics;
using se.skoggy.utils.Resolutions;
using se.skoggy.utils.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Screens
{
    public class BaseScreen : IScreen
    {
        protected string name;
        protected int width, height;

        private TimerTrig transitionTimer;
        protected IGameContext context;
        protected ContentManager content;
        protected SpriteBatch spriteBatch;
        protected Camera cam;
        private ScreenState state;

        protected TweenManager tweenManager;

        public BaseScreen(IGameContext context, string name, int width, int height)
        {
            this.context = context;
            this.name = name;
            this.width = width;
            this.height = height;
            transitionTimer = new TimerTrig(0);
            TransitionDuration = 1000f;
            spriteBatch = new SpriteBatch(context.GraphicsDevice);
            content = new ContentManager(context.ServiceProvider, context.ContentRoot);
            State = ScreenState.Initializing;
            tweenManager = new TweenManager();
            cam = new Camera(new Vector2(width / 2, height / 2));
        }

        protected float TransitionDuration
        {
            get { return transitionTimer.Interval; }
            set { transitionTimer.Set(value); }
        }

        public string Name { get { return name; } }

        public ScreenState State
        {
            get { return state; }
            private set 
            {
                state = value;
                transitionTimer.Reset();
                StateChanged();
            }
        }

        protected bool TransitioningIn
        {
            get { return state == ScreenState.TransitioningIn; }
        }

        protected bool TransitioningOut
        {
            get { return state == ScreenState.TransitioningOut; }
        }

        protected bool Running
        {
            get { return state == ScreenState.Running; }
        }
        
        protected bool Done
        {
            get { return state == ScreenState.Done; }
        }

        protected void Tween(ITween tween) 
        {
            tweenManager.Add(tween);
        }

        protected Vector2 Top { get { return new Vector2(0, -height / 2); } }
        protected Vector2 Left { get { return new Vector2(-width / 2, 0); } }
        protected Vector2 Right { get { return new Vector2(width / 2, 0); } }
        protected Vector2 Bottom { get { return new Vector2(0, height / 2); } }
        protected Vector2 Center { get { return new Vector2(0, 0); } }

        public virtual void StateChanged() 
        {
        }

        public virtual void Load()
        {
            State = ScreenState.TransitioningIn;
        }

        protected virtual void TransitionOut() 
        {
            State = ScreenState.TransitioningOut;
        }

        public virtual void Update(float dt)
        {
            tweenManager.Update(dt);
            transitionTimer.Update(dt);

            switch (state)
            {
                case ScreenState.TransitioningIn:
                    if (transitionTimer.IsTrigged()) 
                    {
                        State = ScreenState.Running;
                    }
                    break;
                case ScreenState.Running:
                    break;
                case ScreenState.TransitioningOut:
                    if (transitionTimer.IsTrigged())
                    {
                        State = ScreenState.Done;
                    }
                    break;
                case ScreenState.Done:
                    break;
            }

            cam.Update(dt);
        }

        public virtual void Draw()
        {
        }
    }
}
