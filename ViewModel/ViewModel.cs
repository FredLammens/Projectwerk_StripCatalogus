using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

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

        public void SearchTitle(string title) 
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Title == title));
        }
        public void SearchSeries(string series)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Series == series));
        }
        public void SearchAuthors(string authors)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where());
        }
        public void SearchPublishingHouse(string publishingHouse)
        {
            Comics = new ObservableCollection<ViewComic>(allComics.Where(c => c.Title == title));
        }
        public void AddComic() { }
        public void ImportComic(string comicsFile) { }
        public void ExportComic() { }
    }
}
