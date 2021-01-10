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
            if (catalogue.AddComic(comic))
            {
                uow.BeginTransaction();
                uow.Comics.AddComic(comic);
                uow.Commit();
            }
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
                if (catalogue.AddComic(comic))
                    uow.Comics.AddComic(comic);
            }
            uow.Commit();
        }
        /// <summary>
        /// Removes comic from catalogue and database
        /// </summary>
        /// <param name="comic">comic to remove</param>
        public void RemoveComic(Comic comic)
        {
            if (catalogue.RemoveComic(comic))
            {
                uow.BeginTransaction();
                uow.Comics.RemoveComic(comic);
                uow.Commit();
            }
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
        /// Adds author to database
        /// </summary>
        /// <param name="author">author to add</param>
        public void AddAuthor(Author author)
        {
            uow.BeginTransaction();
            uow.Comics.AddAuthor(author);
            uow.Commit();
        }
        /// <summary>
        /// Updates author from database
        /// </summary>
        /// <param name="oldAuthor">author to update</param>
        /// <param name="newAuthor">author with updated values</param>
        public void UpdateAuthor(Author oldAuthor, Author newAuthor)
        {
            uow.BeginTransaction();
            uow.Comics.UpdateAuthor(oldAuthor, newAuthor);
            uow.Commit();
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
        /// Adds publisher to database
        /// </summary>
        /// <param name="publisher">publisher to add</param>
        public void AddPublisher(Publisher publisher)
        {
            uow.BeginTransaction();
            uow.Comics.AddPublisher(publisher);
            uow.Commit();
        }
        /// <summary>
        /// Updates publisher in database
        /// </summary>
        /// <param name="oldPublisher">publisher to update.</param>
        /// <param name="newPublisher">publisher with values to update.</param>
        public void UpdatePublisher(Publisher oldPublisher, Publisher newPublisher)
        {
            uow.BeginTransaction();
            uow.Comics.UpdatePublisher(oldPublisher, newPublisher);
            uow.Commit();
        }
        /// <summary>
        /// Gets series of catalogue
        /// </summary>
        /// <returns>list of series</returns>
        public List<Series> GetSeries()
        {
            return uow.Comics.GetAllSeries().ToList();
        }
        /// <summary>
        /// Adds serie to database
        /// </summary>
        /// <param name="series">serie to add</param>
        public void AddSeries(Series series)
        {
            uow.BeginTransaction();
            uow.Comics.AddSeries(series);
            uow.Commit();
        }
        /// <summary>
        /// Updates serie in database
        /// </summary>
        /// <param name="oldSeries">serie to update</param>
        /// <param name="newSeries">serie with updated values</param>
        public void UpdateSeries(Series oldSeries, Series newSeries) 
        {
            uow.BeginTransaction();
            uow.Comics.UpdateSeries(oldSeries, newSeries);
            uow.Commit();
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
        /// <param name="oldComic">comic to update</param>
        /// <param name="UpdatedComic">comic with updated values</param>
        public void UpdateComic(Comic oldComic, Comic updatedComic)
        {
            if (oldComic.GetHashCode() != updatedComic.GetHashCode())
            {
                uow.BeginTransaction();
                catalogue.UpdateComic(oldComic, updatedComic);
                uow.Comics.UpdateComic(oldComic, updatedComic);
                uow.Commit();
            }
        }
        #endregion

    }
}
