using System;
using System.Collections.Generic;
using System.Linq;
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
        public DateTime Date { get; private set; } = DateTime.Now;
        private DateTime _deliveryDate;
        /// <summary>
        /// Date delivery planned
        /// </summary>
        public DateTime DeliveryDate { 
            get => _deliveryDate;
            set {
                if (value < DateTime.Now)
                    throw new ArgumentException("levering kan niet voor in het verleden zijn.");
                _deliveryDate = value;
            } 
        }

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
        /// <param name="date">date delivery was registered</param>
        /// <param name="deliveryDate">date for delivery</param>
        /// <param name="orderComics">comics and amounts to be delivered</param>
        public Delivery(DateTime deliveryDate, Dictionary<Comic, int> orderComics)
        {
            DeliveryDate = deliveryDate;
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
            if (amount < 0)
                throw new ArgumentException("hoeveelheid kan niet negatief zijn.");
            comic.AmountAvailable += amount;
            _orderComics.Add(comic, amount);
        }
        /// <summary>
        /// Checks if amounts are possible with amounts available in comic and sets amountavailable.
        /// </summary>
        /// <param name="orderComics">list of orders , comics and amount comined</param>
        private void CheckSetOrderComics(Dictionary<Comic, int> orderComics)
        {
            if (orderComics.Values.Any(amount => amount < 0))
                throw new ArgumentException("hoeveelheid kan niet negatief zijn.");
            foreach (var orderComic in orderComics)
            {
                orderComic.Key.AmountAvailable += orderComic.Value;
            }
        }
        #endregion
        #region overriden methods
        public override string ToString()
        {
            String toReturn = $"Levering: {Id}\n" +
                              $"Gemaakt op: {Date}\n" +
                              $"Te leveren op: {DeliveryDate}" +
                              $"Met producten: \n";
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
