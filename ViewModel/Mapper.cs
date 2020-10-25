using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class Mapper
    {
        /// <summary>
        /// Maps a viewcomic object to a comic object. So that it can be transfered to the domainLayer.
        /// </summary>
        /// <param name="viewcomic">viewcomic object</param>
        /// <returns></returns>
        public static Comic ViewComicMapper(ViewComic viewcomic) 
        {
            return new Comic(viewcomic.Title, viewcomic.Series, viewcomic.SeriesNumber, ViewAuthorMapper(viewcomic.Authors), new Publisher(viewcomic.Publisher.Name));
        }
        /// <summary>
        /// Maps a list of ViewAuthors objects to Authors objects. So that it can be transfered to the domainLayer.
        /// </summary>
        /// <param name="viewAuthors"></param>
        /// <returns></returns>
        private static List<Author> ViewAuthorMapper(List<ViewAuthor> viewAuthors) 
        {
            List<Author> authors = new List<Author>();
            foreach (ViewAuthor author in viewAuthors)
            {
                authors.Add(new Author(author.Name));
            }
            return authors;
        }
    }
}
