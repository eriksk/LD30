using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Audio
{
    public class Audio
    {
        private static Audio instance;

        public static Audio I
        {
            get 
            {
                if (instance == null)
                    instance = new Audio();
                return instance;
            }

        }

        private Audio()
        {

        }

        private Dictionary<string, SoundEffectInstance> effects;

        public void Load(ContentManager content) 
        {
            effects = new Dictionary<string, SoundEffectInstance>();
            effects.Add("engine", content.Load<SoundEffect>(@"audio/engine").CreateInstance());
            effects.Add("explosion", content.Load<SoundEffect>(@"audio/explosion").CreateInstance());
            effects.Add("switch_positive", content.Load<SoundEffect>(@"audio/switch_positive").CreateInstance());
        }

        public void Play(string name)
        {
            effects[name].Stop(true);
            effects[name].Play();
        }

        public void PlayLooped(string name)
        {
            var i = effects[name];
            i.IsLooped = true;
            i.Play();
        }

        public void Stop(string name) 
        {
            effects[name].Stop(true);
        }
    }
}
