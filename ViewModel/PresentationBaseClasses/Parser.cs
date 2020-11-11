using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class Parser
    {
        public static IEnumerable<ViewComic> DeSerializeComics(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<ViewComic>)serializer.Deserialize(file, typeof(List<ViewComic>));
            }
        }

        public static void SerializeComics(List<ViewComic> comics, string path)
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
