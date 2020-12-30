using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.Repositories
{
    /// <summary>
    /// A collection of orders in database
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Adds an order to the database
        /// </summary>
        /// <param name="order">order to add</param>
        public void AddOrder(Order order);
    }
}
