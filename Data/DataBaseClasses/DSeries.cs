using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// This class represents a series inside the database.
    /// </summary>
    public class DSeries
    {
        #region Properties
        /// <summary>
        /// The identifier in the database for this object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the series.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// All comics belonging to the series.
        /// </summary>
        List<DComic> Comics { get; set; } = new List<DComic>();
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public DSeries()
        {

        }
        /// <summary>
        /// Makes a Series object.
        /// </summary>
        /// <param name="name">The name of the series.</param>
        public DSeries(string name)
        {
            Name = name;
        }
        #endregion

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is DSeries series &&
                   Name == series.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
        #endregion
    }
}
