using Data.Repositories;
using DataLayer.Repositories;
using DomainLibrary;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    /// <summary>
    /// Sends commands from the domainlayer to the datalayer.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        /// <summary>
        /// Connection with the datebase.
        /// </summary>
        private AdoNetContext context;
        public IComicRepository Comics { get; }
        public IOrderRepository Orders { get; }
        public IDeliveryRepository Deliveries { get; }

        #endregion

        #region Constructor
        /// <summary>
        /// Makes a UnitOfWork that establishes a connection the the database.
        /// </summary>
        /// <param name="db">Which database to use, defult is production.</param>
        public UnitOfWork(string db = "Production")
        {
            context = new AdoNetContext(true, db);
            if (Comics == null)
            {
                Comics = new ComicRepository(context);
            }

            if (Orders == null)
            {
                Orders = new OrderRepository(context);
            }

            if (Deliveries == null)
            {
                Deliveries = new DeliveryRepository(context);
            }
        }
        #endregion

        public void Commit()
        {
            context.Commit();
        }
        public void BeginTransaction() 
        {
            context.BeginTransaction();
        }
    }
}
