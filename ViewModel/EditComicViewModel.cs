using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class EditComicViewModel : ViewModelBase
    {
        private readonly ViewComic _newComic;
        private string _authorFilter;
        private Controller controller;
        private List<ViewAuthor> _allAuthorsList;
        private ObservableCollection<ViewAuthor> _possibleAuthorsList;
        private ObservableCollection<ViewAuthor> _selectedAuthorsList;
        private ObservableCollection<ViewPublisher> _publisherList;
        private ObservableCollection<string> _titelList;
        private ViewComic _oldComic;
        private ObservableCollection<ViewSeries> _seriesList;

        #region Constructors
        /// <summary>
        /// constructor for setting controller and all used list + binding coimmands and populates comic if selected
        /// </summary>
        /// <param name="oldComic"></param>
        public EditComicViewModel(ViewComic oldComic)
        {
            _oldComic = oldComic;
            controller = new Controller(new UnitOfWork());
            _newComic = new ViewComic(); 
            _seriesList = new ObservableCollection<ViewSeries>(Mapper.SeriesMapper(controller.GetSeries()).OrderBy(name => name));
            _possibleAuthorsList = new ObservableCollection<ViewAuthor>();
            _allAuthorsList = new List<ViewAuthor>(Mapper.AuthorMapper(controller.GetAuthors()));
            _possibleAuthorsList = new ObservableCollection<ViewAuthor>(_allAuthorsList);
            _selectedAuthorsList = new ObservableCollection<ViewAuthor>();
            _publisherList = new ObservableCollection<ViewPublisher>(Mapper.PublisherMapper(controller.GetPublishers()).OrderBy(name => name));
            _titelList = new ObservableCollection<string>();
            CreateCommand();
            SetComic(_oldComic);
        }
        #endregion

        #region Properties
        /// <summary>
        /// DataBindinded variable for Comic-Title
        /// </summary>
        public string InputTitle
        {
            get { return _newComic.Title; }
            set { _newComic.Title = value; }
        }
        /// <summary>
        /// DataBindinded variable for Comic-Series
        /// </summary>
        public ViewSeries SelectedViewSeries { get; set; }

        /// <summary>
        /// DataBindinded variable for Comic-Series-Nr
        /// </summary>
        public string InputSeriesNr
        {
            get { return _newComic.SeriesNumber.ToString(); }
            set
            {
                int number;
                int.TryParse(value, out number);
                _newComic.SeriesNumber = number;
            }
        }
        ///<summary>
        /// Databinded variable for filtering out authors
        /// </summary>        
        public string AuthorFilter
        {
            get { return _authorFilter; }
            set
            {
                _authorFilter = value;
                OnPropertyChanged(_authorFilter);
                FilterOutAuthors(this.AuthorFilter);
            }
        }
        /// <summary>
        /// Databinded variable for the selected Author in possible author list
        /// </summary>
        public ViewAuthor SelectedPossibleAuthor { get; set; }
        /// <summary>
        /// Databinded variable for the selected Author in selected author list
        /// </summary>
        public ViewAuthor SelectedCurrentAuthor { get; set; }
        /// <summary>
        /// Databinded variable for the currently selected publisher
        /// </summary>
        public ViewPublisher SelectedViewPublisher { get; set; }
        #endregion

        #region Collections
        /// <summary>
        /// A list of possible authors that can be added to te comic
        /// </summary>
        public ObservableCollection<ViewAuthor> PossibleAuthorsList
        {
            get { return _possibleAuthorsList; }
            set
            {
                _possibleAuthorsList = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// A list of authors that will be added to the comic
        /// </summary>
        public ObservableCollection<ViewAuthor> SelectedAuthorList
        {
            get { return _selectedAuthorsList; }
            set
            {
                _selectedAuthorsList = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// A list of all possible publishers
        /// </summary>
        public ObservableCollection<ViewPublisher> PublisherList
        {
            get { return _publisherList; }
            set
            {
                _publisherList = value;
            }
        }
        public ObservableCollection<ViewSeries> SeriesList
        {
            get { return _seriesList; }
            set
            {
                _seriesList = value;
            }
        }
        /// <summary>
        /// A list of all titles already in the database
        /// </summary>
        public ObservableCollection<String> TitelList
        {
            get { return _titelList; }
            set
            {
                _titelList = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for filtering out possible authors depending on a given query
        /// </summary>
        /// <param name="query"></param>
        public void FilterOutAuthors(string query)
        {
            if (query == "")
                PossibleAuthorsList = new ObservableCollection<ViewAuthor>(_allAuthorsList);
            else
            {
                PossibleAuthorsList.Clear();
                foreach (ViewAuthor viewAuthor in _allAuthorsList)
                {
                    if (viewAuthor.Name.ToLower().StartsWith(query.ToLower()))
                    {
                        PossibleAuthorsList.Add(viewAuthor);
                    }
                }
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Databinded command for add comic
        /// </summary>
        public ICommand UpdateCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Databinded command for adding author to the selected author list
        /// </summary>
        public ICommand AddAuthorCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Databinded command for removing author from the selected author list
        /// </summary>
        public ICommand RemoveAuthorCommand
        {
            get;
            internal set;
        }

        public ICommand SetComicCommand
        {
            get;
            internal set;
        }

        private void CreateCommand()
        {
            UpdateCommand = new RelayCommand(UpdateExecute);
            AddAuthorCommand = new RelayCommand(AddAuthorExecute);
            RemoveAuthorCommand = new RelayCommand(RemoveAuthorExecute);
        }

        /// <summary>
        /// Command for add comic
        /// </summary>
        public void UpdateExecute()
        {
            if (_selectedAuthorsList.Count == 0 || String.IsNullOrEmpty(InputTitle) || (String.IsNullOrEmpty(SelectedViewPublisher.Name) || String.IsNullOrEmpty(SelectedViewSeries.Name)))
                throw new PresentationException("Pls fill everything in.");

            ViewComic comic = new ViewComic(InputTitle, SelectedViewSeries, _newComic.SeriesNumber, new List<ViewAuthor>(_selectedAuthorsList), SelectedViewPublisher);
            controller.UpdateComic(Mapper.ViewComicMapper(_oldComic),Mapper.ViewComicMapper(comic));
            _oldComic = comic;
        }
        /// <summary>
        /// Command for adding author to the selected author list
        /// </summary>
        public void AddAuthorExecute()
        {
            ViewAuthor author = SelectedPossibleAuthor;

            PossibleAuthorsList.Remove(author);
            SelectedAuthorList.Add(author);
        }
        /// <summary>
        /// Command for removing author from the selected author list
        /// </summary>
        public void RemoveAuthorExecute()
        {
            ViewAuthor author = SelectedCurrentAuthor;

            SelectedAuthorList.Remove(author);
            PossibleAuthorsList.Add(author);
        }
        /// <summary>
        /// sets ui values to the selectedcomic
        /// </summary>
        /// <param name="comic">comic to set</param>
        public void SetComic(ViewComic comic)
        {
            InputTitle = comic.Title;
            SelectedViewSeries = comic.Series;
            InputSeriesNr = comic.SeriesNumber.ToString();

            foreach (ViewAuthor author in comic.Authors)
            {
                SelectedAuthorList.Add(author);
                PossibleAuthorsList.Remove(author);
            }

            SelectedViewPublisher = comic.Publisher;
        }
        #endregion

    }
}
