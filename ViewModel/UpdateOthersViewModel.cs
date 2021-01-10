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
        /// <summary>
        /// holds input for new serie
        /// </summary>
        public string InputSeries { get; set; }
        /// <summary>
        /// holds input for new author
        /// </summary>
        public string InputAuthor { get; set; }
        /// <summary>
        /// holds input for new publisher
        /// </summary>
        public string InputPublisher { get; set; }
        /// <summary>
        /// used for binding series to ui
        /// </summary>
        public ObservableCollection<ViewSeries> SeriesList 
        {
            get => _seriesList;
            set 
            {
                _seriesList = value;
                OnPropertyChanged();
            } 
        }
        /// <summary>
        /// used for binding authors to ui
        /// </summary>
        public ObservableCollection<ViewAuthor> AuthorsList 
        { 
            get => _authorsList;
            set 
            {
                _authorsList = value;
                OnPropertyChanged();
            } 
        }
        /// <summary>
        /// used for binding publishers to ui
        /// </summary>
        public ObservableCollection<ViewPublisher> PublishersList 
        {
            get => _publishersList;
            set 
            {
                _publishersList = value;
                OnPropertyChanged();
            } 
        }
        /// <summary>
        /// holds input for old series
        /// </summary>
        public ViewSeries SelectedSeries { get; set; }
        /// <summary>
        /// holds input for old author
        /// </summary>
        public ViewAuthor SelectedAuthor { get; set; }
        /// <summary>
        /// holds input for old publisher
        /// </summary>
        public ViewPublisher SelectedPublisher { get; set; }
        /// <summary>
        /// Command for binding insert series to ui
        /// </summary>
        public ICommand InsertSeriesCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Command for binding insert author to ui
        /// </summary>
        public ICommand InsertAuthorCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Command for binding insert publisher to ui
        /// </summary>
        public ICommand InsertPublisherCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// constructor that sets controller/lists to show and executes linking of commands and methods 
        /// </summary>
        public UpdateOthersViewModel()
        {
            controller = new Controller(new UnitOfWork());
            SeriesList = new ObservableCollection<ViewSeries>(Mapper.SeriesMapper(controller.GetSeries()).OrderBy(name => name));
            AuthorsList = new ObservableCollection<ViewAuthor>(Mapper.AuthorMapper(controller.GetAuthors()).OrderBy(name => name));
            PublishersList = new ObservableCollection<ViewPublisher>(Mapper.PublisherMapper(controller.GetPublishers()).OrderBy(name => name));
            CreateCommand();
        }
        /// <summary>
        /// method for inserting series command
        /// </summary>
        public void InsertSeriesExecute()
        {
            if (InputSeries == null)
                throw new PresentationException("Gelieve series in te vullen.");
            if (SelectedSeries == null)
                throw new PresentationException("Gelieve een serie te selecteren.");
            controller.UpdateSeries(new Series(SelectedSeries.Name),new Series(InputSeries));
            SeriesList = new ObservableCollection<ViewSeries>(Mapper.SeriesMapper(controller.GetSeries()).OrderBy(name => name));
        }
        /// <summary>
        /// method for inserting author command
        /// </summary>
        public void InsertAuthorExecute()
        {
            if (InputAuthor == null)
                throw new PresentationException("Gelieve auteur in te vullen.");
            if(SelectedAuthor == null)
                throw new PresentationException("Gelieve een auteur te selecteren.");
            controller.UpdateAuthor(new Author(SelectedAuthor.Name), new Author(InputAuthor));
            AuthorsList = new ObservableCollection<ViewAuthor>(Mapper.AuthorMapper(controller.GetAuthors()).OrderBy(name => name));
        }
        /// <summary>
        /// method for inserting publisher command
        /// </summary>
        public void InsertPublisherExecute()
        {
            if (InputPublisher == null)
                throw new PresentationException("Gelieve uitgeverij in te vullen.");
            if(SelectedPublisher == null)
                throw new PresentationException("Gelieve een uitgeverij te selecteren.");
            controller.UpdatePublisher(new Publisher(SelectedPublisher.Name), new Publisher(InputPublisher));
            PublishersList = new ObservableCollection<ViewPublisher>(Mapper.PublisherMapper(controller.GetPublishers()).OrderBy(name => name));
        }
        /// <summary>
        /// method for linking coimmands with methods
        /// </summary>
        private void CreateCommand()
        {
            InsertSeriesCommand = new RelayCommand(InsertSeriesExecute);
            InsertPublisherCommand = new RelayCommand(InsertPublisherExecute);
            InsertAuthorCommand = new RelayCommand(InsertAuthorExecute);
        }
    }
}
