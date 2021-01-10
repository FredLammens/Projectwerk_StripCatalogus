using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    /// <summary>
    /// An interface for the unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        #region Properties
        /// <summary>
        /// A collection of comics in the database.
        /// </summary>
        public IComicRepository Comics { get;}
        /// <summary>
        /// A collection of orders in the database.
        /// </summary>
        public IOrderRepository Orders { get; }
        /// <summary>
        /// A collection of deliveries in the database.
        /// </summary>
        public IDeliveryRepository Deliveries { get; }
        #endregion
        #region Operations
        /// <summary>
        /// Saves the changes made to the database.
        /// </summary>
        public void Commit();
        /// <summary>
        /// Begins transaction.
        /// </summary>
        public void BeginTransaction();
        #endregion
    }
}
