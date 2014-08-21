using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Screens
{
    public interface IScreen
    {
        string Name { get; }
        void Load();
        void Update(float dt);
        void Draw();
    }
}
