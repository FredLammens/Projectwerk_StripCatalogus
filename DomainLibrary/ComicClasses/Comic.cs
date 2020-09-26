using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLibrary.ComicClasses
{
    /// <summary>
    /// This class represents a comic.
    /// </summary>
    public class Comic
    {
        #region Properties
        /// <summary>
        /// The title of the comic
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The series the comic belongs to.
        /// </summary>
        public string Series { get; set; }
        /// <summary>
        /// The number the comic is in the series.
        /// </summary>
        public int SeriesNumber { get; set; }
        /// <summary>
        /// The autor(s) that wrote this comic
        /// </summary>
        public List<Author> Authors { get; set; }
        /// <summary>
        /// The publisher(s) that published the comic.
        /// </summary>
        public List<Publisher> Publishers { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public Comic()
        {

        }

        /// <summary>
        /// A constuctor that makes a Comic object.
        /// </summary>
        /// <param name="title">The comics title.</param>
        /// <param name="series">The series the comic belongs to.</param>
        /// <param name="seriesNumber">The number the comic is in the series.</param>
        /// <param name="authors">The autor(s) that wrote this comic</param>
        /// <param name="publishers">The publisher(s) that published the comic.</param>
        public Comic(string title, string series, int seriesNumber, List<Author> authors, List<Publisher> publishers)
        {
            Title = title;
            Series = series;
            SeriesNumber = seriesNumber;
            if (DuplicateAuthors(authors))
                throw new DomainException("Een strip kan niet twee keer dezelfde autheur hebben.");
            Authors = authors;
            if (DuplicatePublishers(publishers))
                throw new DomainException("Een strip kan niet twee keer dezelfde uitgeverij hebben.");
            Publishers = publishers;


        }

        #endregion

        #region Functionality
        /// <summary>
        /// Check whether a given list of publishers has a duplicate.
        /// </summary>
        /// <param name="publishers">List of publishers to check</param>
        /// <returns></returns>
        private bool DuplicatePublishers(List<Publisher> publishers)
        {
            if (publishers.GroupBy(a => a.GetHashCode()).Any(g => g.Count() > 1))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check whether a given list of authors has a duplicate.
        /// </summary>
        /// <param name="authors">List of authors to check</param>
        /// <returns></returns>
        private bool DuplicateAuthors(List<Author> authors)
        {
            if (authors.GroupBy(a => a.GetHashCode()).Any(g => g.Count() > 1))
                return true;
            else
                return false;
        }
        #endregion
    }
}
