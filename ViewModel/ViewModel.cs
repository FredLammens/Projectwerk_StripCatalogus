using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private ObservableCollection<ComicsView> comics;
        public ObservableCollection<ComicsView> Comics 
        {
            get { return comics; }
            set {
                comics = value;
                RaisePropertyChanged("Comics");
            }
        }
        public string InputTitle { get; set; }
        public string InputSeries { get; set; }
        public int InputSeriesNr { get; set; }
        public string InputAuthor { get; set; }
        public string InputPublisher { get; set; }

        public List<ComicsView> SearchTitle() 
        {
            throw new NotImplementedException();
        }
        public List<ComicsView> SearchSeries()
        {
            throw new NotImplementedException();
        }
        public List<ComicsView> SearchAuthors()
        {
            throw new NotImplementedException();
        }
        public List<ComicsView> SearchPublishingHouse()
        {
            throw new NotImplementedException();
        }
        public void AddComic() { }
        public void ImportComic(string comicsFile) { }
        public void ExportComic() { }
    }
}
