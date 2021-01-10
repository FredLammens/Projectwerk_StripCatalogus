using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ViewModel.PresentationBaseClasses;

namespace ViewModel
{
    public class CatalogueViewModel : ViewModelBase
    {
        private readonly ViewComic _selectedComic;
        private Controller controller;
        private List<ViewComic> _comicList;
        private ObservableCollection<GridRow> _filteredCollection;



        #region Constructors
        public CatalogueViewModel()
        {
            controller = new Controller(new UnitOfWork());
            _selectedComic = new ViewComic();
            _comicList = new List<ViewComic>(Mapper.ComicsMapper(controller.GetCatalogue().Comics));
            _filteredCollection = new ObservableCollection<GridRow>();

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

        public ICommand EditComicCommand
        {
            get;
            internal set;
        }

        private void CreateCommand()
        {
            FilterCommand = new RelayCommand(FilterExecute);
            EditComicCommand = new RelayCommand(EditComicExecute);
        }

        public void FilterExecute()
        {
            FilteredCollection.Clear();

            switch (SelectedFilterItem)
            {
                case "System.Windows.Controls.ComboBoxItem: Titel":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        if (viewComic.Title.Trim() == FilterQuery.Trim())
                            FilteredCollection.Add(new GridRow(viewComic));
                    }
                    break;

                case "System.Windows.Controls.ComboBoxItem: Reeks":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        if (viewComic.Series.Name == FilterQuery)
                            FilteredCollection.Add(new GridRow(viewComic));
                    }
                    break;

                case "System.Windows.Controls.ComboBoxItem: Auteur":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        foreach (ViewAuthor author in viewComic.Authors)
                        {
                            if (author.Name == FilterQuery)
                                FilteredCollection.Add(new GridRow(viewComic));
                        }
                    }
                    break;
                case "System.Windows.Controls.ComboBoxItem: Uitgeverij":
                    foreach (ViewComic viewComic in _comicList)
                    {
                        if (viewComic.Publisher.Name == FilterQuery)
                            FilteredCollection.Add(new GridRow(viewComic));
                    }
                    break;

                default:
                    break;
            }
        }

        public void EditComicExecute()
        {

        }
        #endregion
        public class GridRow
        {
            private ViewComic comic;

            public String Title { get; set; }
            public String Series { get; set; }
            public String SeriesNumber { get; set; }
            public String Authors { get; set; }
            public String Publishers { get; set; }

            public GridRow(ViewComic viewComic)
            {
                this.comic = viewComic;

                Title = comic.Title;
                Series = comic.Series.Name;
                SeriesNumber = comic.SeriesNumber.ToString();

                foreach (ViewAuthor author in viewComic.Authors)
                {
                    Authors = Authors + author.Name + ", ";
                }

                Publishers = viewComic.Publisher.Name;
            }
        }
    }
}
