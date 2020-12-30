using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    public class Inventory
    {
        /// <summary>
        /// List of orders made
        /// </summary>
        public static List<Order> Orders { get; private set; }
        /// <summary>
        /// List of deliveries made
        /// </summary>
        public static List<Delivery> Deliveries { get; private set; }
        /// <summary>
        /// Add order to orders
        /// </summary>
        /// <param name="id">id of order</param>
        /// <param name="date">date of order</param>
        /// <param name="orderComics">list of comics to order with amounts</param>
        /// <returns>Order made</returns>
        public static Order AddOrder(int id, DateTime date, Dictionary<Comic, int> orderComics) 
        {
            Order order = new Order(id, date, orderComics);
            Orders.Add(order);
            return order;
        }
        /// <summary>
        /// Add delivery to deliveries
        /// </summary>
        /// <param name="id">id of delivery</param>
        /// <param name="date">date delivery was made</param>
        /// <param name="deliveryDate">date for delivery</param>
        /// <param name="orderComics">list of comics to deliver with amounts</param>
        /// <returns>Delivery made</returns>
        public static Delivery AddDelivery(int id, DateTime date,DateTime deliveryDate ,Dictionary<Comic, int> orderComics)
        {
            Delivery delivery = new Delivery(id, date, deliveryDate, orderComics);
            Deliveries.Add(delivery);
            return delivery;
        }
    }
}
