using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    public class Order //todo: add to amount available
    {
        #region Properties
        /// <summary>
        /// Id of order
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Date of order made
        /// </summary>
        public DateTime Date { get; private set; }

        private Dictionary<Comic, int> _orderComics;
        /// <summary>
        /// List of comics from order combined with amounts
        /// </summary>
        public Dictionary<Comic, int> OrderComics 
        {
            get => _orderComics;
            private set 
            {
                CheckAmount(value);
                _orderComics = value;
            } 
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Order() { }
        /// <summary>
        /// Constructor for making an order
        /// </summary>
        /// <param name="id">name of order</param>
        /// <param name="date">date order was made</param>
        /// <param name="orderComics">comics and amounts of order</param> //TODO: int => >0
        public Order(int id, DateTime date, Dictionary<Comic, int> orderComics)
        {
            Id = id;
            Date = date;
            OrderComics = orderComics;
        }
        #endregion
        #region Methods

        /// <summary>
        /// Checks if amounts are possible with amounts available in comic
        /// </summary>
        /// <param name="orderComics">list of orders , comics and amount comined</param>
        private void CheckAmount(Dictionary<Comic, int> orderComics) //TODO: update to add to available
        {
            foreach (var orderComic in orderComics)
            {
                if (orderComic.Value > orderComic.Key.AmountAvailable)
                    throw new ArgumentException($"amount: {orderComic.Value} exceeds amount of {orderComic.Key.Title}: {orderComic.Key.AmountAvailable}.");
            }
        }
        #endregion
        #region Overriden Methods
        public override string ToString()
        {
            String toReturn = $"Order: {Id}\n" +
                  $"Made on: {Date}\n" +
                  $"With Products:\n";
            string toAdd = "";
            foreach (var orderComic in OrderComics)
            {
                toAdd += "Comic Name: " + orderComic.Key.Title + "Amount: " + orderComic.Value + " \n";
            }
            return toReturn + toAdd;
        }
        #endregion
    }
}
