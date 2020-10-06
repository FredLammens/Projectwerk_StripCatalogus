using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLibrary
{
    /// <summary>
    /// An interface for the unit of work.
    /// </summary>
    interface IUnitOfWork
    {
        #region Properties
        /// <summary>
        /// A collection of comics in the database.
        /// </summary>
        public IComicRepository Comics { get; set; }
        #endregion
        #region Operations
        /// <summary>
        /// Will finish the database operation.
        /// </summary>
        /// <returns>Wheter the operation was a succes.</returns>
        public int Complete();
        #endregion
    }
}
