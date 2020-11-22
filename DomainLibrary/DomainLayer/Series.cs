
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
        private string _name;
        /// <summary>
        /// The name of the series.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new DomainException("Naam mag niet leeg zijn.");
                else
                {
                    _name = value;
                }
            }
        }
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
