using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DomainLibrary.DomainLayer
{
    /// <summary>
    /// Controlls all actions in the domain layer.
    /// </summary>
    public class Controller
    {
        #region Properties
        private readonly IUnitOfWork uow;
        private readonly Catalogue catalogue;
        private readonly Inventory inventory;
        #endregion

        #region Constuctors
        /// <summary>
        /// A constructor that makes a Controller object and initializes the catalogue with all comics from the database.
        /// </summary>
        /// <param name="uow">An object that implements the IUnitOfWork interface.</param>
        public Controller(IUnitOfWork uow)
        {
            this.uow = uow;
            //laden van catalogue
            catalogue = new Catalogue(uow.Comics.GetComics().ToList());
            inventory = new Inventory();
        }
        #endregion

        #region Operations
        /// <summary>
        /// Adds a comic to the database and catalogue.
        /// </summary>
        /// <param name="comic">Comic to add.</param>
        public void AddComic(Comic comic)
        {
            uow.BeginTransaction();
            catalogue.AddComic(comic);
            uow.Comics.AddComic(comic);
            uow.Commit();
        }
        /// <summary>
        /// Adds comics to database and catalogue.
        /// </summary>
        /// <param name="comics">List of comics to add</param>
        public void AddComics(IList<Comic> comics) 
        {
            uow.BeginTransaction();
            foreach (Comic comic in comics)
            {
                if(catalogue.AddComic(comic))
                uow.Comics.AddComic(comic);
            }
            uow.Commit();
        }
        /// <summary>
        /// Returns the catalogue
        /// </summary>
        /// <returns>A catalogue of comics.</returns>
        public Catalogue GetCatalogue()
        {        
            return catalogue;
        }
        /// <summary>
        /// Returns a list of all the autors in the database
        /// </summary>
        /// <returns>A list of authors.</returns>
        public List<Author> GetAuthors()
        {
            return uow.Comics.GetAllAuthors().ToList();
        }
        /// <summary>
        /// Returns a list of all the publishers in the database
        /// </summary>
        /// <returns>A list of publishers.</returns>
        public List<Publisher> GetPublishers()
        {
            return uow.Comics.GetAllPublishers().ToList();
        }
        /// <summary>
        /// Adds order to inventory
        /// </summary>
        public void AddOrder(Order order)
        {
            uow.BeginTransaction();
            inventory.AddOrder(order);
            uow.Orders.AddOrder(order);
            uow.Commit();
        }
        /// <summary>
        /// Adds delivery to inventory
        /// </summary>
        /// <param name="id">id of delivery</param>
        /// <param name="date">date delivery was made</param>
        /// <param name="deliveryDate">date for delivery</param>
        /// <param name="orderComics">list of comics to deliver with amounts</param>
        public void AddDelivery(Delivery delivery)
        {
            uow.BeginTransaction();
            inventory.AddDelivery(delivery); //Todo: fix
            uow.Deliveries.AddDelivery(delivery);
            uow.Commit();
        }
        /// <summary>
        /// Updates comic from catalogue
        /// </summary>
        /// <param name="comic">comic to update</param>
        public void UpdateComic(Comic comic) 
        {
            uow.BeginTransaction();
            //catalogue.UpdateComic(comic.)
            //uow.Comics.UpdateComic()
        }
        #endregion

    }
}
