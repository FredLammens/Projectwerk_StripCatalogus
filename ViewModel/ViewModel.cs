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
        private ObservableCollection<ViewComic> comics;
        public ObservableCollection<ViewComic> Comics 
        {
            get { return comics; }
            set {
                comics = value;
                RaisePropertyChanged("Comics");
            }
        }
        private List<ViewComic> allComics;
        public string InputTitle { get; set; }
        public string InputSeries { get; set; }
        public int InputSeriesNr { get; set; }
        public string InputAuthor { get; set; }
        public string InputPublisher { get; set; }

        readonly Controller controller; 



        public void SearchTitle(string title) 
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Title.Contains(title))); //tolower? afhankelijk van hoe in db gestoken 
        }
        public void SearchSeries(string series)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Series.Contains(series)));
        }
        public void SearchAuthors(string authorName)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Authors.Any(a => a.Name.Contains(authorName))));
        }
        public void SearchPublishingHouse(string publishingHouseName)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Publisher.Name.Contains(publishingHouseName)));
        }
        public async Task AddComic() 
        {
            ViewComic comic = new ViewComic(InputTitle, InputSeries, InputSeriesNr, AuthorSplitter(InputAuthor), new ViewPublisher(InputPublisher));
            await Task.Run(() => controller.AddComic(Mapper.ViewComicMapper(comic)));
        }
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
        public void ImportComic(string comicsFile) 
        {

        }
        public void ExportComic() { }
    }
}
