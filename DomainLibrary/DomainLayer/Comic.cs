﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLibrary.DomainLayer

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
        [JsonProperty("Titel")]
        public string Title { get; set; }
        /// <summary>
        /// The series the comic belongs to.
        /// </summary>
        [JsonProperty("Reeks")]
        public Series Series { get; set; }
        /// <summary>
        /// The number the comic is in the series.
        /// </summary>
        [JsonProperty("Nr")]
        public int? SeriesNumber { get; set; }
        /// <summary>
        /// The autor(s) that wrote this comic
        /// </summary>
        [JsonProperty("Auteurs")]
        public List<Author> Authors { get; set; }
        /// <summary>
        /// The publisher that published the comic.
        /// </summary>
        [JsonProperty("Uitgeverij")]
        public Publisher Publisher { get; set; }
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
        /// <param name="publisher">The publisher that published the comic.</param>
        [JsonConstructor]
        public Comic(string title, Series series, int? seriesNumber, List<Author> authors,Publisher publisher)
        {
            Title = title;
            Series = series;
            SeriesNumber = seriesNumber;
            if (DuplicateAuthors(authors))
                throw new DomainException("Een strip kan niet twee keer dezelfde autheur hebben.");
            Authors = authors;
            Publisher = publisher;


        }

        #endregion

        #region Functionality

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

        #region Comparing
        public override bool Equals(object obj)
        {
            return obj is Comic comic &&
                   Title == comic.Title &&
                   EqualityComparer<Series>.Default.Equals(Series, comic.Series) &&
                   SeriesNumber == comic.SeriesNumber &&
                   EqualityComparer<List<Author>>.Default.Equals(Authors, comic.Authors) &&
                   EqualityComparer<Publisher>.Default.Equals(Publisher, comic.Publisher);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Series, SeriesNumber, Authors, Publisher);
        }
        #endregion
    }
}
