using se.skoggy.utils.Metrics;
using se.skoggy.utils.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversations
{
    public class Conversation
    {
        TimerTrig conversationTrig;
        string[] sequence;
        int current;

        public Conversation(string[] sequence, float interval)
        {
            this.sequence = sequence;
            conversationTrig = new TimerTrig(interval);
            current = 0;
        }

        public string Text { get { return sequence[current]; } }

        public bool Done { get { return current == sequence.Length - 1; } }

        public void Next()
        {
            if (!Done)
                current++;
        }

        public void Update(float dt) 
        {
            if (conversationTrig.IsTrigged(dt) && !Done) 
            {
                current++;
            }
        }
    }
}
