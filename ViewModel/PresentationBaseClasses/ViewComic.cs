﻿using Newtonsoft.Json;
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
        [JsonProperty("Titel")]
        public string Title { get; set; }
        /// <summary>
        /// The series the comic belongs to.
        /// </summary>
        [JsonProperty("Reeks")]
        public ViewSeries Series { get; set; }
        /// <summary>
        /// The number the comic is in the series.
        /// </summary>
        [JsonProperty("Nr")]
        public int? SeriesNumber { get; set; }
        /// <summary>
        /// The autor(s) that wrote this comic
        /// </summary>
        [JsonProperty("Auteurs")]
        public List<ViewAuthor> Authors { get; set; }
        /// <summary>
        /// The publisher(s) that published the comic.
        /// </summary>
        [JsonProperty("Uitgeverij")]
        public ViewPublisher Publisher { get; set; }
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
        [JsonConstructor]
        public ViewComic(string title, ViewSeries series, int? seriesNumber, List<ViewAuthor> authors, ViewPublisher publisher)
        {
            Title = title;
            Series = series;
            SeriesNumber = seriesNumber;
            if (DuplicateAuthors(authors))
                throw new PresentationException("Een strip kan niet twee keer dezelfde autheur hebben.");
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
                   EqualityComparer<ViewSeries>.Default.Equals(Series, comic.Series) &&
                   SeriesNumber == comic.SeriesNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Series, SeriesNumber);
        }

        #endregion

        #region ToString
        public override string ToString()
        {
            return Title;
        } 
        #endregion
    }
}
