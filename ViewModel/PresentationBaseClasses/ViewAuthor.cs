﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ViewModel.PresentationBaseClasses
{
    public class ViewAuthor : IComparable<ViewAuthor>
    {
        #region Properties
        /// <summary>
        /// The authors name
        /// </summary>
        [JsonProperty("Naam")]
        public string Name { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ViewAuthor()
        {

        }
        /// <summary>
        /// A constuctor that makes an Author object.
        /// </summary>
        /// <param name="name">The name of the author.</param>
        [JsonConstructor]
        public ViewAuthor(string name)
        {
            Name = name;
        }

        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is ViewAuthor author &&
                   Name == author.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        public int CompareTo([AllowNull] ViewAuthor other)
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
