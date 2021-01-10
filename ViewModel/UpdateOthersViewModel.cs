using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class UpdateOthersViewModel : ViewModelBase
    {
        private Controller controller;
        private ObservableCollection<ViewSeries> _seriesList;
        private ObservableCollection<ViewAuthor> _authorsList;
        private ObservableCollection<ViewPublisher> _publishersList;
        public string InputSeries { get; set; }
        public string InputAuthor { get; set; }
        public string InputPublisher { get; set; }
        public ObservableCollection<ViewSeries> SeriesList 
        {
            get => _seriesList;
            set 
            {
                _seriesList = value;
                OnPropertyChanged();
            } 
        }
        public ObservableCollection<ViewAuthor> AuthorsList 
        { 
            get => _authorsList;
            set 
            {
                _authorsList = value;
                OnPropertyChanged();
            } 
        }
        public ObservableCollection<ViewPublisher> PublishersList 
        {
            get => _publishersList;
            set 
            {
                _publishersList = value;
                OnPropertyChanged();
            } 
        }
        public ViewSeries SelectedSeries { get; set; }
        public ViewAuthor SelectedAuthor { get; set; }
        public ViewPublisher SelectedPublisher { get; set; }

        public ICommand InsertSeriesCommand
        {
            get;
            internal set;
        }
        public ICommand InsertAuthorCommand
        {
            get;
            internal set;
        }
        public ICommand InsertPublisherCommand
        {
            get;
            internal set;
        }
        public UpdateOthersViewModel()
        {
            controller = new Controller(new UnitOfWork());
            SeriesList = new ObservableCollection<ViewSeries>(Mapper.SeriesMapper(controller.GetSeries()).OrderBy(name => name));
            AuthorsList = new ObservableCollection<ViewAuthor>(Mapper.AuthorMapper(controller.GetAuthors()).OrderBy(name => name));
            PublishersList = new ObservableCollection<ViewPublisher>(Mapper.PublisherMapper(controller.GetPublishers()).OrderBy(name => name));
            CreateCommand();
        }
        public void InsertSeriesExecute()
        {
            if (InputSeries == null)
                throw new PresentationException("Gelieve series in te vullen.");
            if (SelectedSeries == null)
                throw new PresentationException("Gelieve een serie te selecteren.");
            controller.UpdateSeries(new Series(SelectedSeries.Name),new Series(InputSeries));
            SeriesList = new ObservableCollection<ViewSeries>(Mapper.SeriesMapper(controller.GetSeries()).OrderBy(name => name));
        }
        public void InsertAuthorExecute()
        {
            if (InputAuthor == null)
                throw new PresentationException("Gelieve auteur in te vullen.");
            if(SelectedAuthor == null)
                throw new PresentationException("Gelieve een auteur te selecteren.");
            controller.UpdateAuthor(new Author(SelectedAuthor.Name), new Author(InputAuthor));
            AuthorsList = new ObservableCollection<ViewAuthor>(Mapper.AuthorMapper(controller.GetAuthors()).OrderBy(name => name));
        }
        public void InsertPublisherExecute()
        {
            if (InputPublisher == null)
                throw new PresentationException("Gelieve uitgeverij in te vullen.");
            if(SelectedPublisher == null)
                throw new PresentationException("Gelieve een uitgeverij te selecteren.");
            controller.UpdatePublisher(new Publisher(SelectedPublisher.Name), new Publisher(InputPublisher));
            PublishersList = new ObservableCollection<ViewPublisher>(Mapper.PublisherMapper(controller.GetPublishers()).OrderBy(name => name));
        }
        private void CreateCommand()
        {
            InsertSeriesCommand = new RelayCommand(InsertSeriesExecute);
            InsertPublisherCommand = new RelayCommand(InsertPublisherExecute);
            InsertAuthorCommand = new RelayCommand(InsertAuthorExecute);
        }
    }
}
