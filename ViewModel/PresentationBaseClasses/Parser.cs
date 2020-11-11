using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class Parser
    {
        /// <summary>
        /// Reads file and deserializes comics to a list of viewcomics
        /// </summary>
        /// <param name="path">path of file to deserialize </param>
        /// <returns></returns>
        public static IEnumerable<ViewComic> DeSerializeComics(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (List<ViewComic>)serializer.Deserialize(file, typeof(List<ViewComic>));
            }
        }
        /// <summary>
        /// Serializes viewcomics and make JSON file in path.
        /// </summary>
        /// <param name="comics">List of viewcomics to serialize</param>
        /// <param name="path">path to save JSON file</param>
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
