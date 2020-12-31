using Data.Repositories;
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
        public UnitOfWork()
        {
            context = new AdoNetContext(true);
            if (Comics == null)
            {
                Comics = new ComicRepository(context);
            }

            if (Orders == null)
            {
                Comics = new ComicRepository(context);
            }

            if (Deliveries == null)
            {
                Comics = new ComicRepository(context);
            }
        }
        #endregion

        public void Commit()
        {
            context.Commit();
        }
    }
}
