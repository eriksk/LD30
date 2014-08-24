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
            effects.Add("skid", content.Load<SoundEffect>(@"audio/skid").CreateInstance());
            effects["skid"].Volume = 0.5f;
            effects.Add("switch_positive", content.Load<SoundEffect>(@"audio/switch_positive").CreateInstance());
            effects.Add("menu_song", content.Load<SoundEffect>(@"audio/menu_song").CreateInstance());
            effects["menu_song"].Volume = 0.3f;
        }

        public void Play(string name)
        {
            effects[name].Stop(true);
            effects[name].Play();
        }

        public void SetPitch(string name, float pitch)
        {
            effects[name].Pitch = pitch;
        }

        public void PlayLooped(string name)
        {
            var i = effects[name];
            if (i.State == SoundState.Playing)
                return;

            i.IsLooped = true;
            i.Play();
        }

        public void Stop(string name) 
        {
            effects[name].Stop(true);
        }
    }
}
