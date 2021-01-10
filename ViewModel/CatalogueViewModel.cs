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
        private Controller controller;
        private List<ViewComic> _comicList;
        private ObservableCollection<GridRow> _filteredCollection;


        #region Constructors
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
        public string FilterQuery { get; set; }

        private GridRow _selectedGridRow;
        public GridRow SelectedGridRow {
            get { return _selectedGridRow; }
            set 
            { 
                _selectedGridRow = value; 
                
            }
        }

        public string SelectedFilterItem { get; set; }

        #endregion

        #region Collections
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

        #region Methods

        #endregion

        #region Commands
        public ICommand FilterCommand
        {
            get;
            internal set;
        }

        private void CreateCommand()
        {
            FilterCommand = new RelayCommand(FilterExecute);
        }

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
