using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// A class to transform object to and from the DataClasses
    /// </summary>
    class Mapper
    {
        #region toDComic
        /// <summary>
        /// Transforms a Comic object into a DComic object.
        /// </summary>
        /// <param name="comic">Comic to transform.</param>
        /// <returns>A DComic object.</returns>
        public static DComic ToDComic(Comic comic)
        {
            DComic toReturn = new DComic(comic.Title, comic.Series, comic.SeriesNumber, ToDAuthors(comic.Authors), ToDPublishers(comic.Publishers));
            return toReturn;
        }
        /// <summary>
        /// Transforms a list of Publisher objects into a list of DPublisher objects.
        /// </summary>
        /// <param name="publishers">Publishers to transform.</param>
        /// <returns>A list of of DPublisher objects.</returns>
        private static List<DPublisher> ToDPublishers(List<Publisher> publishers)
        {
            List<DPublisher> toReturn = new List<DPublisher>();

            foreach (var publisher in publishers)
            {
                toReturn.Add(new DPublisher(publisher.Name));
            }

            return toReturn;
        }
        /// <summary>
        /// Transforms a list of Author objects into a list of DAuthor objects.
        /// </summary>
        /// <param name="authors">Authors to transform.</param>
        /// <returns>A list of of DAuthor objects.</returns>
        private static List<DAuthor> ToDAuthors(List<Author> authors)
        {
            List<DAuthor> toReturn = new List<DAuthor>();

            foreach(var author in authors)
            {
                toReturn.Add(new DAuthor(author.FirstName, author.LastName));
            }

            return toReturn;
        }
        #endregion


        #region toComic
        /// <summary>
        /// Transforms a DComic object into a Comic object.
        /// </summary>
        /// <param name="dComic">DComic to transform.</param>
        /// <returns>A Comic object.</returns>
        public static Comic ToComic(DComic dComic)
        {
            Comic toReturn = new Comic(dComic.Title, dComic.Series, dComic.SeriesNumber, ToAuthors(dComic.Authors), ToPublishers(dComic.Publishers));
            return toReturn;
        }
        /// <summary>
        /// Transforms a list of DPublisher objects into a list of Publisher objects.
        /// </summary>
        /// <param name="dPublishers">DPublishers to transform.</param>
        /// <returns>A list of of Publisher objects.</returns>
        private static List<Publisher> ToPublishers(List<DPublisher> dPublishers)
        {
            List<Publisher> toReturn = new List<Publisher>();

            foreach (var dPublisher in dPublishers)
            {
                toReturn.Add(new Publisher(dPublisher.Name));
            }

            return toReturn;
        }
        /// <summary>
        /// Transforms a list of DAuthor objects into a list of Author objects.
        /// </summary>
        /// <param name="dAuthors">DAuthors to transform.</param>
        /// <returns>A list of of Author objects.</returns>
        private static List<Author> ToAuthors(List<DAuthor> dAuthors)
        {
            List<Author> toReturn = new List<Author>();

            foreach (var dAuthor in dAuthors)
            {
                toReturn.Add(new Author(dAuthor.FirstName, dAuthor.LastName));
            }

            return toReturn;
        }

        #endregion
    }
}
