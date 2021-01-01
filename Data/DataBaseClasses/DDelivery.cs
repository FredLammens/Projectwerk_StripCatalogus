using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    public class DDelivery
    {
        #region Properties
        /// <summary>
        /// Id of delivery
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date of delivery made
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Date delivery planned
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// List of comics from delivery combined with amounts
        /// </summary>
        public Dictionary<DComic, int> OrderComics { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public DDelivery() { }
        /// <summary>
        /// Constructor for delivery
        /// </summary>
        /// <param name="id">delivery id</param>
        /// <param name="date">date delivery was registered</param>
        /// <param name="deliveryDate">date for delivery</param>
        /// <param name="orderComics">comics and amounts to be delivered</param>
        public DDelivery(int id, DateTime date, DateTime deliveryDate, Dictionary<DComic, int> orderComics)
        {
            Id = id;
            Date = date;
            DeliveryDate = deliveryDate;
            OrderComics = orderComics;
        }

        #endregion

    }
}
