using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    public class DOrder
    {
        #region Properties
        /// <summary>
        /// Id of order
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Date of order made
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// List of comics from order combined with amounts
        /// </summary>
        public Dictionary<DComic, int> OrderComics;
        #endregion

        #region Constructors
        /// <summary>
        /// Empty constructor
        /// </summary>
        public DOrder()
        {

        }

        /// <summary>
        /// Constructor for making an order
        /// </summary>
        /// <param name="id">name of order</param>
        /// <param name="date">date order was made</param>
        /// <param name="orderComics">comics and amounts of order</param>
        public DOrder(int id, DateTime date, Dictionary<DComic, int> orderComics)
        {
            Id = id;
            Date = date;
            OrderComics = orderComics;
        }

        #endregion
    }
}
