using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// This class represents a publisher inside the database.
    /// </summary>
    public class DPublisher
    {
        #region Properties
        /// <summary>
        /// The identifier in the database for this object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the publisher.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public DPublisher()
        {

        }
        /// <summary>
        /// A constuctor that makes a Publisher object..
        /// </summary>
        /// <param name="name">The name of the publisher.</param>
        public DPublisher(string name)
        {
            Name = name;
        }
        #endregion
    }
}
