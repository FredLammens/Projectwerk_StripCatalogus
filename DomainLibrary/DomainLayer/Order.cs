using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    public class Order
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
                AddToAmounts(value);
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
        /// <param name="orderComics">comics and amounts of order</param>
        public Order(int id, DateTime date, Dictionary<Comic, int> orderComics)
        {
            Id = id;
            Date = date;
            OrderComics = orderComics;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Adds an order to the list of orders
        /// </summary>
        /// <param name="comic">comic to add to order</param>
        /// <param name="amount">amount of comic to add to order</param>
        public void AddOrderCommic(Comic comic, int amount) 
        {
            if(amount < 0)
                throw new ArgumentException("hoeveelheid kan niet negatief zijn.");
            comic.AmountAvailable += amount;
            _orderComics.Add(comic, amount);
        }
        /// <summary>
        /// Checks if amounts are possible with amounts available in comic
        /// </summary>
        /// <param name="orderComics">list of orders , comics and amount comined</param>
        private void CheckAmount(Dictionary<Comic, int> orderComics)
        {
            if (orderComics.Values.Any(amount => amount < 0))
                throw new ArgumentException("hoeveelheid kan niet negatief zijn.");
            foreach (var orderComic in orderComics)
            {
                if (orderComic.Value > orderComic.Key.AmountAvailable)
                    throw new ArgumentException($"hoeveelheid: {orderComic.Value} overschrijdt hoeveelheid van {orderComic.Key.Title}: {orderComic.Key.AmountAvailable}.");
            }
        }
        /// <summary>
        /// Adds the amount of order to the comic
        /// </summary>
        /// <param name="orderComics">list of orders , comics and amount comined</param>
        private void AddToAmounts(Dictionary<Comic, int> orderComics) 
        {
            foreach (var orderComic in orderComics)
            {
                orderComic.Key.AmountAvailable += orderComic.Value;
            }
        }
        #endregion
        #region Overriden Methods
        public override string ToString()
        {
            String toReturn = $"Bestelling: {Id}\n" +
                  $"Gemaakt op: {Date}\n" +
                  $"Met producten :\n";
            string toAdd = "";
            foreach (var orderComic in OrderComics)
            {
                toAdd += "Strip naam: " + orderComic.Key.Title + "hoeveelheid: " + orderComic.Value + " \n";
            }
            return toReturn + toAdd;
        }
        #endregion
    }
}
