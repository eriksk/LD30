using Microsoft.Xna.Framework;
using se.skoggy.utils.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Collision
{
    public interface ICollidable
    {
        Rectangle Bounds { get; }
        GameObject SetPosition(float x, float y);
    }
}
