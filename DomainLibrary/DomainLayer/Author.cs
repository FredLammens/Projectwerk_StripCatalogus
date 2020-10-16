using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// This class is a person who writes comics.
    /// </summary>
    public class Author
    {
        #region Properties
        /// <summary>
        /// The authors name.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Author()
        {

        }
        /// <summary>
        /// A constuctor that makes an Author object.
        /// </summary>
        /// <param name="name">The name of the author.</param>
        public Author(string name)
        {
           Name = name;
        }

        #endregion

        #region Comparing

        public override bool Equals(object obj)
        {
            return obj is Author author &&
                   Name == author.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
