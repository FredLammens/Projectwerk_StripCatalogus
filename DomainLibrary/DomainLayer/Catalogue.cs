﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<Comic> _comics = new List<Comic>();
        public IReadOnlyList<Comic> Comics { get => _comics.AsReadOnly(); }
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
        public Catalogue(List<Comic> comics)
        {
            SetComics(comics);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds a comic to the Catalogue
        /// </summary>
        /// <param name="comic">comic to add</param>
        /// <returns>false if already in catalogue </returns>
        public bool AddComic(Comic comic)
        {
            if (_comics.Contains(comic)) 
                return false;
             _comics.Add(comic);
            return true;
        }
        /// <summary>
        /// Removes a comic of the catalogue
        /// </summary>
        /// <param name="comic">comic object to remove</param>
        /// /// <returns>false if not in catalogue </returns>
        public bool RemoveComic(Comic comic)
        {
            if (!_comics.Contains(comic))
                return false;
            _comics.Remove(comic);
            return true;
        }
        /// <summary>
        /// Updates a comic of the catalogue
        /// </summary>
        /// <param name="index">index of comic to update</param>
        /// <param name="comic">comic object to update</param>
        public void UpdateComic(Comic oldComic, Comic updatedComic)
        {
            int index = _comics.FindIndex(x => x.GetHashCode() == oldComic.GetHashCode());
            if (!_comics.Any(x => x.GetHashCode() == updatedComic.GetHashCode()))
                _comics[index] = updatedComic;
            else 
            {
                throw new DomainException("De geupdate comic zit al in de catalogus");
            }

        }
        /// <summary>
        /// Sets comic if comics has no duplicates
        /// </summary>
        /// <param name="comics">list of comics to set</param>
        public void SetComics(List<Comic> comics)
        {
            if (DuplicateComics(comics))
                throw new DomainException("Een strip kan niet twee keer voorkomen.");
            _comics = comics;
        }
        /// <summary>
        /// Check whether a given list of comics has a duplicate.
        /// </summary>
        /// <param name="comics">List of comics to check</param>
        /// <returns></returns>
        private bool DuplicateComics(List<Comic> comics)
        {
            if (comics.GroupBy(a => a.GetHashCode()).Any(g => g.Count() > 1))
                return true;
            else
                return false;
        }
        #endregion
    }
}
