﻿using DataLayer;
using DomainLibrary.DomainLayer;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class OrderDeliveryViewModel : ViewModelBase
    {
        private List<ViewComic> _comics;
        private Controller controller;
        /// <summary>
        /// holds comics to display => order & delivery
        /// </summary>
        public ObservableCollection<KeyValuePair<ViewComic, int>> _comicList = new ObservableCollection<KeyValuePair<ViewComic, int>>();

        #region Constructors
        /// <summary>
        /// constructor that sets controller and comics
        /// </summary>
        public OrderDeliveryViewModel()
        {
            CreateCommand();
        }
        public async Task Init()
        {
            await Task.Run(() =>
            {
                controller = new Controller(new UnitOfWork());
                _comics = new List<ViewComic>(Mapper.ComicsMapper(controller.GetCatalogue().Comics));
            });
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

        private bool _isOrder = true;
        /// <summary>
        /// Databinded variabele to check if Order radiobutton is checked
        /// </summary>
        public bool IsOrder
        {
            get { return _isOrder; }
            set
            {
                _isOrder = value;
                OnPropertyChanged("IsOrder");
            }
        }

        private bool _isDelivery = false;
        /// <summary>
        /// Databinded variabele to check if Delivery radiobutton is checked
        /// </summary>
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
        /// <summary>
        /// commmand for ui to addcomic
        /// </summary>
        public ICommand AddComic
        {
            get;
            internal set;
        }
        /// <summary>
        /// command for ui to removeComic
        /// </summary>
        public ICommand RemoveComic
        {
            get;
            internal set;
        }
        /// <summary>
        /// command for ui to addorder or to add delivery
        /// </summary>
        public ICommand AddOrderDelivery
        {
            get;
            internal set;
        }
        /// <summary>
        /// connects methods to commands
        /// </summary>
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
            Dictionary<Comic, int> comicDict = Mapper.ComicDictMapper(ComicList, controller.GetCatalogue());



            if (IsOrder == true)
                controller.AddOrder(new Order(comicDict));
            else
                controller.AddDelivery(new Delivery(SelectedDate, comicDict));

            ComicList = new ObservableCollection<KeyValuePair<ViewComic, int>>();
        }
        #endregion
    }
}
