using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    public class Delivery
    {
        #region Properties
        /// <summary>
        /// Id of delivery
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Date of delivery made
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Date delivery planned
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        private Dictionary<Comic, int> _orderComics;
        /// <summary>
        /// List of comics from delivery combined with amounts
        /// </summary>
        public Dictionary<Comic, int> OrderComics
        {
            get => _orderComics;
            private set
            {
                CheckSetOrderComics(value);
                _orderComics = value;
            }
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Delivery() { }
        /// <summary>
        /// Constructor for delivery
        /// </summary>
        /// <param name="id">delivery id</param>
        /// <param name="date">date delivery was registered</param>
        /// <param name="deliveryDate">date for delivery</param>
        /// <param name="orderComics">comics and amounts to be delivered</param>
        public Delivery(int id, DateTime date, DateTime deliveryDate, Dictionary<Comic, int> orderComics)
        {
            Id = id;
            Date = date;
            DeliveryDate = deliveryDate;
            OrderComics = orderComics;
        }

        #endregion
        #region Methods
        /// <summary>
        /// Checks if amounts are possible with amounts available in comic and sets amountavailable.
        /// </summary>
        /// <param name="orderComics">list of orders , comics and amount comined</param>
        private void CheckSetOrderComics(Dictionary<Comic, int> orderComics)
        {
            foreach (var orderComic in orderComics)
            {
                if (orderComic.Value > orderComic.Key.AmountAvailable)
                    throw new ArgumentException($"amount: {orderComic.Value} exceeds amount of {orderComic.Key.Title}: {orderComic.Key.AmountAvailable}.");
                orderComic.Key.AmountAvailable -= orderComic.Value;
            }
        }
        #endregion
        #region overriden methods
        public override string ToString()
        {
            String toReturn = $"Delivery: {Id}\n" +
                              $"Made on: {Date}\n" +
                              $"To be delivered on: {DeliveryDate}" +
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
