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
        string mapsDirectoryName;

        public TmxMapLoader(ContentManager content, string mapsDirectoryName)
        {
            this.content = content;
            this.mapsDirectoryName = mapsDirectoryName;
        }

        public TmxMapData Load(string name) 
        {
            string path = string.Format("{0}/{1}/{2}.json", content.RootDirectory, mapsDirectoryName, name);
            var jsonContent = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TmxMapData>(jsonContent);
        }
    }
}
