using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class Mapper //extension method van maken 
    {
        public static Comic ViewComicMapper(ViewComic viewcomic) 
        {
            return new Comic(viewcomic.Title, viewcomic.Series, viewcomic.SeriesNumber, ViewAuthorMapper(viewcomic.Authors), viewcomic.Publisher);
        }

        private static List<Author> ViewAuthorMapper(List<ViewAuthor> viewAuthors) 
        {
            List<Author> authors = new List<Author>();
            foreach (ViewAuthor author in viewAuthors)
            {
                authors.Add(new Author(author.Name));
            }
            return authors;
        }
        private static List<Publisher> ViewPublisherMapper(List<ViewPublisher> viewPublishers) 
        {
            List<Publisher> publishers = new List<Publisher>();
            foreach (ViewPublisher publisher in viewPublishers)
            {
                publishers.Add(new Publisher(publisher.Name));
            }
            return publishers;
        }
    }
}
