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
        #region AddObject
        /// <summary>
        /// Adds a comic to the database.
        /// </summary>
        /// <param name="comic">Comic to add.</param>
        public void AddComic(Comic comic);
        /// <summary>
        /// Adds a author to the database.
        /// </summary>
        /// <param name="author">Author to add.</param>
        public void AddAuthor(Author author);
        /// <summary>
        /// Adds a publisher to the database.
        /// </summary>
        /// <param name="publisher">Publisher to add.</param>
        public void AddPublisher(Publisher publisher);
        /// <summary>
        /// Adds a series to the database.
        /// </summary>
        /// <param name="series">Series to add.</param>
        public void AddSeries(Series series);
        /// <summary>
        /// Adds a collection of comics to the database.
        /// </summary>
        /// <param name="comics">Comics to add.</param>
        public void AddComics(IEnumerable<Comic> comics);
        #endregion

        #region GetObject
        /// <summary>
        /// Retrieve all Series objects from the database
        /// </summary>
        /// <returns>All series in the database</returns>
        public IEnumerable<Series> GetAllSeries();
        /// <summary>
        /// Retrieve all Publishers objects from the database
        /// </summary>
        /// <returns>All publishers in the database</returns>
        public IEnumerable<Publisher> GetAllPublishers();
        /// <summary>
        /// Retrieve all Authors objects from the database
        /// </summary>
        /// <returns>All Author in the database</returns>
        public IEnumerable<Author> GetAllAuthors();
        /// <summary>
        /// Retrieves all comics from the database.
        /// </summary>
        /// <returns>A collection of comics.</returns>
        public IEnumerable<Comic> GetComics();
        #endregion

        #region RemoveObject

        /// <summary>
        /// Deletes a comic from the database.
        /// </summary>
        /// <param name="comic">Comic to remove.</param>
        public void RemoveComic(Comic comic);
        #endregion
    }
}
