using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    /// <summary>
    /// This class represents a comic for the presentation layer.
    /// </summary>
    public class ViewComic
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
        public List<ViewAuthor> Authors { get; set; }
        /// <summary>
        /// The publisher(s) that published the comic.
        /// </summary>
        public List<ViewPublisher> Publishers { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public ViewComic()
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
        public ViewComic(string title, string series, int seriesNumber, List<ViewAuthor> authors, List<ViewPublisher> publishers)
        {
            Title = title;
            Series = series;
            SeriesNumber = seriesNumber;
            if (DuplicateAuthors(authors))
                throw new PresentationException("Een strip kan niet twee keer dezelfde autheur hebben.");
            Authors = authors;
            if (DuplicatePublishers(publishers))
                throw new PresentationException("Een strip kan niet twee keer dezelfde uitgeverij hebben.");
            Publishers = publishers;

        }

        #endregion

        #region Functionality
        /// <summary>
        /// Check whether a given list of publishers has a duplicate.
        /// </summary>
        /// <param name="publishers">List of publishers to check</param>
        /// <returns></returns>
        private bool DuplicatePublishers(List<ViewPublisher> publishers)
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
        private bool DuplicateAuthors(List<ViewAuthor> authors)
        {
            if (authors.GroupBy(a => a.GetHashCode()).Any(g => g.Count() > 1))
                return true;
            else
                return false;
        }


        #endregion

        #region Comparing

        public override bool Equals(object obj)
        {
            return obj is ViewComic comic &&
                   Title == comic.Title &&
                   Series == comic.Series &&
                   SeriesNumber == comic.SeriesNumber &&
                   EqualityComparer<List<ViewAuthor>>.Default.Equals(Authors, comic.Authors) &&
                   EqualityComparer<List<ViewPublisher>>.Default.Equals(Publishers, comic.Publishers);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Series, SeriesNumber, Authors, Publishers);
        }
        #endregion
    }
}
