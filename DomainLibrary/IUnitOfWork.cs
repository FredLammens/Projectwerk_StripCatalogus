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
        #endregion
        #region Operations
        /// <summary>
        /// Saves the changes made to the database.
        /// </summary>
        public void SaveChanges();
        #endregion
    }
}
