﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// This class represents a comic inside the database.
    /// </summary>
    public class DComic
    {
        #region Properties
        /// <summary>
        /// The identifier in the database for this object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The title of the comic
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The series the comic belongs to.
        /// </summary>
        public DSeries Series { get; set; }
        /// <summary>
        /// The number the comic is in the series.
        /// </summary>
        public int? SeriesNumber { get; set; }
        /// <summary>
        /// The autor(s) that wrote this comic
        /// </summary>
        public List<DAuthor> Authors { get; set; }
        /// <summary>
        /// The publisher that published the comic.
        /// </summary>
        public DPublisher Publisher { get; set; }
        /// <summary>
        /// amount of this comic available
        /// </summary>
        public int AmountAvailable { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// An empty constructor.
        /// </summary>
        public DComic()
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
        public DComic(string title, DSeries series, int? seriesNumber, List<DAuthor> authors, DPublisher publisher, int amountAvailable)
        {
            Title = title;
            Series = series;
            SeriesNumber = seriesNumber;
            Authors = authors;
            Publisher = publisher;
        }

        #endregion
    }
}
