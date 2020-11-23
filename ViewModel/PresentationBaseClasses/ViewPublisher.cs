using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class ViewPublisher : IComparable<ViewPublisher>
    {
        #region Properties
        /// <summary>
        /// The name of the publisher.
        /// </summary>
        [JsonProperty("Naam")]
        public string Name { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ViewPublisher()
        {

        }

        /// <summary>
        /// A constuctor that makes a Publisher object..
        /// </summary>
        /// <param name="name">The name of the publisher.</param>
        [JsonConstructor]
        public ViewPublisher(string name)
        {
            Name = name;
        }

        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is ViewPublisher publisher &&
                   Name == publisher.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public int CompareTo([AllowNull] ViewPublisher other)
        {
            return this.Name.CompareTo(other.Name);
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
