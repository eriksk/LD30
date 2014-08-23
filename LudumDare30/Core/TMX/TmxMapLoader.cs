using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TMX
{
    public class TmxMapLoader
    {
        ContentManager content;

        public TmxMapLoader(ContentManager content)
        {
            this.content = content;
        }

        public TmxMapData Load(string name) 
        {
            string path = string.Format("{0}/{1}.json", content.RootDirectory, name);
            var jsonContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TmxMapData>(jsonContent);
        }
    }
}
