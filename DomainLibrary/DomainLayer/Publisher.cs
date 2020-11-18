//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// This class is a place that publishes comics.
    /// </summary>
    public class Publisher
    {
        #region Properties
        /// <summary>
        /// The name of the publisher.
        /// </summary>
       // [JsonProperty("Naam")]
        public string Name { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Publisher()
        {

        }

        /// <summary>
        /// A constuctor that makes a Publisher object..
        /// </summary>
        /// <param name="name">The name of the publisher.</param>
       // [JsonConstructor]
        public Publisher(string name)
        {
            Name = name;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is Publisher publisher &&
                   Name == publisher.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
