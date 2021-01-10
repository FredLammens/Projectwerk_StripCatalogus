using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class GridRow
    {
        public ViewComic Comic { get; }

        public String Title { get; set; }
        public String Series { get; set; }
        public String SeriesNumber { get; set; }
        public String Authors { get; set; }
        public String Publishers { get; set; }

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
