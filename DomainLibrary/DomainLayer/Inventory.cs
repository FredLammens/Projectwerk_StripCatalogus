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
        public List<Order> Orders { get; private set; } = new List<Order>();
        /// <summary>
        /// List of deliveries made
        /// </summary>
        public List<Delivery> Deliveries { get; private set; } = new List<Delivery>();
        /// <summary>
        /// Add order to inventory
        /// </summary>
        /// <param name="order">order to add</param>
        public void AddOrder(Order order) 
        {
            Orders.Add(order);
        }
        /// <summary>
        /// Add delivery to inventory
        /// </summary>
        /// <param name="delivery">delivery to add</param>
        public void AddDelivery(Delivery delivery)
        {
            Deliveries.Add(delivery);
        }
    }
}
