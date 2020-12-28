using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary.Repositories
{
    /// <summary>
    /// A collection of deliveries in database
    /// </summary>
    public interface IDeliveryRepository
    {
        /// <summary>
        /// Adds a delivery to the database
        /// </summary>
        /// <param name="delivery">delivery to add</param>
        public void AddDelivery(Delivery delivery);
    }
}
