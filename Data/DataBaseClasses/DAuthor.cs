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
        /// The authors first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The authors last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The comics written by the author.
        /// </summary>
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
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        public DAuthor(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is DAuthor author &&
                   FirstName == author.FirstName &&
                   LastName == author.LastName;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
        #endregion
    }
}
