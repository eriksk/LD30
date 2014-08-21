using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Screens
{
    public interface IGameContext
    {
        GraphicsDevice GraphicsDevice { get; }
        IServiceProvider ServiceProvider { get; }
        string ContentRoot { get; }
        int Width { get; }
        int Height { get; }

        void ChangeScreen(IScreen screen);
        void Exit();
    }
}
