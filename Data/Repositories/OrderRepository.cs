using DomainLibrary.DomainLayer;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        #region Properties
        /// <summary>
        /// Connection with the datebase.
        /// </summary>
        private AdoNetContext context;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to make a ComicRepository.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public OrderRepository(AdoNetContext context)
        {
            this.context = context;
        }
        #endregion

        #region AddFomainObject
        public void AddOrder(Order order)
        {
            
        }
        #endregion



    }
}
