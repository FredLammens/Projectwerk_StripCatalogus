using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class OrderDeliveryViewModel : ViewModelBase
    {
        private List<ViewComic> _comics;
        public ObservableCollection<KeyValuePair<ViewComic, int>> _comicList;
        private Controller controller;

        #region Constructors
        public OrderDeliveryViewModel()
        {
            controller = new Controller(new UnitOfWork());
            _comics = new List<ViewComic>(Mapper.ComicsMapper(controller.GetCatalogue().Comics));
            _comicList = new ObservableCollection<KeyValuePair<ViewComic, int>>();

            CreateCommand();
        }

        #endregion

        #region Properties
        /// <summary>
        /// DataBinded variable for the selected comic in combobox.
        /// </summary>        
        public ViewComic SelectedComic { get; set; }

        /// <summary>
        /// DataBinded variable for the selected comic in grid.
        /// </summary>        
        public KeyValuePair<ViewComic, int> SelectedGridComic { get; set; }

        /// <summary>
        /// DataBinded variable for the amount of a certain comic.
        /// </summary>
        public int OrderAmount { get; set; }

        /// <summary>
        /// Databinded variabele to check if Order radiobutton is checked
        /// </summary>
        private bool _isOrder = true;
        public bool IsOrder
        {
            get { return _isOrder;  }
            set
            {
                _isOrder = value;
                OnPropertyChanged("IsOrder");
            }
        }

        /// <summary>
        /// Databinded variabele to check if Delivery radiobutton is checked
        /// </summary>
        private bool _isDelivery = false;
        public bool IsDelivery
        {
            get { return _isDelivery; }
            set
            {
                _isDelivery = value;
                OnPropertyChanged("IsDelivery");
            }
        }

        /// <summary>
        /// Databinded variabele for the selected date for a delivery
        /// </summary>
        public DateTime SelectedDate { get; set; }
        #endregion

        #region Collections
        /// <summary>
        /// The list of comics added to the order/delivery
        /// </summary>
        public ObservableCollection<KeyValuePair<ViewComic, int>> ComicList
        {
            get { return _comicList; }
            set
            {
                _comicList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// The list we use to fill the combobox with possible comics
        /// </summary>
        /// <returns></returns>
        public List<ViewComic> GetComics()
        {
            return _comics;
        }
        #endregion

        #region Commands
        public ICommand AddComic
        {
            get;
            internal set;
        }
        public ICommand RemoveComic
        {
            get;
            internal set;
        }
        public ICommand AddOrderDelivery
        {
            get;
            internal set;
        }

        private void CreateCommand()
        {
            AddComic = new RelayCommand(AddComicExecute);
            RemoveComic = new RelayCommand(RemoveComicExecute);
            AddOrderDelivery = new RelayCommand(AddOrderDeliveryExecute);
        }

        /// <summary>
        /// Databinded Command to add comic to list of comics
        /// </summary>
        public void AddComicExecute()
        {
            if (SelectedComic == null)
                throw new PresentationException("Gelieve een strip te selecteren.");
            ViewComic comic = SelectedComic;
            int amount = OrderAmount;
            ComicList.Add(new KeyValuePair<ViewComic, int>(comic, amount));
        }

        /// <summary>
        /// Databinded Command to remove comic from list of comics
        /// </summary>
        public void RemoveComicExecute()
        {
            ComicList.Remove(SelectedGridComic);
        }

        /// <summary>
        /// Databinded Command to make an order or delivery
        /// </summary>
        public void AddOrderDeliveryExecute()
        {
            Dictionary<Comic, int> comicDict = Mapper.ComicDictMapper(ComicList);

            if (IsOrder == true)
                controller.AddOrder(new Order(comicDict));
            else
                controller.AddDelivery(new Delivery(SelectedDate, comicDict));
        }
        #endregion
    }
}
