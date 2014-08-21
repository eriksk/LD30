using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Tweening
{
    public interface ITween
    {
        bool Done { get; }
        void Update(float dt);
    }
}
