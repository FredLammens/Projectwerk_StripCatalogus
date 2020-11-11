﻿using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// A class to transform object to and from the DataClasses
    /// </summary>
    public class Mapper
    {
        #region toDComic
        /// <summary>
        /// Transforms a Comic object into a DComic object.
        /// </summary>
        /// <param name="comic">Comic to transform.</param>
        /// <returns>A DComic object.</returns>
        public static DComic ToDComic(Comic comic)
        {
            DComic toReturn = new DComic(comic.Title, ToDSeries(comic.Series), comic.SeriesNumber, ToDAuthors(comic.Authors), ToDPublisher(comic.Publisher));
            return toReturn;
        }

        /// <summary>
        /// Makes a new DSeries object from a given string.
        /// </summary>
        /// <param name="series">Name of the sereis</param>
        /// <returns>A new DSereis object</returns>
        public static DSeries ToDSeries(Series series)
        {
            return new DSeries(series.Name);
        }

        /// <summary>
        /// Transforms a Publisher object into a DPublisher object.
        /// </summary>
        /// <param name="publisher">Publisher to transform.</param>
        /// <returns>A list of of DPublisher objects.</returns>
        public static DPublisher ToDPublisher(Publisher publisher)
        {
            return new DPublisher(publisher.Name);
        }

        /// <summary>
        /// Transforms Author object into a DAuthor object.
        /// </summary>
        /// <param name="author">Author to transform.</param>
        /// <returns>An Author object.</returns>
        public static DAuthor ToDAuthor(Author author)
        {
            return new DAuthor(author.Name);
        }

        /// <summary>
        /// Transforms a list of Author objects into a list of DAuthor objects.
        /// </summary>
        /// <param name="authors">Authors to transform.</param>
        /// <returns>A list of of DAuthor objects.</returns>
        public static List<DAuthor> ToDAuthors(List<Author> authors)
        {
            List<DAuthor> toReturn = new List<DAuthor>();

            foreach (var author in authors)
            {
                toReturn.Add(new DAuthor(author.Name));
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
            Comic toReturn = new Comic(dComic.Title, ToSeries(dComic.Series), dComic.SeriesNumber, ToAuthors(dComic.Authors), ToPublisher(dComic.Publisher));
            return toReturn;
        }

        /// <summary>
        /// Transforms a list of DPublisher objects into a list of Publisher objects.
        /// </summary>
        /// <param name="dPublisher">DPublishers to transform.</param>
        /// <returns>A list of of Publisher objects.</returns>
        public static Publisher ToPublisher(DPublisher dPublisher)
        {
            return new Publisher(dPublisher.Name);
        }
        /// <summary>
        /// Transforms a list of DAuthor objects into a list of Author objects.
        /// </summary>
        /// <param name="dAuthors">DAuthors to transform.</param>
        /// <returns>A list of of Author objects.</returns>
        public static List<Author> ToAuthors(List<DAuthor> dAuthors)
        {
            List<Author> toReturn = new List<Author>();

            foreach (var dAuthor in dAuthors)
            {
                toReturn.Add(new Author(dAuthor.Name));
            }

            return toReturn;
        }

        /// <summary>
        /// Makes a new DSeries object from a given string.
        /// </summary>
        /// <param name="dSeries">Name of the sereis</param>
        /// <returns>A new DSereis object</returns>
        public static Series ToSeries(DSeries dSeries)
        {
            return new Series(dSeries.Name);
        }

        /// <summary>
        /// Transforms DAuthor objects into a Author object.
        /// </summary>
        /// <param name="dAuthor">Author to transform.</param>
        /// <returns>A Author object.</returns>
        public static Author ToAuthor(DAuthor dAuthor)
        {
            return new Author(dAuthor.Name);
        }

        #endregion
    }
}
