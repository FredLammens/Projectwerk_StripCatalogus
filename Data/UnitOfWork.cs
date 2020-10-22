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
        #endregion

        #region Constructor
        /// <summary>
        /// Makes a UnitOfWork that establishes a connection the the database.
        /// </summary>
        public UnitOfWork()
        {
            string connection = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Projectwerk-Stripcatalogus;Integrated Security=True;Pooling=False";
            context = new AdoNetContext(connection, true);
            if (Comics == null)
            {
                Comics = new ComicRepository(context);
            }
        }
        #endregion

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
