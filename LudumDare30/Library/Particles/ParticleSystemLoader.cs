using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Particles
{
    public class ParticleSystemLoader
    {
        ContentManager content;
        string effectsDirectoryName;

        public ParticleSystemLoader(ContentManager content, string effectsDirectoryName)
        {
            this.content = content;
            this.effectsDirectoryName = effectsDirectoryName;
        }

        public ParticleSystemSettings Load(string name) 
        {
            return LoadFromJson(File.ReadAllText(string.Format("{0}/{1}/{2}.json", content.RootDirectory, effectsDirectoryName, name)));
        }

        private ParticleSystemSettings LoadFromJson(string json) 
        {
            return JsonConvert.DeserializeObject<ParticleSystemSettings>(json, new ParticleSystemSettingsConverter());
        }
    }

    internal class ParticleSystemSettingsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BlendState);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            switch (reader.Value.ToString().ToLower())
            {
                case "additive": return BlendState.Additive;
                case "alphablend": return BlendState.AlphaBlend;
                case "nonpremultiplied": return BlendState.NonPremultiplied;
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as BlendState).Name.Replace("BlendState.", ""));
        }
    }
}
