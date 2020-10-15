using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// This class represents an author inside the database.
    /// </summary>
    class DAuthor
    {
        #region Properties
        /// <summary>
        /// The identifier in the database for this object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The authors name.
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public DAuthor()
        {

        }
        /// <summary>
        /// A constuctor that makes an Author object.
        /// </summary>
        /// <param name="lastName">The last name of the author.</param>
        public DAuthor(string name)
        {
            Name = name;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is DAuthor author &&
                   Name == author.Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
