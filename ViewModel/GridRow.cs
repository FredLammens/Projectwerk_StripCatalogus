using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    /// <summary>
    /// class for custom gridRow
    /// </summary>
    public class GridRow
    {
        /// <summary>
        /// comic to display in gridrow
        /// </summary>
        public ViewComic Comic { get; }
        /// <summary>
        /// title to display in gridrow
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// series to display in gridrow
        /// </summary>
        public String Series { get; set; }
        /// <summary>
        /// seriesnr to display in gridrow
        /// </summary>
        public String SeriesNumber { get; set; }
        /// <summary>
        /// author to display in gridrow
        /// </summary>
        public String Authors { get; set; }
        /// <summary>
        /// publisher to display in gridrow
        /// </summary>
        public String Publishers { get; set; }
        /// <summary>
        /// Constructor to set members from with viewcomic
        /// </summary>
        /// <param name="viewComic">viewcomic to map to gridrow</param>
        public GridRow(ViewComic viewComic)
        {
            this.Comic = viewComic;

            Title = Comic.Title;
            Series = Comic.Series.Name;
            SeriesNumber = Comic.SeriesNumber.ToString();

            foreach (ViewAuthor author in viewComic.Authors)
            {
                Authors = Authors + author.Name + ", ";
            }

            Publishers = viewComic.Publisher.Name;
        }
    }
}
