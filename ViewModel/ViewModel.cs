using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary;
using DomainLibrary.DomainLayer;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        #region propertyHandler
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        /// <summary>
        /// variable that holds all comics with getter and setter => setter raises property changed when object changes.
        /// </summary>
        private ObservableCollection<ViewComic> _comics;
        /// <summary>
        /// getter and setter with RaisePropertyChanged event called when object changes.
        /// </summary>
        public ObservableCollection<ViewComic> Comics 
        {
            get { return _comics; }
            set {
                _comics = value;
                RaisePropertyChanged("Comics");
            }
        }
        /// <summary>
        /// holds all comics , used for searching through the list.
        /// </summary>
        private List<ViewComic> allComics;
        /// <summary>
        /// DataBindinded variable for Comic-Title
        /// </summary>
        public string InputTitle { get; set; }
        /// <summary>
        /// DataBindinded variable for Comic-Series
        /// </summary>
        public string InputSeries { get; set; }
        /// <summary>
        /// DataBindinded variable for Comic-Series-Nr
        /// </summary>
        public string InputSeriesNr { get; set; }
        /// <summary>
        /// holds the parsedinput of the SeriesNr
        /// </summary>
        private int seriesNr;
        /// <summary>
        /// DataBindinded variable for Comic-Authors
        /// </summary>
        public string InputAuthor { get; set; }
        /// <summary>
        /// DataBindinded variable for Comic-Publisher
        /// </summary>
        public string InputPublisher { get; set; }

        /// <summary>
        /// domainLayer controller object
        /// </summary>
        readonly Controller controller;
        /// <summary>
        /// Constructor for initializing allcomics and comics.
        /// </summary>
        public async Task InitializeComicsAsync()
        {
            //allcomics initialized 
            allComics = await Task.Run(() => Mapper.ComicsMapper(controller.GetCatalogue().Comics));
            //initalize comics with allcomics
            Comics = new ObservableCollection<ViewComic>(allComics);
        }
        /// <summary>
        /// DataBindinded method for changing the comicslist to a comicslist where the title matches. 
        /// </summary>
        /// <param name="title">title to match</param>
        public void SearchTitle(string title) 
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Title.Contains(title)));
        }
        /// <summary>
        ///  DataBindinded method for changing the comicslist to a comicslist where the series matches. 
        /// </summary>
        /// <param name="series">series to match</param>
        public void SearchSeries(string series)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Series.Contains(series)));
        }
        /// <summary>
        ///  DataBindinded method for changing the comicslist to a comicslist where the author matches. 
        /// </summary>
        /// <param name="authorName">authorname to match</param>
        public void SearchAuthors(string authorName)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Authors.Any(a => a.Name.Contains(authorName))));
        }
        /// <summary>
        ///  DataBindinded method for changing the comicslist to a comicslist where the publisher matches. 
        /// </summary>
        /// <param name="publisherName">publishername to match</param>
        public void SearchPublisher(string publisherName)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Publisher.Name.Contains(publisherName)));
        }
        /// <summary>
        ///  DataBindinded method for adding a comic.
        /// </summary>
        /// <returns></returns>
        public async Task AddComic() 
        {
            if (String.IsNullOrEmpty(InputAuthor) || String.IsNullOrEmpty(InputTitle) || String.IsNullOrEmpty(InputPublisher) || String.IsNullOrEmpty(InputSeries) ||
                 int.TryParse(InputSeriesNr, out seriesNr))
                throw new PresentationException("Pls fill everything in.");
            ViewComic comic = new ViewComic(InputTitle, InputSeries, seriesNr, AuthorSplitter(InputAuthor), new ViewPublisher(InputPublisher));
            await Task.Run(() => controller.AddComic(Mapper.ViewComicMapper(comic)));
        }
        /// <summary>
        /// Takes the authorInputString and returns a list of ViewAuthors
        /// </summary>
        /// <param name="authors">InputString from authors</param>
        /// <param name="delimeter"> the sign for seperating authors</param>
        /// <returns></returns>
        private List<ViewAuthor> AuthorSplitter(string authors, string delimeter = ",") 
        {
            string[] splittedAuthors = authors.Split(delimeter);
            List<ViewAuthor> viewAuthors = new List<ViewAuthor>();
            foreach (string authorName in splittedAuthors)
            {
                viewAuthors.Add(new ViewAuthor(authorName));
            }
            return viewAuthors;
        }
        /// <summary>
        /// Imports the compics via the filepath
        /// </summary>
        /// <param name="comicsFilePath"> path of the comicsFile</param>
        public void ImportComic(string comicsFilePath) 
        {
            controller.ImportComics(comicsFilePath);
        }
        /// <summary>
        /// Exports the comics to a json file in the designated path
        /// </summary>
        /// <param name="path">path where JSONfile needs to be exported to</param>
        public void ExportComic(string path) 
        {
            controller.ExportComics(path);
        }
    }
}
