using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class CatalogueViewModel : ViewModelBase
    {
        /// <summary>
        /// controller
        /// </summary>
        private Controller controller;
        /// <summary>
        /// holds comics from db
        /// </summary>
        private List<ViewComic> _comicList;

        private ObservableCollection<GridRow> _filteredCollection;


        #region Constructors
        /// <summary>
        /// constructor initializing controller/comics and sets filtered to "De"
        /// </summary>
        public CatalogueViewModel()
        {
            controller = new Controller(new UnitOfWork());
            _comicList = new List<ViewComic>(Mapper.ComicsMapper(controller.GetCatalogue().Comics));
            _filteredCollection = new ObservableCollection<GridRow>();
            FilterQuery = "De";
            SelectedFilterItem = "System.Windows.Controls.ComboBoxItem: Titel";
            FilterExecute();
            CreateCommand();
        }
        #endregion

        #region Properties
        /// <summary>
        /// input for filter query
        /// </summary>
        public string FilterQuery { get; set; }

        private GridRow _selectedGridRow;
        /// <summary>
        /// holds selected grid row
        /// </summary>
        public GridRow SelectedGridRow
        {
            get { return _selectedGridRow; }
            set
            {
                _selectedGridRow = value;
            }
        }
        /// <summary>
        /// holds selecteditem from filter
        /// </summary>
        public string SelectedFilterItem { get; set; }

        #endregion

        #region Collections
        /// <summary>
        /// holds filtered collection
        /// </summary>
        public ObservableCollection<GridRow> FilteredCollection
        {
            get { return _filteredCollection; }
            set
            {
                _filteredCollection = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command for binding with filterbutton
        /// </summary>
        public ICommand FilterCommand
        {
            get;
            internal set;
        }
        /// <summary>
        /// Command for binding with deletebutton
        /// </summary>
        public ICommand DeleteCommand { get; internal set; }
        /// <summary>
        /// Binds commands to methods
        /// </summary>
        private void CreateCommand()
        {
            FilterCommand = new RelayCommand(FilterExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
        }
        /// <summary>
        /// method for deleteCommand
        /// </summary>
        public void DeleteExecute()
        {
            if (_selectedGridRow != null)
            {
                Comic toDelete = Mapper.ViewComicMapper(_selectedGridRow.Comic);
                controller.RemoveComic(toDelete);
                _comicList = new List<ViewComic>(Mapper.ComicsMapper(controller.GetCatalogue().Comics));
                FilterExecute();
            }
            else
                throw new PresentationException("Gelieve iets te selecteren a.u.b.");
        }
        /// <summary>
        /// method for filterCommand
        /// </summary>
        public void FilterExecute()
        {
            FilteredCollection.Clear();

            switch (SelectedFilterItem)
            {
                case "System.Windows.Controls.ComboBoxItem: Titel":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        if (viewComic.Title.Trim().Contains(FilterQuery.Trim()))
                            FilteredCollection.Add(new GridRow(viewComic));
                    }
                    break;

                case "System.Windows.Controls.ComboBoxItem: Reeks":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        if (viewComic.Series.Name.Trim().Contains(FilterQuery.Trim()))
                            FilteredCollection.Add(new GridRow(viewComic));
                    }
                    break;

                case "System.Windows.Controls.ComboBoxItem: Auteur":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        foreach (ViewAuthor author in viewComic.Authors)
                        {
                            if (author.Name.Trim().Contains(FilterQuery.Trim()))
                                FilteredCollection.Add(new GridRow(viewComic));
                        }
                    }
                    break;
                case "System.Windows.Controls.ComboBoxItem: Uitgeverij":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        if (viewComic.Publisher.Name.Trim().Contains(FilterQuery.Trim()))
                            FilteredCollection.Add(new GridRow(viewComic));
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
