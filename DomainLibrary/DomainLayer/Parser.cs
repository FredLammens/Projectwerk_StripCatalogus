using Newtonsoft.Json;
using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainLibrary.DomainLayer
{
   public class Parser
    {
        public static IEnumerable<Comic> DeSerializeComics(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<Comic>)serializer.Deserialize(file, typeof(List<Comic>));
            }
        }

        static public void SerializeComics(List<Comic> comics, string path)
        {
  
            path += @"/Strips.json";
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, comics);
            }
        }
    }
}
