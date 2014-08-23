using Core.Screens;
using se.skoggy.utils.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bootstrap
{
    public class DefaultScreenSelector
    {
        IGameContext context;

        public DefaultScreenSelector(IGameContext context)
        {
            this.context = context;
        }

        public IScreen CreateDefaultScreen() 
        {
            return new CharacterTestScreen(context);
        }
    }
}
