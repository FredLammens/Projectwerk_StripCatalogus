
using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class AddComicViewModel : ViewModelBase
    {
        private readonly ViewComic _comic;
        private string _authorFilter;
        private Controller controller;
        private List<ViewAuthor> _allAuthorsList;
        private ObservableCollection<ViewAuthor> _possibleAuthorsList;
        private ObservableCollection<ViewAuthor> _selectedAuthorsList;
        private ObservableCollection<ViewPublisher> _publisherList;
        private ObservableCollection<String> _titelList;

        #region Constructors
        public AddComicViewModel()
        {
            controller = new Controller(new UnitOfWork());
            _comic = new ViewComic();
            _possibleAuthorsList = new ObservableCollection<ViewAuthor>();
            _allAuthorsList = new List<ViewAuthor>(Mapper.AuthorMapper(controller.GetAuthors()));
            _possibleAuthorsList = new ObservableCollection<ViewAuthor>(_allAuthorsList);
            _selectedAuthorsList = new ObservableCollection<ViewAuthor>();
            _publisherList = new ObservableCollection<ViewPublisher>(Mapper.PublisherMapper(controller.GetPublishers()));
            _titelList = new ObservableCollection<string>();

            CreateCommand();
        }
        #endregion
        #region Properties
        /// <summary>
        /// DataBindinded variable for Comic-Title
        /// </summary>
        public string InputTitle
        {
            get { return _comic.Title; }
            set { _comic.Title = value; }
        }
        /// <summary>
        /// DataBindinded variable for Comic-Series
        /// </summary>
        public string InputSeries
        {
            get { return _comic.Series; }
            set { _comic.Series = value; }
        }
        /// <summary>
        /// DataBindinded variable for Comic-Series-Nr
        /// </summary>
        public string InputSeriesNr
        {
            get { return _comic.SeriesNumber.ToString(); }
            set
            {
                int number;
                int.TryParse(value, out number);
                _comic.SeriesNumber = number;
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
        /// Opvullen van titellist
        /// </summary>
        public void FillTitelList()
        {
            string titel1 = "Eerste titel";
            TitelList.Add(titel1);
            string titel2 = "Tweede titel";
            TitelList.Add(titel2);
            string titel3 = "Derde titel";
            TitelList.Add(titel3);
            string titel4 = "Vierde titel";
            TitelList.Add(titel4);
            string titel5 = "Vijfde titel";
            TitelList.Add(titel5);
            string titel6 = "Zesde titel";
            TitelList.Add(titel6);
        }
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
        public ICommand AddCommand
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

        private void CreateCommand()
        {
            AddCommand = new RelayCommand(AddExecute);
            AddAuthorCommand = new RelayCommand(AddAuthorExecute);
            RemoveAuthorCommand = new RelayCommand(RemoveAuthorExecute);
        }

        /// <summary>
        /// Command for add comic
        /// </summary>
        public void AddExecute()
        {
            if (_selectedAuthorsList.Count == 0 || String.IsNullOrEmpty(InputTitle) || (String.IsNullOrEmpty(SelectedViewPublisher.Name) || String.IsNullOrEmpty(InputSeries)))
                throw new PresentationException("Pls fill everything in.");

            ViewComic comic = new ViewComic(InputTitle, InputSeries, _comic.SeriesNumber, new List<ViewAuthor>(_selectedAuthorsList), SelectedViewPublisher);
            controller.AddComic(Mapper.ViewComicMapper(comic));
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
        #endregion








    }
}
