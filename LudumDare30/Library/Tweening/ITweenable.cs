using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening
{
    public interface ITweenable
    {
        void SetPosition(float x, float y);
        void SetPositionX(float x);
        void SetPositionY(float y);
        void AddRotation(float rotationToAdd);
        void SetRotation(float rotation);
        void SetScale(float scalar);
        void SetScale(float x, float y);
        void SetAlpha(byte a);
        void SetColor(byte r, byte g, byte b, byte a);
    }
}
