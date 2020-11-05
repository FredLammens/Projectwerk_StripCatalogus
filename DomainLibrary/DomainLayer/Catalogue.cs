using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// A class that represents a collection of comic objects.
    /// </summary>
    public class Catalogue
    {
        #region Properties
        /// <summary>
        /// A collection of Comic objects.
        /// </summary>
        public HashSet<Comic> Comics { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor
        /// </summary>
        public Catalogue()
        {

        }

        /// <summary>
        /// A constuctor that makes a Comic object.
        /// </summary>
        /// <param name="comics">The comic(s) in the catalogue.</param>
        public Catalogue(HashSet<Comic> comics)
        {
            Comics = comics;
        }
        #endregion
        #region Methods
        public void AddComic(Comic comic) 
        {
            Comics.Add(comic);
        }
        #endregion

    }
}
