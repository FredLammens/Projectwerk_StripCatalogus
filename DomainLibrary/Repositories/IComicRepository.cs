using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.Repositories
{
    /// <summary>
    /// A collection of comics in the database.
    /// </summary>
    public interface IComicRepository
    {
        #region Operations
        /// <summary>
        /// Adds a comic to the database.
        /// </summary>
        /// <param name="comic">Comic to add.</param>
        public void AddComic(Comic comic);
        /// <summary>
        /// Adds a collection of comics to the database.
        /// </summary>
        /// <param name="comics">Comics to add.</param>
        public void AddComics(IEnumerable<Comic> comics);
        /// <summary>
        /// Retrieves all comics from the database.
        /// </summary>
        /// <returns>A collection of comics.</returns>
        public IEnumerable<Comic> GetComics();
        /// <summary>
        /// Deletes a comic from the database.
        /// </summary>
        /// <param name="comic">Comic to remove.</param>
        public void RemoveComic(Comic comic);
        #endregion
    }
}
