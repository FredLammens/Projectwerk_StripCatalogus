using DataLayer.DataBaseClasses;
using DataLayer.Extension_Methods;
using DomainLibrary.DomainLayer;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
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
        public DeliveryRepository(AdoNetContext context)
        {
            this.context = context;
        }
        #endregion

        #region AddDomainObject
        public void AddDelivery(Delivery delivery)
        {
            DDelivery toAdd = Mapper.toDDelivery(delivery);
            SetComicIds(toAdd);
            AddDDelivery(toAdd);
            LinkStockToDelivery(toAdd);
        }
        #endregion

        #region AddDataObject
        /// <summary>
        /// Add the info of a DDelivery to the databse.
        /// </summary>
        /// <param name="dDelivery">DDelivery to add.</param>
        private void AddDDelivery(DDelivery dDelivery)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = $@"Insert into Deliveries (DeliveryDate, Date) " +
                                        "values ('@deliveryDate' ,'@date') " +
                                        "SELECT CAST(scope_identity() AS int);";

                command.AddParameter("date", dDelivery.Date.ToString());
                command.AddParameter("deliveryDate", dDelivery.DeliveryDate.ToString());
                dDelivery.Id = (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Link the Delivery and the stock and update the stock. Also add the ComicDelivery for the amounds.
        /// </summary>
        /// <param name="dDelivery">DDelivery to use.</param>
        private void LinkStockToDelivery(DDelivery dDelivery)
        {
            using (var command = context.CreateCommand())
            {
                command.AddParameter("delivery_Id", dDelivery.Id);
                int i = 0;
                foreach (var comicPair in dDelivery.OrderComics)
                {
                    command.CommandText = @"Select Stock.ID From Stock " +
                                          $"where Stock.ComicID = @comic_Id{i};";

                    command.AddParameter($"Comic_Id{i}", comicPair.Key.Id);

                    int StockID = (int)command.ExecuteScalar();

                    command.CommandText = @"insert into DeliveriesComics (DeliveryID, StockID, AmountDelivered) " +
                                          $"values (@delivery_Id, @stock_Id{i}, @amount{i});";

                    command.AddParameter($"stock_Id{i}", StockID);
                    command.AddParameter($"amount{i}", comicPair.Value);

                    command.CommandText = @"UPDATE Stock " +
                                          $"SET Stock.Stock += @amount{i}" +
                                          $"WHERE Stock.ID = @stock_Id{i};";

                    i++;
                }

            }
        }
        #endregion

        #region GetIds
        /// <summary>
        /// Sets the id for each comic ordered.
        /// </summary>
        /// <param name="toAdd">Delivery contraining no id.</param>
        private void SetComicIds(DDelivery toAdd)
        {
            using (var command = context.CreateCommand())
            {

                int i = 0;
                foreach (var comic in toAdd.OrderComics.Keys)
                {

                    command.CommandText = @$"Select * From Series Where Series.name = @name{i}";
                    command.AddParameter($"name{i}", comic.Series.Name);
                    int? seriesId = (int?)command.ExecuteScalar();

                    if (seriesId == null)
                        throw new DataException($"Series {comic.Series.Name} is not in the database");

                    command.CommandText = @$"Select * From Comics Where Comics.Title = @title{i}  AND Comics.SeriesNr = @series_Nr{i} AND Comics.Series_ID = @series_Id{i};";
                    command.AddParameter($"title{i}", comic.Title);
                    command.AddParameter($"series_Nr{i}", comic.SeriesNumber);
                    command.AddParameter($"series_Id{i}", seriesId);
                    int? id = (int?)command.ExecuteScalar();

                    if (id == null)
                        throw new DataException($"Comic {comic.Title} is not in the database");

                    comic.Id = (int)id;

                    i++;
                }
            }
        }
        #endregion
    }
}
