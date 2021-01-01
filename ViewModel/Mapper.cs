﻿using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            return new Comic(viewcomic.Title, new Series(viewcomic.Series.Name), viewcomic.SeriesNumber, ViewAuthorMapper(viewcomic.Authors), new Publisher(viewcomic.Publisher.Name));
        }
        /// <summary>
        /// Maps a comic object to a viewcomic object.
        /// </summary>
        /// <param name="comic"></param>
        /// <returns></returns>
        private static ViewComic ComicMapper(Comic comic) 
        {
            return new ViewComic(comic.Title, new ViewSeries(comic.Series.Name), comic.SeriesNumber, AuthorMapper(comic.Authors), new ViewPublisher(comic.Publisher.Name));
        }
        public static List<ViewComic> ComicsMapper(IEnumerable<Comic> comics) 
        {
            List<ViewComic> viewComics = new List<ViewComic>();
            foreach (Comic comic in comics)
            {
                viewComics.Add(ComicMapper(comic));
            }
            return viewComics;
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
        /// <summary>
        /// Maps a list of Authors objects to ViewAuthors objects.
        /// </summary>
        /// <param name="authors"></param>
        /// <returns></returns>
        public static List<ViewAuthor> AuthorMapper(IReadOnlyList<Author> authors) 
        {
            List<ViewAuthor> viewAuthors = new List<ViewAuthor>();
            foreach (Author author in authors)
            {
                viewAuthors.Add(new ViewAuthor(author.Name));
            }
            return viewAuthors;
        }
        /// <summary>
        /// Maps a list of Publisher objects to ViewPublisher objects.
        /// </summary>
        /// <param name="publishers"></param>
        /// <returns></returns>
        public static List<ViewPublisher> PublisherMapper(IReadOnlyList<Publisher> publishers)
        {
            List<ViewPublisher> viewPublishers = new List<ViewPublisher>();
            foreach (Publisher publisher in publishers)
            {
                viewPublishers.Add(new ViewPublisher(publisher.Name));
            }
            return viewPublishers;
        }

        /// <summary>
        /// Maps a KeyValuePair of <ViewComic, int> to a dictionary of <Comic, int>
        /// </summary>
        /// <param name="viewcomicDict"></param>
        /// <returns></returns>
        public static Dictionary<Comic, int> ComicDictMapper(ObservableCollection<KeyValuePair<ViewComic, int>> viewcomicDict)
        {
            Dictionary<Comic, int> comicDict = new Dictionary<Comic, int>();

            for (int i = 0; i < viewcomicDict.Count ; i++)
            {
                Comic comic = ViewComicMapper(viewcomicDict[i].Key);

                comicDict.Add(comic, viewcomicDict[i].Value);
            }

            return comicDict;
        }
    }
}
