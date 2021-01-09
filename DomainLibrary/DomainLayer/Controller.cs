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
        /// <param name="id">id of order</param>
        /// <param name="date">date of order</param>
        /// <param name="orderComics">list of comics to order with amounts</param>
        public void AddOrder(int id, DateTime date, Dictionary<Comic, int> orderComics) //mag eventueel weg ? ui laag kan hier direct aan ?
        {
            inventory.AddOrder(id, date, orderComics);
        }
        /// <summary>
        /// Adds delivery to inventory
        /// </summary>
        /// <param name="id">id of delivery</param>
        /// <param name="date">date delivery was made</param>
        /// <param name="deliveryDate">date for delivery</param>
        /// <param name="orderComics">list of comics to deliver with amounts</param>
        public void AddDelivery(int id, DateTime date, DateTime deliveryDate, Dictionary<Comic, int> orderComics) //mag eventueel weg ? ui laag kan hier direct aan ?
        {
            inventory.AddDelivery(id, date, deliveryDate, orderComics);
        }
        #endregion

    }
}
