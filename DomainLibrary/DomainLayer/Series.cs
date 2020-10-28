using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// This class represents a series.
    /// </summary>
    public class Series
    {
        #region Properties
        /// <summary>
        /// The name of the series.
        /// </summary>
        [JsonProperty("Naam")]
        public string Name { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Series()
        {

        }
        /// <summary>
        /// Makes a Series object.
        /// </summary>
        /// <param name="name">The name of the series.</param>
        [JsonConstructor]
        public Series(string name)
        {
            Name = name;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is Series series &&
                   Name == series.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
