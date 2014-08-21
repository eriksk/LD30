using Microsoft.Xna.Framework;
using se.skoggy.utils.Interpolations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Cameras
{
    public class Camera
    {
        protected Vector2 position, center, target;
        protected Interpolation movementInterpolation;
        protected float rotation, targetRotation;
        protected float scale, targetScale;

        public Camera(Vector2 center)
        {
            this.center = center;
            target = new Vector2();
            position = new Vector2();
            rotation = 0f;
            scale = 1f;
            targetScale = 1f;
            movementInterpolation = Interpolation.Linear;
        }

        public void SetSize(int Width, int Height)
        {
            center.X = Width / 2;
            center.Y = Height / 2;
        }

        public virtual Matrix Projection 
        {
            get 
            {
                return Matrix.CreateScale(scale) *
                       Matrix.CreateRotationZ(rotation) * 
                       Matrix.CreateTranslation(position.X, position.Y, 0f) * 
                       Matrix.CreateTranslation(center.X, center.Y, 0f);
            }
        }

        public Vector2 ScreenToWorld(Vector2 screen)
        {
            return Vector2.Transform(screen, Matrix.Invert(Projection));
        }

        public Vector2 WorldToScreen(Vector2 screen) 
        {
            return new Vector2();
        }

        public void SetPosition(float x, float y) 
        {
            position.X = x;
            position.Y = y;
            Move(x, y);
        }

        public void Move(float x, float y)
        {
            target.X = x;
            target.Y = y;
        }

        public void SetRotation(float rotation) 
        {
            this.rotation = rotation;
            Rotate(rotation);
        }

        public void Rotate(float rotation)
        {
            this.targetRotation = rotation;
        }

        public void SetZoom(float zoom) 
        {
            this.scale = zoom;
            targetScale = zoom;
        }

        public void Zoom(float zoom)
        {
            targetScale += zoom;
        }

        public virtual void Update(float dt)
        {
            float speed = 0.05f;
            
            position.X = movementInterpolation.Apply(position.X, target.X, speed);
            position.Y = movementInterpolation.Apply(position.Y, target.Y, speed);

            rotation = movementInterpolation.Apply(rotation, targetRotation, speed);
            scale = movementInterpolation.Apply(scale, targetScale, speed);
        }
    }
}
