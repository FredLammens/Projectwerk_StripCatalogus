using DomainLibrary.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataBaseClasses
{
    /// <summary>
    /// A class to transform object to and from the DataClasses
    /// </summary>
    public class Mapper
    {
        #region toDComic
        /// <summary>
        /// Transforms a Comic object into a DComic object.
        /// </summary>
        /// <param name="comic">Comic to transform.</param>
        /// <returns>A DComic object.</returns>
        public static DComic ToDComic(Comic comic)
        {
            DComic toReturn = new DComic(comic.Title, ToDSeries(comic.Series), comic.SeriesNumber, ToDAuthors(comic.Authors), ToDPublisher(comic.Publisher), comic.AmountAvailable);
            return toReturn;
        }

        /// <summary>
        /// Makes a new DSeries object from a given string.
        /// </summary>
        /// <param name="series">Name of the sereis</param>
        /// <returns>A new DSereis object</returns>
        public static DSeries ToDSeries(Series series)
        {
            return new DSeries(series.Name);
        }

        /// <summary>
        /// Transforms a Publisher object into a DPublisher object.
        /// </summary>
        /// <param name="publisher">Publisher to transform.</param>
        /// <returns>A list of of DPublisher objects.</returns>
        public static DPublisher ToDPublisher(Publisher publisher)
        {
            return new DPublisher(publisher.Name);
        }

        /// <summary>
        /// Transforms Author object into a DAuthor object.
        /// </summary>
        /// <param name="author">Author to transform.</param>
        /// <returns>An Author object.</returns>
        public static DAuthor ToDAuthor(Author author)
        {
            return new DAuthor(author.Name);
        }

        /// <summary>
        /// Transforms a list of Author objects into a list of DAuthor objects.
        /// </summary>
        /// <param name="authors">Authors to transform.</param>
        /// <returns>A list of of DAuthor objects.</returns>
        private static List<DAuthor> ToDAuthors(IReadOnlyList<Author> authors)
        {
            List<DAuthor> toReturn = new List<DAuthor>();

            foreach (var author in authors)
            {
                toReturn.Add(new DAuthor(author.Name));
            }

            return toReturn;
        }
        #endregion

        #region toComic
        /// <summary>
        /// Transforms a DComic object into a Comic object.
        /// </summary>
        /// <param name="dComic">DComic to transform.</param>
        /// <returns>A Comic object.</returns>
        public static Comic ToComic(DComic dComic)
        {
            Comic toReturn = new Comic(dComic.Title, ToSeries(dComic.Series), dComic.SeriesNumber, ToAuthors(dComic.Authors), ToPublisher(dComic.Publisher), dComic.AmountAvailable);
            return toReturn;
        }

        /// <summary>
        /// Transforms a list of DPublisher objects into a list of Publisher objects.
        /// </summary>
        /// <param name="dPublisher">DPublishers to transform.</param>
        /// <returns>A list of of Publisher objects.</returns>
        public static Publisher ToPublisher(DPublisher dPublisher)
        {
            return new Publisher(dPublisher.Name);
        }
        /// <summary>
        /// Transforms a list of DAuthor objects into a list of Author objects.
        /// </summary>
        /// <param name="dAuthors">DAuthors to transform.</param>
        /// <returns>A list of of Author objects.</returns>
        public static List<Author> ToAuthors(List<DAuthor> dAuthors)
        {
            List<Author> toReturn = new List<Author>();

            foreach (var dAuthor in dAuthors)
            {
                toReturn.Add(new Author(dAuthor.Name));
            }

            return toReturn;
        }

        /// <summary>
        /// Makes a new DSeries object from a given string.
        /// </summary>
        /// <param name="dSeries">Name of the sereis</param>
        /// <returns>A new DSereis object</returns>
        public static Series ToSeries(DSeries dSeries)
        {
            return new Series(dSeries.Name);
        }

        /// <summary>
        /// Transforms DAuthor objects into a Author object.
        /// </summary>
        /// <param name="dAuthor">Author to transform.</param>
        /// <returns>A Author object.</returns>
        public static Author ToAuthor(DAuthor dAuthor)
        {
            return new Author(dAuthor.Name);
        }

        #endregion

        #region toDOrder
        /// <summary>
        /// Transforms a Order object into a DOrder object.
        /// </summary>
        /// <param name="order">Order to transform.</param>
        /// <returns>A DOrder object.</returns>
        static public DOrder ToDOrder(Order order)
        {
            DOrder toReturn = new DOrder(order.Id, order.Date, ToDOrderComics(order.OrderComics));

            return toReturn;
        }

        /// <summary>
        /// Transforms a Dictionary of DComics and ints to a Dictionary of Comics and ints.
        /// </summary>
        /// <param name="orderComics">OrderComics to transform.</param>
        /// <returns>The transformed dictionary.</returns>
        private static Dictionary<DComic, int> ToDOrderComics(Dictionary<Comic, int> orderComics)
        {
            Dictionary<DComic, int> toReturn = new Dictionary<DComic, int>();
            foreach (var item in orderComics)
            {
                toReturn.Add(ToDComic(item.Key), item.Value);
            }

            return toReturn;
        }
        #endregion

        #region toOrder
        /// <summary>
        /// Transforms a DOrder object into a Order object.
        /// </summary>
        /// <param name="dOrder">DOrder to transform.</param>
        /// <returns>A Order object.</returns>
        static public Order ToOrder(DOrder dOrder)
        {
            Order toReturn = new Order(dOrder.Id, dOrder.Date, ToOrderComics(dOrder.OrderComics));

            return toReturn;
        }

        /// <summary>
        /// Transforms a Dictionary of DComics and ints to a Dictionary of Comics and ints.
        /// </summary>
        /// <param name="orderComics">OrderComics to transform.</param>
        /// <returns>The transformed dictionary.</returns>
        private static Dictionary<Comic, int> ToOrderComics(Dictionary<DComic, int> orderComics)
        {
            Dictionary<Comic, int> toReturn = new Dictionary<Comic, int>();
            foreach (var item in orderComics)
            {
                toReturn.Add(ToComic(item.Key), item.Value);
            }

            return toReturn;
        }
        #endregion

        #region toDDelivery
        /// <summary>
        /// Transforms a Delivery object into a DDelivery object.
        /// </summary>
        /// <param name="delivery">Delivery to transform.</param>
        /// <returns>A DDelivery object.</returns>
        static public DDelivery toDDelivery(Delivery delivery)
        {
            DDelivery toReturn = new DDelivery(delivery.Id, delivery.Date, delivery.DeliveryDate, ToDOrderComics(delivery.OrderComics));

            return toReturn; 
        }

        #endregion

        #region toDDelivery
        /// <summary>
        /// Transforms a Delivery object into a DDelivery object.
        /// </summary>
        /// <param name="delivery">Delivery to transform.</param>
        /// <returns>A DDelivery object.</returns>
        static public Delivery toDelivery(DDelivery dDelivery)
        {
            Delivery toReturn = new Delivery(dDelivery.Id, dDelivery.Date, dDelivery.DeliveryDate, ToOrderComics(dDelivery.OrderComics));

            return toReturn;
        }

        #endregion

    }
}
